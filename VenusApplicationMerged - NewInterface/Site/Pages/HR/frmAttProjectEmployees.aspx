<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAttProjectEmployees.aspx.vb"
    Inherits="frmAttProjectEmployees" Culture="auto" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmAttProjectEmployees</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
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
        }
        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                Sender.value = retvalue;
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
    <script type="text/javascript" id="igClientScript">
        function UltraWebTree1_NodeChecked(treeId, nodeId, bChecked) {
            var selectedNode = igtree_getNodeById(nodeId);
            var childNodes = selectedNode.getChildNodes();
            if (bChecked) {
                for (n in childNodes) {
                    childNodes[n].setChecked(true);
                }
            }
            else {
                for (n in childNodes) {
                    childNodes[n].setChecked(false);
                }
            }
        }

        function uwgEmployeesprojectTransfer_ClickCellButtonHandler(gridName, cellId) {

            var ultraTab = igtab_getTabById("UltraWebTab1");
            var cell = igtbl_getCellById(cellId);
            var Row = igtbl_getActiveRow(gridName);
            var AttProjectID = Row.getCellFromKey("AttProjectID").getValue();
            var mode = window.location.search.split('&')[0];
            
            
            



            if (cell.Index== 5) {

                var hight = window.screen.availHeight - 35;
                var width = window.screen.availWidth - 10;
                var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=AttProjectID&preview=1&ReportCode=EmployeesTransfer&sq0=''&v=" + AttProjectID, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
                win.focus();
                return false;
            }

            if (cell.Index ==6) {
                if (confirm("هل انت متأكد من حذف الموظف من المشروع؟")) {
                    
                }   else{return false}
                
            }
            


        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 15px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmAttProjectEmployees" runat="server">
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
                                        <td style="width: 40px; text-align: center;">
                                            
                                                               
                                                                    <asp:ImageButton ID="ImageButton_Save" Width="30px" Height="30px" runat="server"
                                    CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1" ImageUrl="~/Pages/HR/Img/transfer.png" />
                                            
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Label ID="Label7" runat="server" Text="|"></asp:Label>
                                        </td>
                                        <td style="width: 40px; text-align: center;">
                                            <igtxt:WebImageButton ID="btnPrint" runat="server" Height="18px" Overflow="NoWordWrap"
                                                Style="cursor: pointer;" UseBrowserDefaults="False" Width="24px">
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
                                            <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" />
                                        </td>
                                        <td style="width: 50%; vertical-align: middle">
                                            <asp:Label ID="Label_Header" runat="server">تنقلات الموظفين بين المشاريع</asp:Label>
                                        </td>
                                        <td style="width: 50%; vertical-align: middle">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default">
                        <Tabs>
                            <igtab:Tab Text="عام">
                                <ContentTemplate>
                                    <div>
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="3">
                                                    <div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="3">
                                                    <div>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 10px;">&nbsp;</td>
                                                                <td style="width: 5%;"><asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" Text="كود الموظف" Width="80px" meta:resourcekey="lblCodeResource"></asp:Label></td>
                                                                <td style="width: 25%;">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCode" runat="server" AutoPostBack="True" MaxLength="30"  SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 25px;">
                                                                                <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px" meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                                    <Alignments TextImage="ImageBottom" />
                                                                                    <Appearance>
                                                                                        <Image Url="./Img/forum_search.gif" />
                                                                                    </Appearance>
                                                                                </igtxt:WebImageButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td style="width: 25%;">
                                                                    
                                                               
                                                                    &nbsp;</td>
                                                                <td style="width: 5%;">&nbsp;</td>
                                                                <td style="width: 30%;">&nbsp;</td>
                                                                <td style="width: 5px;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;"></td>
                                                                <td style="width: 5%;">
                                                                    <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="إختر المشروع" Width="80px" Height="16px" meta:resourcekey="lblProjectResource"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    <asp:Label ID="lblTrnsDate" runat="server" SkinID="Label_DefaultNormal" Text="إختر التاريخ" Width="80px"  meta:resourcekey="lblTrnsDateResource"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                    <igtxt:WebMaskEdit ID="txtTrnsDate" runat="server" AutoPostBack="True" InputMask="##/##/####" meta:resourcekey="txtIssueDateResource1" SkinID="WebMaskEdit_Fix">
                                                                    </igtxt:WebMaskEdit>
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="إختر المشروع" Width="80px"  meta:resourcekey="lblProjectResource"></asp:Label>
                                                                    &nbsp;</td>
                                                                <td style="width: 25%;">
                                                                    <asp:DropDownList ID="ddlProjectTransfer" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;">&nbsp;</td>
                                                                <td style="width: 5%;">&nbsp;</td>
                                                                <td style="width: 25%; vertical-align: top;">
                                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgEmployeesProject" runat="server" EnableTheming="True" meta:resourcekey="uwgForNationalityResource1" SkinID="Default" Width="325px">
                                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="None" AllowDeleteDefault="No" AllowRowNumberingDefault="Continuous" AllowSortingDefault="Yes" AllowUpdateDefault="No" AutoGenerateColumns="False" BorderCollapseDefault="Separate" HeaderClickActionDefault="Select" LoadOnDemand="Automatic" Name="uwgForNationality" RowHeightDefault="18px" SelectTypeRowDefault="None" StationaryMargins="No" StationaryMarginsOutlookGroupBy="False" TableLayout="Auto" Version="4.00" ViewType="Flat">
                                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="325px">
                                                                            </FrameStyle>
                                                                            <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler" ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
                                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                            </EditCellStyleDefault>
                                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </FooterStyleDefault>
                                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </HeaderStyleDefault>
                                                                            <RowSelectorStyleDefault Font-Names="Arial" Font-Size="7pt" Width="40px">
                                                                            </RowSelectorStyleDefault>
                                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" Width="200px">
                                                                                    <Padding Left="2px" />
                                                                                </FilterDropDownStyle>
                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                </FilterHighlightRowStyle>
                                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                                    <Padding Left="2px" />
                                                                                </FilterOperandDropDownStyle>
                                                                            </FilterOptionsDefault>
                                                                        </DisplayLayout>
                                                                        <Bands>
                                                                            <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                                </AddNewRow>
                                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                    <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                                        <Padding Left="2px" />
                                                                                    </FilterDropDownStyle>
                                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                    </FilterHighlightRowStyle>
                                                                                </FilterOptions>
                                                                                <Columns>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                        <Header Caption="Payroll Id">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource2" Type="CheckBox" Width="23px">
                                                                                        <Header Caption="√">
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" meta:resourcekey="UltraGridColumnResource3" Width="90px">
                                                                                        <Header Caption="Code">
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Header>
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FullName" Key="FullName" meta:resourcekey="UltraGridColumnResource4" Width="100%">
                                                                                        <Header Caption="Employee Name">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                </Columns>
                                                                            </igtbl:UltraGridBand>
                                                                        </Bands>
                                                                    </igtbl:UltraWebGrid>
                                                                </td>
                                                                <td style=" vertical-align: top; width: 5%;">
                                                                    <asp:Label ID="lblReason" runat="server" SkinID="Label_DefaultNormal" Text="سبب التنقل" Width="80px" meta:resourcekey="lblReasonResource"></asp:Label>
                                                                </td>
                                                                <td style=" vertical-align: top; width: 25%;">
                                                                    <asp:TextBox ID="txtCompanyConditions" runat="server" Height="60px" MaxLength="8000" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%;">&nbsp;</td>
                                                                <td style="width: 25%; vertical-align: top">
                                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEmployeesprojectTransfer" runat="server" EnableTheming="True" meta:resourcekey="uwgForNationalityResource1" SkinID="Default" Width="325px">
                                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="None" AllowDeleteDefault="No" AllowRowNumberingDefault="Continuous" AllowSortingDefault="Yes" AllowUpdateDefault="No" AutoGenerateColumns="False" BorderCollapseDefault="Separate" HeaderClickActionDefault="Select" LoadOnDemand="Automatic" Name="uwgForNationality" RowHeightDefault="18px" SelectTypeRowDefault="None" StationaryMargins="No" StationaryMarginsOutlookGroupBy="False" TableLayout="Auto" Version="4.00" ViewType="Flat">
                                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="325px">
                                                                            </FrameStyle>
                                                                           
                                                                            
                                                                            <ClientSideEvents AfterCellUpdateHandler="uwgEmployeesprojectTransfer_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="uwgEmployeesprojectTransfer_ClickCellButtonHandler" />
                                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                            </EditCellStyleDefault>
                                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </FooterStyleDefault>
                                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </HeaderStyleDefault>
                                                                            <RowSelectorStyleDefault Font-Names="Arial" Font-Size="7pt" Width="40px">
                                                                            </RowSelectorStyleDefault>
                                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" Width="200px">
                                                                                    <Padding Left="2px" />
                                                                                </FilterDropDownStyle>
                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                </FilterHighlightRowStyle>
                                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                                    <Padding Left="2px" />
                                                                                </FilterOperandDropDownStyle>
                                                                            </FilterOptionsDefault>
                                                                        </DisplayLayout>
                                                                        <Bands>
                                                                            <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                                </AddNewRow>
                                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                    <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                                        <Padding Left="2px" />
                                                                                    </FilterDropDownStyle>
                                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                    </FilterHighlightRowStyle>
                                                                                </FilterOptions>
                                                                                <Columns>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                        <Header Caption="Payroll Id">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource2" Type="CheckBox" Width="23px">
                                                                                        <Header Caption="√">
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" meta:resourcekey="UltraGridColumnResource3" Width="90px">
                                                                                        <Header Caption="Code">
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Header>
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FullName" Key="FullName" meta:resourcekey="UltraGridColumnResource4" Width="100%">
                                                                                        <Header Caption="Employee Name">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="PreviousProject" Key="PreviousProject" meta:resourcekey="UltraGridPreviousProject" Width="100%">
                                                                                        <Header Caption="Previous Project">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn Type="Button" Width="20px" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                                        meta:resourcekey="UltraGridColumnResource16">
                                                                                        <Header Caption="">
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Header>
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Footer>
                                                                                        <CellButtonStyle BackgroundImage="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" BorderStyle="None"
                                                                                            Cursor="Hand" Height="12px" Width="13px">
                                                                                        </CellButtonStyle>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn Key="btnDel" Width="20px" Type="Button" CellButtonDisplay="Always"
                                                                                        meta:resourcekey="UltraGridColumnResource46">
                                                                                        <CellButtonStyle BorderStyle="None" Cursor="Hand" BackgroundImage="./Img/logoff_small.gif"
                                                                                            HorizontalAlign="Center" Height="15px" VerticalAlign="Middle" Width="15px">
                                                                                        </CellButtonStyle>
                                                                                        <Header Caption="">
                                                                                            <RowLayoutColumnInfo OriginX="16" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="16" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>

                                                                                     <igtbl:UltraGridColumn BaseColumnName="AttProjectID" Hidden="True" Key="AttProjectID" meta:resourcekey="UltraGridColumnResource1">
                                                                                        <Header Caption="Payroll Id">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>

                                                                                     <igtbl:UltraGridColumn BaseColumnName="PreviousProjectID" Hidden="True" Key="PreviousProjectID" meta:resourcekey="UltraGridColumnResource1">
                                                                                        <Header Caption="PreviousProjectID">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                </Columns>
                                                                            </igtbl:UltraGridBand>
                                                                        </Bands>
                                                                    </igtbl:UltraWebGrid>
                                                                </td>
                                                                <td style="width: 5px;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    &nbsp;</td>
                                                                <td style="width: 25%;">
                                                                    &nbsp;</td>
                                                                <td style="width: 5%;">
                                                                    &nbsp;</td>
                                                                <td style="width: 25%;">
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    &nbsp;</td>
                                                                <td style="width: 25%;">
                                                                    &nbsp;</td>
                                                                <td style="width: 5px;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%; vertical-align: top">
                                                </td>
                                                <td style="width: 96%; vertical-align: top">
                                                    &nbsp;</td>
                                                <td style="width: 2%; vertical-align: top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%; vertical-align: top">
                                                </td>
                                                <td style="width: 96%; vertical-align: top">
                                                    &nbsp;</td>
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
