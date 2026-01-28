<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectExistEmployee.aspx.vb"
    Inherits="frmProjectExistEmployee" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"  %>

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
    <title>* Venus Payroll * ~ frmProjectExistEmployee</title>
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
            if (cell.Index == 6) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellDetailID = Row.getCellFromKey("LocationDetailID")
                var currDetailID = cellDetailID.getValue()

                if (currProjectEmployee != null) {
                    var win = window.open("frmProjectPlacementPlanning.aspx?LocationDetailID=" + currDetailID + "&PlacementsID=" + currProjectEmployee, "_Parent", "height=" + 600 + ",width=" + 1000 + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=1");
                    return false;
                }
            }
            else if (cell.Index == 8) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellLastDate = Row.getCellFromKey("LastDate")
                var currLastDate = cellLastDate.getValue()

                if (currProjectEmployee != null) {
                    var win = window.open("frmProjectNewEmployee.aspx?ID=" + currProjectEmployee + "&LastDate=" + currLastDate, "_Parent", "height=" + 600 + ",width=" + 800 + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=1");
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
    <style type="text/css">
        .style1
        {
            width: 10px;
            height: 24px;
        }
        .style2
        {
            width: 10%;
            height: 24px;
        }
        .style3
        {
            width: 39%;
            height: 24px;
        }
        .style4
        {
            height: 24px;
        }
        .style5
        {
            width: 5px;
            height: 24px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectExistEmployee" runat="server" defaultbutton="ImageButton2">
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
                                        <td style="display: none">
                                            <asp:ImageButton ID="ImageButton2" Width="0px" Height="0px" runat="server" meta:resourcekey="ImageButton2Resource1" 
                                                 />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                Text="كود الوظيفة" meta:resourcekey="lblCodeResource1" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" 
                                                SkinID="Label_CopyRightsNormal" 
                                                meta:resourcekey="lblProjectCodeResource1" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="تاريخ التفعيل"
                                                Width="80px" meta:resourcekey="lblNameResource1" ></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" 
                                                SkinID="Label_CopyRightsNormal" 
                                                meta:resourcekey="lblProjectNameResource1" ></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1" >
                        <Tabs>
                            <igtab:Tab Enabled="true" 
                                Text="قائمة الموظفين المتفرغين للعمل فى التواريخ المحددة" meta:resourcekey="TabResource1" 
                                >
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="3">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="style2">
                                                                <asp:Label ID="lblLocation" runat="server" SkinID="Label_DefaultNormal" Text="الجنسية"
                                                                    Width="80px" meta:resourcekey="lblLocationResource1" ></asp:Label>
                                                            </td>
                                                            <td class="style3">
                                                                <asp:DropDownList ID="ddlnationality" runat="server" AutoPostBack="True" 
                                                                    SkinID="DropDownList_LargNormal" 
                                                                    meta:resourcekey="ddlnationalityResource1" >
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="style4">
                                                            </td>
                                                            <td class="style2">
                                                                <asp:CheckBox ID="CheckBox_RelatedPosition" runat="server" AutoPostBack="True" meta:resourcekey="CheckBox_RelatedPositionResource1" 
                                                                     />
                                                            </td>
                                                            <td class="style3">
                                                                <asp:Label ID="Label_IsHijri" runat="server" SkinID="Label_DefaultNormal" 
                                                                    Text="الإلتزام بالمهنة المحددة فى التعاقد" meta:resourcekey="Label_IsHijriResource1" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="style2">
                                                                <asp:Label ID="lblLocation0" runat="server" SkinID="Label_DefaultNormal" Text="الرقم الوظيفى"
                                                                    Width="80px" meta:resourcekey="lblLocation0Resource1" ></asp:Label>
                                                            </td>
                                                            <td class="style3">
                                                                <asp:TextBox ID="txtCode" runat="server" MaxLength="30" 
                                                                    SkinID="TextBox_LargeNormalC" meta:resourcekey="txtCodeResource1" ></asp:TextBox>
                                                            </td>
                                                            <td class="style4">
                                                            </td>
                                                            <td class="style2">
                                                                <asp:CheckBox ID="CheckBox_IsOld" runat="server" AutoPostBack="True" meta:resourcekey="CheckBox_IsOldResource1" 
                                                                     />
                                                            </td>
                                                            <td class="style3">
                                                                <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" 
                                                                    Text="إظهار قائمة الموظفين السابقين قبل التعديل" meta:resourcekey="Label2Resource1" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td class="style5">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10px;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 10%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 39%;">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 10%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Overflow="NoWordWrap"
                                                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                                    Text=" بحث " UseBrowserDefaults="False" Width="80px" meta:resourcekey="btnFindResource1" 
                                                                    >
                                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                                    <Appearance>
                                                                        <Image Url="./img/forum_search.gif" />
                                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px;">
                                                                &nbsp;
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
                                                <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgLocationPositions" runat="server" EnableAppStyling="True"
                                                    Height="200px" SkinID="Default" Width="100%" EnableTheming="True" meta:resourcekey="uwgLocationPositionsResource1" 
                                                    >
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
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1" >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource1" 
                                                                    >
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Code" Width="15%" Key="Code" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource2" >
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="الرقم الوظيفى">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EmployeeName" Width="45%" Key="EmployeeName"
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource3" >
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="إسم الموظف">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Position" Width="20%" Key="Position" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource4" >
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="الوظيفة الحالية">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Salary" Key="Salary" Width="20%" DataType="System.Decimal"
                                                                    Format="##.##" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource5" 
                                                                    >
                                                                    <Header Caption="الراتب الحالى">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="80px" meta:resourcekey="UltraGridColumnResource6" 
                                                                    >
                                                                    <Header Caption="إضافة">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Table.bmp" Height="20px" HorizontalAlign="Center"
                                                                        Width="23px">
                                                                    </CellButtonStyle>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
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
