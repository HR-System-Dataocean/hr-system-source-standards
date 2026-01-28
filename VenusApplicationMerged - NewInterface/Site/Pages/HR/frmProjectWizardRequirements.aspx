<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectWizardRequirements.aspx.vb"
    Inherits="frmProjectWizardRequirements" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"  %>

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
    <title>* Venus Payroll * ~ frmProjectWizardRequirements</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
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
        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("txtCode");
            if (CheckID == false) {
                var page = pageurl;
                var $dialog = $('<div></div>')
               .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
               .dialog({
                   autoOpen: false,
                   modal: true,
                   height: height,
                   width: width
               });
                ODialoge = $dialog;
                OSender = SenderCtrl;
                $dialog.dialog('open');
            }
            else {
                var page = pageurl;
                var $dialog = $('<div></div>')
               .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
               .dialog({
                   autoOpen: false,
                   modal: true,
                   height: height,
                   width: width
               });
                ODialoge = $dialog;
                OSender = SenderCtrl;
                $dialog.dialog('open');
            }
        }
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
                    OpenModal1('frmProjectPlacementPlanning.aspx?LocationDetailID=' + currDetailID + '&PlacementsID=' + currProjectEmployee, hight, width, true, '');
                }
            }
            else if (cell.Index == 9) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                var cellLastDate = Row.getCellFromKey("LastDate")
                var currLastDate = cellLastDate.getValue()

                if (currProjectEmployee != null) {
                    OpenModal1('frmProjectNewEmployee.aspx?ID=' + currProjectEmployee + '&LastDate=' + currLastDate, 450, 900, true, '');
                }
                return false;
            }
        }

    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmProjectWizardRequirements" runat="server" defaultbutton="ImageButton1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ddlbranches.ClientID%>').change(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ddllocation.ClientID%>').change(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ddlproject.ClientID%>').change(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton1.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
        function CloseIt() {
            var $dialog = ODialoge;
            $dialog.dialog('close');
            __doPostBack('<%= LinkButton1.ClientID  %>', '');
        }
    </script>
    <div style="display: none">
        <asp:LinkButton ID="LinkButton1" runat="server" meta:resourcekey="LinkButton1Resource1" 
            >LinkButton</asp:LinkButton>
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
                                        <td style="display: none">
                                            <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" 
                                                CommandArgument="N" meta:resourcekey="ImageButton1Resource1"  />
                                        </td>
                                        <td style="width: 40px; text-align: center;">
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="width: 40px; text-align: center;">
                                            <igtxt:WebImageButton ID="btnPrint" runat="server" Height="18px" Overflow="NoWordWrap"
                                                Style="cursor: pointer;" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnPrintResource1" 
                                                >
                                                <Alignments TextImage="ImageBottom" />
                                                <Appearance>
                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" />
                                                </Appearance>
                                            </igtxt:WebImageButton>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="width: 40px; text-align: center;">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; height: 42px; vertical-align: top">
                                    <tr>
                                        <td style="width: 32px; vertical-align: top">
                                            <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" 
                                                ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" meta:resourcekey="Image_LogoResource1" 
                                                 />
                                        </td>
                                        <td style="width: 50%; vertical-align: middle">
                                            <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1" 
                                                >متابعة المواقع الوظيفية المتاحة فى كل المشاريع</asp:Label>
                                        </td>
                                        <td style="width: 50%; vertical-align: middle">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1" >
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1" >
                                <ContentTemplate>
                                    <div>
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="3">
                                                    <div>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="برجاء إختر الفرع"
                                                                        Width="80px" meta:resourcekey="lblProjectResource1" ></asp:Label>
                                                                </td>
                                                                <td style="width: 34%;">
                                                                    <asp:DropDownList ID="ddlbranches" runat="server" AutoPostBack="True" 
                                                                        SkinID="DropDownList_LargNormal" meta:resourcekey="ddlbranchesResource1" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblSupervisor" runat="server" SkinID="Label_DefaultNormal" 
                                                                        Text="الى تاريخ" meta:resourcekey="lblSupervisorResource1" ></asp:Label>
                                                                </td>
                                                                <td style="width: 34%;">
                                                                    <igtxt:WebMaskEdit ID="txtFromDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix"
                                                                        Enabled="False" meta:resourcekey="txtFromDateResource1" >
                                                                    </igtxt:WebMaskEdit>
                                                                </td>
                                                                <td style="width: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblProject0" runat="server" SkinID="Label_DefaultNormal" 
                                                                        Text="برجاء إختيار الموقع" meta:resourcekey="lblProject0Resource1" ></asp:Label>
                                                                </td>
                                                                <td style="width: 34%;">
                                                                    <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="True" 
                                                                        SkinID="DropDownList_LargNormal" meta:resourcekey="ddllocationResource1" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 34%;">
                                                                </td>
                                                                <td style="width: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:Label ID="lblProject1" runat="server" SkinID="Label_DefaultNormal" 
                                                                        Text="برجاء إختيار المشروع" meta:resourcekey="lblProject1Resource1" ></asp:Label>
                                                                </td>
                                                                <td style="width: 34%;">
                                                                    <asp:DropDownList ID="ddlproject" runat="server" AutoPostBack="True" 
                                                                        SkinID="DropDownList_LargNormal" meta:resourcekey="ddlprojectResource1" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 34%;">
                                                                </td>
                                                                <td style="width: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 34%;">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Overflow="NoWordWrap"
                                                                        Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                                        Text="عرض" UseBrowserDefaults="False" Width="80px" meta:resourcekey="btnFindResource1" 
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
                                                                <td style="width: 34%;">
                                                                </td>
                                                                <td style="width: 5px;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top;" colspan="3">
                                                    <asp:Label ID="Label_Title2" runat="server" Text="قائمة بالمشاريع المتواجد بها مواقع وظيفية شاغرة"
                                                        SkinID="Label_DefaultBold" meta:resourcekey="Label_Title2Resource1" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%; vertical-align: top">
                                                </td>
                                                <td style="width: 96%; vertical-align: top">
                                                    <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgLocationPositions" runat="server" EnableAppStyling="True"
                                                        Height="350px" SkinID="Default" Width="100%" EnableTheming="True" meta:resourcekey="uwgLocationPositionsResource1" 
                                                        >
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                            HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors" RowHeightDefault="18px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes"
                                                            CellClickActionDefault="Edit">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="350px"
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
                                                                    <igtbl:UltraGridColumn BaseColumnName="PlacementCode" Width="15%" Key="PlacementCode"
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
                                                                    <igtbl:UltraGridColumn BaseColumnName="Position" Width="25%" Key="Position" 
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
                                                                    <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="100px" meta:resourcekey="UltraGridColumnResource10" 
                                                                        >
                                                                        <Header Caption="الموظف الجديد">
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
                                                                    <igtbl:UltraGridColumn BaseColumnName="LocationDetailID" Hidden="True" Key="LocationDetailID"
                                                                        Width="0px" meta:resourcekey="UltraGridColumnResource11" >
                                                                        <Header Caption="LocationDetailID">
                                                                            <RowLayoutColumnInfo OriginX="10" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="10" />
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
                                    </div>
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
