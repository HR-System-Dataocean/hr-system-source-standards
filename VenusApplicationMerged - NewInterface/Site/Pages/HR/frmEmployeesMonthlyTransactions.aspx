<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesMonthlyTransactions.aspx.vb"
    Inherits="frmEmployeesMonthlyTransactions" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Employees Monthly Transactions</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmEmployeesMonthlyTransactions.js" type="text/javascript"></script>
    <script type="text/javascript" id="Infragistics">
        function CloseMe() {
            parent.CloseIt("");
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesMonthlyTransactions" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:DropDownList ID="ddlPeriod" runat="server" SkinID="DropDownList_LargNormal"
            meta:resourcekey="ddlPeriodResource1">
        </asp:DropDownList>
        <asp:HiddenField runat="server" ID="hdnTotalMonthlyDays" />
        <asp:HiddenField runat="server" ID="hdnPrepared" />
        <asp:HiddenField runat="server" ID="hdnLang" />
        <asp:HiddenField runat="server" ID="hdnContractID" />
        <asp:HiddenField ID="txtEmployeeID" runat="server" />
        <asp:TextBox ID="txtWorkingHoursPerDay" runat="server" meta:resourcekey="txtWorkingHoursPerDayResource1"></asp:TextBox>
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
                                                            <igtxt:WebImageButton ID="btnRefund" runat="server" Height="18px" meta:resourcekey="btnRefundResource1"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" Style="cursor: pointer;">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Delete.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 5px">
                                                            <asp:Label ID="Label_TSP4" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                Text="|" Width="5px"></asp:Label>
                                                        </td>
                                                        <td style="width: 100%">
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:CheckBox ID="ChkPaid" runat="server" meta:resourcekey="ChkPaidRes" Text="Hide Not Paid"
                                                                Width="180px" AutoPostBack="True" Checked="True" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black" />
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
                                                            <asp:Label ID="txtEmployeeCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
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
                                                            <asp:Label ID="lbError" runat="server" Width="100%" SkinID="Label_WarningBold" meta:resourcekey="lblMSGResource1"></asp:Label>
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
                                                            <asp:Label ID="lbCurrentPeriod" runat="server" Width="100px" SkinID="Label_DefaultNormal"
                                                                Text="Current Period" meta:resourcekey="lbCurrentPeriodResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblCurrentPeriod" runat="server" SkinID="TextBox_DefaultNormalC"
                                                                ReadOnly="True" meta:resourcekey="lblCurrentPeriodResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblNoOfWorkingUnits" runat="server" meta:resourcekey="lblNoOfWorkingUnitsResource1"
                                                                SkinID="Label_DefaultNormal" Text="Working Days" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtNoOfWorkingUnits" runat="server" meta:resourcekey="txtNoOfWorkingUnitsResource1"
                                                                ReadOnly="True" SkinID="TextBox_DefaultNormalC"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="lbBasicSalary" runat="server" meta:resourcekey="lbBasicSalaryResource1"
                                                                SkinID="Label_DefaultNormal" Text="Basic Salary" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblBasicSalary" runat="server" meta:resourcekey="lblBasicSalaryResource1"
                                                                ReadOnly="True" SkinID="TextBox_DefaultNormalC"></asp:TextBox>
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
                                                            <asp:Label ID="lbSalaryPerDay" runat="server" meta:resourcekey="lbSalaryPerDayResource1"
                                                                SkinID="Label_DefaultNormal" Text="Salary per day" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblSalaryPerDay" runat="server" meta:resourcekey="lblSalaryPerDayResource1"
                                                                ReadOnly="True" SkinID="TextBox_DefaultNormalC"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="lbTotalLate" runat="server" meta:resourcekey="lbTotalLateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Total Late (H)" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblTotalLate" runat="server" meta:resourcekey="lblTotalLateResource1"
                                                                ReadOnly="True" SkinID="TextBox_DefaultNormalC"></asp:TextBox>
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
                                                            <asp:Label ID="lbSalaryPerHour" runat="server" meta:resourcekey="lbSalaryPerHourResource1"
                                                                SkinID="Label_DefaultNormal" Text="Salary per hour" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblSalaryPerHour" runat="server" meta:resourcekey="lblSalaryPerHourResource1"
                                                                ReadOnly="True" SkinID="TextBox_DefaultNormalC"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="lblOvertimeWorkHours" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="Overtime Work (H)" Width="100px" meta:resourcekey="lblOvertimeWorkHoursResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtOvertimeWorkHours" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="txtOvertimeWorkHoursResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lbOverTimePerHour" runat="server" SkinID="Label_DefaultNormal" Text="Overtime per hour"
                                                                Width="100px" meta:resourcekey="lbOverTimePerHourResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblOverTimePerHour" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="lblOverTimePerHourResource1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="lblHolidayWorkHours" runat="server" SkinID="Label_DefaultNormal" Text="Holiday Work (H)"
                                                                Width="100px" meta:resourcekey="lblHolidayWorkHoursResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtHolidayWorkHours" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="txtHolidayWorkHoursResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbHolidayPerHour" runat="server" SkinID="Label_DefaultNormal" Text="Holiday per hour"
                                                                Width="100px" meta:resourcekey="lbHolidayPerHourResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblHolidayPerHour" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="lblHolidayPerHourResource1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Employee Benefits" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title2" runat="server" meta:resourcekey="Label_Title2Resource1"
                                                                SkinID="Label_DefaultBold" Text="Employee Pyabilities"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgEmployeeTransaction" runat="server" EnableAppStyling="False"
                                                    Height="180px" SkinID="Default" Width="100%" meta:resourcekey="uwgEmployeeTransactionResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                        RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                            Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="uwgEmployeeTransaction_AfterCellUpdateHandler" />
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
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
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
                                                                <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                    Key="TransactionTypeID" Type="DropDownList" Width="60%" meta:resourcekey="UltraGridColumnResource18">
                                                                    <Header Caption="Transaction Type">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Amount" DataType="System.Double" FooterTotal="Sum"
                                                                    Format="###,###,###.##" Key="Value" AllowUpdate="No" Width="70px" meta:resourcekey="UltraGridColumnResource19">
                                                                    <Header Caption="Value ">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer Total="Sum">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Description" AllowUpdate="No" Key="Description"
                                                                    Width="40%" meta:resourcekey="UltraGridColumnResource20">
                                                                    <Header Caption="Description">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="TransactionTypeID" Hidden="true"
                                                                    Key="Transaction Type" Width="0px" meta:resourcekey="UltraGridColumnResource21">
                                                                    <Header Caption="Transaction Type">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" AllowUpdate="No" Hidden="true" Key="ID"
                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource22">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EmpSchId" AllowUpdate="No" Hidden="true" Width="0px"
                                                                    Key="EmpSchId" meta:resourcekey="UltraGridColumnResource23">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DescriptionSign" AllowUpdate="No" Hidden="true"
                                                                    Key="DescriptionSign" Width="0px" meta:resourcekey="UltraGridColumnResource24">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Used" Hidden="true" AllowUpdate="No" Key="Used"
                                                                    Width="0px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OrgAmount" Hidden="true" AllowUpdate="No"
                                                                    Key="OrgValue" Width="0px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPayabilities" runat="server" EnableAppStyling="False"
                                                    Height="180px" SkinID="Default" Width="100%" meta:resourcekey="uwgPayabilitiesResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                        RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                            Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="frmEmpMonthlyTransuwgPayabilities_AfterCellUpdateHandler" />
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
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource3">
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
                                                                <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                    Key="TransactionTypeID" Type="DropDownList" Width="60%" meta:resourcekey="UltraGridColumnResource25">
                                                                    <Header Caption="Transaction Type">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Amount" DataType="System.Double" Format="###,###,###.##"
                                                                    Key="Value" Width="70px" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource26">
                                                                    <Header Caption="Value ">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Description" AllowUpdate="No" Key="Description"
                                                                    Width="40%" meta:resourcekey="UltraGridColumnResource27">
                                                                    <Header Caption="Description">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TransactionTypeID" AllowUpdate="No" Hidden="true"
                                                                    Key="Transaction Type" Width="0px" meta:resourcekey="UltraGridColumnResource28">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EmpSchId" AllowUpdate="No" Key="EmpSchId"
                                                                    Width="0px" Hidden="true" meta:resourcekey="UltraGridColumnResource29">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DescriptionSign" AllowUpdate="No" Hidden="true"
                                                                    Key="DescriptionSign" Width="0px" meta:resourcekey="UltraGridColumnResource30">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Used" AllowUpdate="No" Hidden="true" Key="Used"
                                                                    Width="0px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OrgAmount" AllowUpdate="No" Hidden="true"
                                                                    Key="OrgValue" Width="0px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbTotalBenefits" runat="server" SkinID="Label_DefaultNormal" Text="Total Benefits"
                                                                Width="100px" meta:resourcekey="lbTotalBenefitsResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblTotalBenefits" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="lblTotalBenefitsResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lbTotalDeductions" runat="server" SkinID="Label_DefaultNormal" Text="Total Deductions"
                                                                Width="100px" meta:resourcekey="lbTotalDeductionsResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblTotalDeductions" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="lblTotalDeductionsResource1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
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
                                                            <asp:Label ID="lbNetSalary" runat="server" SkinID="Label_DefaultNormal" Text="Net Salary"
                                                                Width="100px" meta:resourcekey="lbNetSalaryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblNetSalary" runat="server" ReadOnly="True" SkinID="TextBox_DefaultNormalC"
                                                                meta:resourcekey="lblNetSalaryResource1"></asp:TextBox>
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
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
