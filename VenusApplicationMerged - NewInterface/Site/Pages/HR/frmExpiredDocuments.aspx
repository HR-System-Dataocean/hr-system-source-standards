<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmExpiredDocuments.aspx.vb"
    Inherits="frmExpiredDocuments" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Expired Documents</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
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

        function UwgSearchEmployees_ClickCellButtonHandler(gridName, cellId) {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var Row = igtbl_getActiveRow(gridName);
            var TestDepend = Row.getCellFromKey("DependantName").getValue();
            var DocNo = Row.getCellFromKey("DocumentNumber").getValue();
            var TableName;
            var intID;

            if (TestDepend == null) {
                TableName = "hrs_Employees";
                intID = Row.getCellFromKey("EmpID").getValue();
            }
            else {
                TableName = "hrs_EmployeesDependants";
                intID = Row.getCellFromKey("DepenID").getValue();
            }
            if (intID != null) {

                var winopen = window.open("frmEmployeesDocuments.aspx?TB=" + TableName + "&SV=" + intID + "&DN=" + DocNo, "_blank", "height=450,width=700,left=0,top=0,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
                winopen.moveTo(0, 0);
                winopen.focus();
            }
        }

        function OpenEXDOCReport(lang, v) {

            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            if (lang == "0")
                var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmpCode|ExpireFromDate|ExpireToDate|DeptCode|BranchCode|FilterType|Lang|DocumentTypeID&preview=1&ReportCode=ExpiredDocuments&sq0=''&v=" + v, "_blank", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            else
                var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=true&Criteria=EmpCode|ExpireFromDate|ExpireToDate|DeptCode|BranchCode|FilterType|Lang|DocumentTypeID&preview=1&ReportCode=ExpiredDocuments&sq0=''&v=" + v, "_blank", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.moveTo(0, 0);
            win.focus();
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
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmExpiredDocuments" runat="server" defaultbutton="ImageButton1">
        <script type="text/javascript" id="Script1">
            $(function () {
                $('#<%= ImageButton_Delete.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Delete.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton1.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
        </script>
        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
            </asp:ScriptManager>
            <igtxt:WebMaskEdit meta:resourcekey="wdcCIssuDateRecourcekey" ID="wdcCIssuDate" runat="server"
                Height="16px" HorizontalAlign="Left" InputMask="##/##/####" TabIndex="4" Width="155px">
            </igtxt:WebMaskEdit>
            <asp:HiddenField ID="hdnLang" runat="server" Value="0" />

            <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                meta:resourcekey="ImageButton1Resource1" />
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
                                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                            <tr>
                                                <td style="width: 20px">
                                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                                        CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" ImageUrl="~/Common/Images/ToolBox/Print.png" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:LinkButton ID="LinkButton_Print" runat="server" Text="Print" CommandArgument="Print"
                                                        meta:resourcekey="LinkButton_PrintResource1" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                                                </td>

                                                <td style="width: 20px">
    <asp:ImageButton ID="ImageButton_Excel" Width="16px" Height="16px" runat="server"
        CommandArgument="Export Excel" meta:resourcekey="ImageButton_ExcelResource1" ImageUrl="~/Common/Images/Pages.png" />
</td>
<td style="width: 100px">
    <asp:LinkButton ID="LinkButton_Excel" runat="server" Text="Export Excel" CommandArgument="Excel"
        meta:resourcekey="LinkButton_ExcelResource1" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
</td>
                                             
                                                <td style="width: 20px">
                                                    <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                                        CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" ImageUrl="~/Pages/HR/Img/logoff_small.gif" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:LinkButton ID="LinkButton_Delete" runat="server" Text="Delete" CommandArgument="Delete"
                                                        meta:resourcekey="LinkButton_DeleteResource1" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                                                </td>
                                                <td style="width: 10px;">&nbsp;
                                                </td>
                                                <td style="width: 100px">&nbsp;
                                                </td>
                                                <td style="width: 20px">&nbsp;
                                                </td>
                                                <td style="width: 100px">&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 80px">
                                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"
                                                        Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                                                </td>
                                                <td style="width: 40px"></td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
                <td class="SeparArea"></td>
                <td class="LabelArea">
                   
                </td>
                <td class="DataArea">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                
                            </td>
                            <td style="width: 25px;">
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;
    </td>
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
                <td class="SeparArea"></td>
                <td class="LabelArea">
                    <asp:Label ID="lblDocumentTypesGroup" runat="server" SkinID="Label_DefaultNormal" Text="Document Types Group"
                        Width="90px" meta:resourcekey="lblDocumentTypesGroupResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:DropDownList ID="ddlDocumentTypesGroup" runat="server" SkinID="DropDownList_LargNormal" AutoPostBack="true"
                        meta:resourcekey="ddlDocumentTypesGroupTypeResource1">
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
                                                            <td class="SeparArea"></td>
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
                                                <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lbl1" runat="server" SkinID="Label_DefaultNormal" Text="Document Type"
                                                                    Width="90px" meta:resourcekey="lbl1Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlDocumentType" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="ddlDocumentTypeResource1">
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
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Department" Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                    SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                    Text="Branch" Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1"
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
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="From Date"
                                                                    Width="90px" meta:resourcekey="Label1Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igsch:WebDateChooser ID="wdcFromDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                    Width="100%" meta:resourcekey="wdcFromDateResource1">
                                                                </igsch:WebDateChooser>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" Text="To Date"
                                                                    Width="90px" meta:resourcekey="Label2Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igsch:WebDateChooser ID="wdcToDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                    Width="100%" meta:resourcekey="wdcToDateResource1">
                                                                </igsch:WebDateChooser>
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
                                                                <asp:Label ID="Label_Title1" runat="server" Text="Please select employees" SkinID="Label_DefaultBold"
                                                                    meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: middle; text-align: center;">&nbsp;&nbsp;
                                                </td>
                                                <td style="width: 47%; vertical-align: middle;">
                                                    <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                        meta:resourcekey="btnFindRes"
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
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: auto" colspan="3">
                                                    <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgExpiredDocuments" runat="server" EnableAppStyling="False"
                                                        Height="100%" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                        Width="100%">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                                ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
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
                                                            <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn Width="23px" AllowUpdate="Yes" Type="CheckBox" Key="Check"
                                                                        meta:resourcekey="UltraGridColumnResource2">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <Header Caption="√">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmpCode" Key="EmpCode" Width="70px" meta:resourcekey="UltraGridColumnResource3">
                                                                        <Header Caption="Code">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EmployeeName" Key="EmployeeName"
                                                                        Width="50%" meta:resourcekey="UltraGridColumnResource4">
                                                                        <Header Caption="Employee Name">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DependantName" Key="DependantName"
                                                                        Width="50%" meta:resourcekey="UltraGridColumnResource5">
                                                                        <Header Caption="Dependant Name">
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DocumentNumber" Key="DocumentNumber"
                                                                        Width="80px" meta:resourcekey="UltraGridColumnResource6">
                                                                        <Header Caption="Doc No.">
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DocumentType" Key="DocumentType"
                                                                        Width="80px" meta:resourcekey="UltraGridColumnResource7">
                                                                        <Header Caption="Doc Type">
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" EditorControlID="wdcCIssuDate" Key="CIssueDate"
                                                                        Type="Custom" Width="80px" meta:resourcekey="UltraGridColumnResource8">
                                                                        <Header Caption="Issue Date">
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="IssueDate" Key="IssueDate" Hidden="True" FooterText="Issue Date"
                                                                        meta:resourcekey="UltraGridColumnResource9">
                                                                        <Header Caption="Payroll Id">
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Header>
                                                                        <Footer Caption="Issue Date">
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="IssueDate_D" Hidden="True" Key="IssueDate_D"
                                                                        meta:resourcekey="UltraGridColumnResource10">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ExpiryDate" Hidden="False" Key="ExpiryDate" Format="dd/MM/yyyy"
    meta:resourcekey="UltraGridColumnExpiryDate">
    <Header>
        <RowLayoutColumnInfo OriginX="9" />
    </Header>
    <Footer>
        <RowLayoutColumnInfo OriginX="9" />
    </Footer>
</igtbl:UltraGridColumn>
                                                                                                                                        <igtbl:UltraGridColumn BaseColumnName="RegDate" Hidden="False" Key="RegDate" Format="dd/MM/yyyy"
    meta:resourcekey="UltraGridColumnRegDate">
    <Header>
        <RowLayoutColumnInfo OriginX="9" />
    </Header>
    <Footer>
        <RowLayoutColumnInfo OriginX="9" />
    </Footer>
</igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="IssueCity" Key="IssueCity"
                                                                        Width="110px" FooterText="Issue City" meta:resourcekey="UltraGridColumnResource11">
                                                                        <Header Caption="Issue City">
                                                                            <RowLayoutColumnInfo OriginX="10" />
                                                                        </Header>
                                                                        <Footer Caption="Issue City">
                                                                            <RowLayoutColumnInfo OriginX="10" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn Type="Button" BaseColumnName="EmpDocs" Hidden="False" Key="EmpDocs" Width="25px" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                        meta:resourcekey="UltraGridColumnResource12">
                                                                        <CellButtonStyle BackgroundImage="~/Pages/HR/Img/i.p.edit.gif" BorderStyle="None"
                                                                            Cursor="Hand" Height="12px" Width="13px">
                                                                        </CellButtonStyle>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <Header Caption="">
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmpID" Hidden="True" Key="EmpID" meta:resourcekey="UltraGridColumnResource13">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="12" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="12" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DepenID" Hidden="True" Key="DepenID" meta:resourcekey="UltraGridColumnResource14">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="13" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="13" />
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
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
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
