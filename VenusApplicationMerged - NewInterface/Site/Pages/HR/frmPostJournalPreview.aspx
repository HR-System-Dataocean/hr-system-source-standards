<%@ page language="VB" autoeventwireup="false" codefile="frmPostJournalPreview.aspx.vb"
    inherits="frmDistributedSalary" culture="auto" meta:resourcekey="PageResource1"
    uiculture="auto" %>

<%@ register tagprefix="igmisc" namespace="Infragistics.WebUI.Misc" assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igsch" namespace="Infragistics.WebUI.WebSchedule" assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtab" namespace="Infragistics.WebUI.UltraWebTab" assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtbar" namespace="Infragistics.WebUI.UltraWebToolbar" assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtxt" namespace="Infragistics.WebUI.WebDataInput" assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ register assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Salary Distribution Details</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        var ODialoge;
        var OSender;
        var IsEdit = true;
        function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var gridLength = grid.Rows.length;
            var cell = igtbl_getCellById(cellId);
            var row = cell.getRow();
            var blSign = cell.getValue();
            var CellToChange;
            if (IsEdit) {
                if (row.Id == gridName + "_r_0") {
                    IsEdit = false;
                    for (i = 0; i < gridLength; i++) {
                        var currRow = grid.Rows.rows[i];
                        var currRow = igtbl_getCellById(gridName + "_r" + i);
                        CellToChange = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                        CellToChange.setValue(blSign)
                    }
                }
                else {
                    IsEdit = false;
                    CellToChange = igtbl_getCellById(gridName + "_rc_0_1");
                    CellToChange.setValue(false)
                }
                IsEdit = true;
            }
        }

        function ddlDepartment_Change() {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var ddlDepartment = igtab_getElementById("ddlDepartment", ultraTab.element);
            PageMethods.GetRelatedDepartment(ddlDepartment.value, OnSucceeded, OnFailed);
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'GetRelatedDepartment') {
                var ultraTab = igtab_getTabById("UltraWebTab1");
                var ddlBranch = igtab_getElementById("ddlBranche", ultraTab.element);
                ddlBranch.outerHTML = result;
            }
        }

        function OnFailed(error) {
            alert();
        }

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
    <style type="text/css">
        .style1 {
            height: 21px;
        }
        .hidden {
            display: none;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmDistributedSalary" runat="server">
        <script type="text/javascript" id="Script1">
            $(function () {
            });
        </script>
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
            <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 42px; vertical-align: top">
                            <tr>
                                <td style="width: 32px; vertical-align: top">
                                    <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                        meta:resourcekey="Image_LogoResource1" />
                                </td>
                                <td style="vertical-align: middle">
                                    <asp:Label ID="Label_Header" runat="server" 
                                        Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;" meta:resourcekey="Label_HeaderResource1">شاشة عرض القيود</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Details">
                        <igtab:ultrawebtab id="UltraWebTab1" runat="server" enableappstyling="True" skinid="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <tabs>
                                <igtab:tab text="عام" meta:resourcekey="TabResource1">
                                    <contenttemplate>
                                        <table style="width: 100%; height: 100%; min-height: 600px; vertical-align: top"
                                            cellspacing="0">
                                              <tr>
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                             <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;  vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Text="Employees Code" Width="90px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="30" meta:resourcekey="txtCodeResource1"
                                                                            SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
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
                                                            <asp:Label ID="lblCode1" runat="server" SkinID="Label_DefaultNormal" Text="Fisical Periods"
                                                                Width="90px" meta:resourcekey="lblCode1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlPeriods" Enabled="False" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlPeriodsResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" class="auto-style1">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                SkinID="Label_DefaultNormal" Text="Department" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack ="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top" class="auto-style2">
                                            </td>
                                            <td style="vertical-align: top" class="auto-style1">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                Text="Branch" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1" AutoPostBack="true"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblFilter" runat="server" meta:resourcekey="lblFilterResource1" SkinID="Label_DefaultNormal"
                                                                Text="Prepare Type" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlFilter" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlFilterResource1" AutoPostBack="True">
                                                                <asp:ListItem Value="A" Text="All Transactions" meta:resourcekey="AllDataRes"></asp:ListItem>
                                                                <asp:ListItem Value="N" Text="Salaries" meta:resourcekey="SalariesRes"></asp:ListItem>
                                                                <asp:ListItem Value="V" Text="Vacations" meta:resourcekey="VacationsRes"></asp:ListItem>
                                                                <asp:ListItem Value="E" Text="End Of Services" meta:resourcekey="EOSRes"></asp:ListItem>
                                                                <asp:ListItem Value="L" Text="Loans" meta:resourcekey="LoansRes"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                                &nbsp;&nbsp;
                                            </td>
                                            
                                            <td style="width: 47%; vertical-align: middle;">
                                               <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="LblSector" runat="server" meta:resourcekey="lblSectorResource1" SkinID="Label_DefaultNormal"
                                                                Text="Sector" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlsector" runat="server" meta:resourcekey="ddlsectorResource1"  AutoPostBack="True"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label_Sponsor" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                                SkinID="Label_DefaultNormal" Text="Sponsor" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
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
                                                         
                                                        </td>
                                                        <td class="DataArea">
                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            
                                        </tr>

                                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                               <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnFindRes"
                                                    Overflow="NoWordWrap" Text=" Search " UseBrowserDefaults="False" Width="80px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="./img/forum_search.gif" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                            <td style="width: 47%; vertical-align: middle;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                                        </table>
                                                    </td>
                                                </tr>


                                             <tr>
                                        <td style="width: 100%; height: 16px; vertical-align: top">
                                            <table style="width: 47%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td class="SeparArea" >
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:Label ID="lblDebit" runat="server" Enabled="False" Width="80px" SkinID="Label_DefaultNormal" Text="اجمالي المدين"
                                                            meta:resourcekey="lblDebitResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtDebitCode" runat="server" Enabled="False" SkinID="TextBox_DefaultNormalltr"
                                                            meta:resourcekey="txtDebitResource1"></asp:TextBox>
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:Label ID="lblCredit" runat="server" Width="80px" SkinID="Label_DefaultNormal" Text="اجمالي الدائن"
                                                            meta:resourcekey="lblCreditResource1"></asp:Label>
                                                    </td>
                                                  <td class="DataArea">
                                                        <asp:TextBox ID="txtCredit" runat="server" Enabled="False" SkinID="TextBox_DefaultNormalltr"
                                                            meta:resourcekey="txtCreditResource1"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                </table>
                                            </td>
                                    </tr>

                                            <tr>
                                                <td style="width: 100%; height:100%; vertical-align: auto;">
                                                    <igtbl:UltraWebGrid Browser="UpLevel"  ID="grdProjects" runat="server" AutoGenerateColumns="False"  Height="100%"  Width="100%" >
                                                        
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="NotSet" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                                    <FilterOptionsDefault AllowRowFiltering="OnClient" ApplyOnAdd="True" 
                                                            FilterIcon="True" FilterRowView="Top" FilterUIType="FilterRow">
                                                        </FilterOptionsDefault>




                                                                </DisplayLayout>
                                                        <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="LedgerCode" Key="LedgerCode" Width="7%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="Ledger Code">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ArbDesc" Key="ArbDesc" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="Arb Desc">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="CostCenter1Code" Key="CostCenter1Code" Width="7%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="CostCenter1Code">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="CostCenter1Name" Key="CostCenter1Name" Width="10%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="CostCenter1Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                           <igtbl:UltraGridColumn BaseColumnName="CostCenter2Code" Key="CostCenter2Code" Width="7%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="CostCenter2Code">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="CostCenter2Name" Key="CostCenter2Name" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource6">
                                                                    <Header Caption="CostCenter2Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>

                                                                 <igtbl:UltraGridColumn BaseColumnName="CostCenter3Code" Key="CostCenter3Code" Width="8%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource9">
                                                                    <Header Caption="CostCenter3Code">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="CostCenter3Name" Key="CostCenter3Name" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource10">
                                                                    <Header Caption="CostCenter3Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>

                                                            <igtbl:UltraGridColumn BaseColumnName="Debit" Key="Debit" Width="9%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource7">
                                                                    <Header Caption="Debit">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Credit" Key="Credit" Width="9%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource8">
                                                                    <Header Caption="Credit">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>

                                                       </Columns>

                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
                                            </tr>
                                        </table>
                                    </contenttemplate>
                                </igtab:tab>
                            </tabs>
                        </igtab:ultrawebtab>
                    </td>
                </tr>
            </table>
        </div>
    </form> 
</body>
</html>
