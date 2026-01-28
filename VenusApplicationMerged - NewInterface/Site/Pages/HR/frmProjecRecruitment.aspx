<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjecRecruitment.aspx.vb"
    Inherits="frmProjecRecruitment" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmProjecRecruitment</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function uwg_ClickCellButtonHandler(gridName, cellId) {
            var cell = igtbl_getCellById(cellId);
            if (cell.Index == 8) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellDetailID = Row.getCellFromKey("LocationDetailID")
                var currDetailID = cellDetailID.getValue()

                if (currProjectEmployee != null) {
                    var hight = 600;
                    var width = 1000;
                    var win = window.open("frmProjectPlacementPlanning.aspx?LocationDetailID=" + currDetailID + "&PlacementsID=" + currProjectEmployee, "_NEW", "height=" + hight + ",width=" + width + ",resizable=1,menubar=0,toolbar=0,location=0,directories=0,scrollbars=1,status=0,center=0");
                    win.focus();
                }
            }
            else if (cell.Index == 9) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellLastDate = Row.getCellFromKey("LastDate")
                var currLastDate = cellLastDate.getValue()

                if (currProjectEmployee != null) {
                    var win = window.open("frmProjectExistEmployee.aspx?ID=" + currProjectEmployee + "&LastDate=" + currLastDate, "_NEW", "height=" + 400 + ",width=" + 800 + ",resizable=1,menubar=0,toolbar=0,location=0,directories=0,scrollbars=1,status=0,center=0");
                    var timer = setInterval(function () {
                        if (win.closed) {
                            clearInterval(timer);
                            location.reload();
                        }
                    }, 1000);

                }
                return false;
            }
            else if (cell.Index == 10) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellLastDate = Row.getCellFromKey("LastDate")
                var currLastDate = cellLastDate.getValue()

                if (currProjectEmployee != null) {
                    var win = window.open("frmProjectNewEmployee.aspx?ID=" + currProjectEmployee + "&LastDate=" + currLastDate, "_NEW", "height=" + 600 + ",width=" + 800 + ",resizable=1,menubar=0,toolbar=0,location=0,directories=0,scrollbars=1,status=0,center=0");
                    var timer = setInterval(function () {
                        if (win.closed) {
                            clearInterval(timer);
                            location.reload();
                        }
                    }, 1000);

                }
                return false;
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjecRecruitment" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none">
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 18px">
                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                Text="الكود" meta:resourcekey="lblCodeResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
                                                meta:resourcekey="lblProjectCodeResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                Width="80px" meta:resourcekey="lblNameResource1"></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"
                                                meta:resourcekey="lblProjectNameResource1"></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Enabled="true" Text="قائمة الوظائف المتاحة" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="3">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10px;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblLocation" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                    Width="80px" meta:resourcekey="lblLocationResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="ddlLocationResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblSubLocation" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع الفرعى"
                                                                    Width="80px" meta:resourcekey="lblSubLocationResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlSubLocation" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="ddlSubLocationResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; text-align: center" colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgLocationPositions" runat="server" EnableAppStyling="True"
                                                    Height="200px" SkinID="Default" Width="100%" EnableTheming="True" meta:resourcekey="uwgLocationPositionsResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors" RowHeightDefault="18px"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                        TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes"
                                                        CellClickActionDefault="Edit">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                                            Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents ClickCellButtonHandler="uwg_ClickCellButtonHandler" />
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PlacementCode" Width="15%" Key="PlacementCode"
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource2">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="كود الوظيفة">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeIn" Key="TimeIn" Width="80px" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="من">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeOut" Key="TimeOut" Width="80px" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="الى">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Position" Width="25%" Key="Position" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource5">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المهنة">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalAmt" Key="InternalAmt" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource6">
                                                                    <Header Caption="سقف الراتب">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WeekDays" Key="WeekDays" Width="10%" DataType="System.Int32"
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource7">
                                                                    <Header Caption="أيام العمل">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LastDate" Key="LastDate" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource8">
                                                                    <Header Caption="تاريخ التفعيل">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="40px" meta:resourcekey="UltraGridColumnResource9">
                                                                    <Header Caption="الدوام">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/cal_date_picker.gif" Height="20px"
                                                                        HorizontalAlign="Center" Width="23px">
                                                                    </CellButtonStyle>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="40px" meta:resourcekey="UltraGridColumnResource10">
                                                                    <Header Caption="حالى">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/cal_date_picker.gif" Height="20px"
                                                                        HorizontalAlign="Center" Width="23px">
                                                                    </CellButtonStyle>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Hidden="true" Type="Button" Width="40px"
                                                                    meta:resourcekey="UltraGridColumnResource11">
                                                                    <Header Caption="جديد">
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Header>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/cal_date_picker.gif" Height="20px"
                                                                        HorizontalAlign="Center" Width="23px">
                                                                    </CellButtonStyle>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LocationDetailID" Hidden="True" Key="LocationDetailID"
                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource12">
                                                                    <Header Caption="LocationDetailID">
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
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
