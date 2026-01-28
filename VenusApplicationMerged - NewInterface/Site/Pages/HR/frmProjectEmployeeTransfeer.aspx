<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectEmployeeTransfeer.aspx.vb"
    Inherits="frmProjectEmployeeTransfeer" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"  %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmProjectEmployeeTransfeer</title>
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
                    var win = window.open("frmProjectPlacementPlanning.aspx?LocationDetailID=" + currDetailID + "&PlacementsID=" + currProjectEmployee, "_blank", "height=" + 600 + ",width=" + 1000 + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=1");
                    return false;
                }
            }
        }
        function CloseMe() {
            parent.CloseIt();
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectEmployeeTransfeer" runat="server" defaultbutton="ImageButton1">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" meta:resourcekey="nameResource1" ></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" meta:resourcekey="realnameResource1" ></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1" ></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" meta:resourcekey="TargetControlResource1" ></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1" ></asp:Label>
        <asp:Button ID="Button_Confirm" runat="server" Text="Button" meta:resourcekey="Button_ConfirmResource1" 
             />
        <asp:HiddenField ID="HiddenField_SPlacement" runat="server" />
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 18px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" 
                                    CommandArgument="N" meta:resourcekey="ImageButton1Resource1"  />
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 10px;">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1"  />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;" meta:resourcekey="LinkButton_HelpResource1" 
                                    ></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1" >
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1" >
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                                <tr>
                                                                    <td style="width: 5px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                                            Text="الكود" meta:resourcekey="lblCodeResource1" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblProjectCode" runat="server" Width="80px" 
                                                                            SkinID="Label_CopyRightsNormal" 
                                                                            meta:resourcekey="lblProjectCodeResource1" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                                            Width="80px" meta:resourcekey="lblNameResource1" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%;">
                                                                        <asp:Label ID="lblProjectName" runat="server" Width="100%" 
                                                                            SkinID="Label_CopyRightsNormal" 
                                                                            meta:resourcekey="lblProjectNameResource1" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 60%">
                                                                        <asp:Label ID="Msg" runat="server" SkinID="Label_WarningBold" meta:resourcekey="MsgResource1" 
                                                                            ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_StartDate" runat="server" Text="من تاريخ" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label_StartDateResource1" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtStartDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix"
                                                                AutoPostBack="True" meta:resourcekey="txtStartDateResource1" >
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td style="width: 90px;">
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                            &nbsp;
                                                        </td>
                                                        <td class="SeparArea">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                                <asp:LinkButton ID="LinkButton_Copy" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إيقاف من المشروع الحالى فقط فى هذا التاريخ" meta:resourcekey="LinkButton_CopyResource1" 
                                                    ></asp:LinkButton>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label1" runat="server" Text="التحويل الى" 
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label1Resource1" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                                <asp:LinkButton ID="LinkButton_Copy0" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إلغاء الموظف من المشروع الحالى" meta:resourcekey="LinkButton_Copy0Resource1" 
                                                    ></asp:LinkButton>
                                            </td>
                                            <td style="vertical-align: top" class="style2">
                                                &nbsp;
                                            </td>
                                            <td style="vertical-align: top" class="style1">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="إختر المشروع"
                                                                Width="90px" meta:resourcekey="lblProjectResource1" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlProject" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True" meta:resourcekey="ddlProjectResource1" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="SeparArea">
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
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                            &nbsp;
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
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblLocations" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                Width="90px" meta:resourcekey="lblLocationsResource1" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlLocation" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True" meta:resourcekey="ddlLocationResource1" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" colspan="3">
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
                                                                <igtbl:UltraGridColumn BaseColumnName="PlacementCode" Width="20%" Key="PlacementCode"
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource2" >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeIn" Key="TimeIn" Width="80px" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource3" >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeOut" Key="TimeOut" Width="80px" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource4" >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="Position" Width="30%" Key="Position" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource5" >
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
                                                                    DataType="System.Decimal" Format="##.##" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource6" 
                                                                    >
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
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource7" >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="LastDate" Key="LastDate" Width="15%" 
                                                                    AllowUpdate="No" meta:resourcekey="UltraGridColumnResource8" >
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
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="40px" meta:resourcekey="UltraGridColumnResource9" 
                                                                    >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="LocationDetailID" Hidden="True" Key="LocationDetailID"
                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource10" >
                                                                    <Header Caption="LocationDetailID">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
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
