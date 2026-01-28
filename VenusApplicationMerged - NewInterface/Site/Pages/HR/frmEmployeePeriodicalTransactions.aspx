<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeePeriodicalTransactions.aspx.vb"
    Inherits="frmEmployeePeriodicalTransactions" Culture="auto" UICulture="auto"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Periodical Transactions</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <%--<script language="javascript" type="text/javascript" src="Scripts/App_ContractsTransactions.js"></script>--%>
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

        function UwgSearchEmployees_ClickCellButtonHandler(gridName, cellId) {
          
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var Row = igtbl_getActiveRow(gridName);
         
            var msg;
            if (window.document.all.item("lblLage").innerText == "0")
                msg = 'Are you sure ?'
            else
                msg = 'هل أنت متأكد ؟'
           

           
            
            if (confirm(msg) == false) {
                cellId.preventDefault();
                return false;
                
            } 
        }


        var IsEdit = true;
        function uwg_AfterCellUpdateHandler(gridName, cellId) {
            if (IsEdit == true) {
                IsEdit = false;
                var IsAllowAdd = window.document.all.item("lblAllowAdd").value;
                var count = igtbl_getGridById(gridName).Rows.length - 1;
                var FirstRow = igtbl_getRowById(gridName + "_r_0");
                var ActiveRow = igtbl_getActiveRow(gridName);
                var ActiveCell = ActiveRow.getCellFromKey("TransactionName");
                if (ActiveCell.Id == cellId) {
                    var ActiveTrasnId = ActiveCell.getValue();
                    ActiveCell.setValue("");

                    var msg;
                    if (window.document.all.item("lblLage").innerText == "0")
                        msg = 'This transaction type previously entrance';
                    else
                        msg = 'هذا البند موجود سابقا';

                    var iRow;
                    var iCell;
                    var Index = 0;
                    var IsFind = false;
                    for (var i = 0; Index <= count; i++) {
                        iRow = igtbl_getRowById(gridName + "_r_" + i);

                        if (iRow == null)
                            continue;

                        iCell = iRow.getCellFromKey("TransactionName");

                        if (iCell.getValue() == ActiveTrasnId) {
                            alert(msg);
                            IsFind = true;
                            break;
                        }
                        Index++;
                    }

                    //Add New Row
                    if (IsFind == false) {
                        ActiveCell.setValue(ActiveTrasnId);
                        var rowIndex = ActiveRow.Element.rowIndex;

                        if (rowIndex - 1 == count) {
                            if (IsAllowAdd == "1")
                                igtbl_addNew(gridName, 0, true, false);
                        }
                    }
                }
                IsEdit = true;
            }
        }

        function Validation() {

            
            //-----------------
            //var ActiveRow = igtbl_getActiveRow(gridName);
            //var ActiveCell = ActiveRow.getCellFromKey("TransactionName");
            //console.log(ActiveCell)
            //return false;
            //---------------
        }

    </script>
  
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeePeriodicalTransactions1" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource2"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="realnameResource2"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="txtId" runat="server" />
        <asp:HiddenField ID="lblLage" runat="server" />
        <asp:HiddenField ID="lblAllowAdd" runat="server" />
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1"   runat="server" DisplayModeFormat="dd/MM/yyyy"
            EditModeFormat="dd/MM/yyyy" meta:resourcekey="WebDateTimeEdit1Resource1">
        </igtxt:WebDateTimeEdit>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" meta:resourcekey="WebAsyncRefreshPanel1Resource1">
                    </igmisc:WebAsyncRefreshPanel>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource2">
                                <ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 18px" colspan="3">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;" Height="18px" 
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSaveResource2">
                                                                <ClientSideEvents Click="btnSave_ClientClick" />
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 10px">
                                                            <asp:Label ID="Label_TSP3" runat="server" Text="|" meta:resourcekey="Label_TSP3Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <igtxt:WebImageButton ID="btnGetDefault" runat="server" Overflow="NoWordWrap" Style="cursor: pointer;"
                                                                UseBrowserDefaults="False" ToolTip="Get Default Transactions" meta:resourcekey="btnGetDefaultResource2">
                                                                <Appearance>
                                                                    <Image Url="./img/forum_newmsg.gif" />
                                                                    <ButtonStyle Font-Size="X-Small">
                                                                    </ButtonStyle>
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 15px">
                                                            <asp:Label ID="Label1" runat="server" Text="|" meta:resourcekey="Label1Resource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebImageButton ID="btnPrint" runat="server" Overflow="NoWordWrap" Style="cursor: pointer;"
                                                                UseBrowserDefaults="False" ToolTip="Print" meta:resourcekey="btnPrintResource1">
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" />
                                                                    <ButtonStyle Font-Size="X-Small">
                                                                    </ButtonStyle>
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
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
                                             <asp:Label ID="LblRegistereBy" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                 Text="Code" meta:resourcekey="LblRegistereByResource2"></asp:Label>
                                         </td>
                                         <td>
                                             <asp:Label ID="LblRegistereByValue" runat="server" Width="200px" SkinID="Label_CopyRightsNormal"
                                                 meta:resourcekey="lLblRegistereByValueResource2"></asp:Label>
                                         </td>
                                         <td>
                                             <asp:Label ID="LblLastUpdate" runat="server" SkinID="Label_CopyRightsBold" Text="Name"
                                                 Width="80px" meta:resourcekey="LblLastUpdateResource2"></asp:Label>
                                         </td>
                                         <td style="width: 40%;">
                                             <asp:Label ID="LblLastUpdateValue" runat="server" Width="250px" SkinID="Label_CopyRightsNormal"
                                                 meta:resourcekey="lblDescEnglishNameResource2"></asp:Label>
                                         </td>
                                         <td style="width: 60%">
                                     <%--        <asp:Label ID="Label6" runat="server" Width="100%" SkinID="Label_WarningBold"
                                                 meta:resourcekey="lblNoBasicSalaryResource2"></asp:Label>--%>
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
                                                                Text="Code" meta:resourcekey="lblUserResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDescEmployeeCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEmployeeCodeResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="Name"
                                                                Width="80px" meta:resourcekey="lblNameResource2"></asp:Label>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:Label ID="lblDescEnglishName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEnglishNameResource2"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%">
                                                            <asp:Label ID="lblNoBasicSalary" runat="server" Width="100%" SkinID="Label_WarningBold"
                                                                meta:resourcekey="lblNoBasicSalaryResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 3px">
                                            </td>
                                            <td>
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom; height: 24px; width: 200px">
                                                            <asp:Label ID="Label213" runat="server" SkinID="Label_DefaultBold" Text="Current contract transaction"
                                                                meta:resourcekey="Label213Resource2"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: bottom; height: 24px; width: 200px">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_TAddtions" runat="server" SkinID="Label_WarningBold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_TAddtionsAmt" runat="server" SkinID="Label_DefaultNormal"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_TDeductions" runat="server" SkinID="Label_WarningBold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_TDeductionsAmt" runat="server" SkinID="Label_DefaultNormal"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_Total" runat="server" SkinID="Label_WarningBold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label_TotalAmt" runat="server" SkinID="Label_DefaultNormal"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 3px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 3px">
                                            </td>
                                            <td>
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgContractsTransactoions" runat="server" Height="100%"
                                                    SkinID="Default" Width="100%" EnableAppStyling="True">
                                                    <DisplayLayout AllowDeleteDefault="Yes" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" Name="uwgForNationality" 
                                                        RowHeightDefault="18px" TableLayout="Fixed" Version="4.00" 
                                                        AllowAddNewDefault="Yes">
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
                                                        <ClientSideEvents ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler"
                                                            AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" />
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource1">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Width="0px" Hidden="True" meta:resourcekey="UltraGridColumnResource15">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TransactionCode" Hidden="True" Key="TransactionCode"
                                                                    meta:resourcekey="UltraGridColumnResource16">
                                                                    <Header Caption="Transaction code">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TransactionTypeID" Key="TransactionName" Width="30%"
                                                                    Type="DropDownList" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource21">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Transaction name">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowNull="False" BaseColumnName="Amount" DataType="System.Double"
                                                                    Format="###,###,###.##" Key="Amount" Width="10%" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource23">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Amount">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Active" Hidden="True" Key="Status" Width="10%"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource32">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="RegUser" Hidden="True" Key="RegUser" Width="0px"
                                                                    meta:resourcekey="UltraGridColumnResource33">
                                                                    <Header Caption="RegDate">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="RegDate" Hidden="True" Key="RegDate" Width="0px"
                                                                    meta:resourcekey="UltraGridColumnResource34">
                                                                    <Header Caption="RegUser">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="CancelDate" Key="CancelDate" Hidden="True"
                                                                    meta:resourcekey="UltraGridColumnResource35">
                                                                    <Header Caption="CancelDate">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Remarks" Key="Remarks" Width="0px" Hidden="True"
                                                                    meta:resourcekey="UltraGridColumnResource36">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ContractId" DataType="System.UInt64" Hidden="True"
                                                                    Key="ContractId" Width="0px" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource37">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Active" Key="Active" Width="10%" Type="DropDownList"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource39">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Active">
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IntervalID" Key="IntervalID" Width="10%" Type="DropDownList"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource41">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Interval">
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PaidAtVacation" Key="PaidAtVacation" Width="15%"
                                                                    Type="DropDownList" AllowUpdate="Yes" DataType="System.UInt64" meta:resourcekey="UltraGridColumnResource42">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Paid at vacation">
                                                                        <RowLayoutColumnInfo OriginX="12" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="12" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OnceAtPeriod" Key="OnceAtPeriod" Width="13%"
                                                                    Type="DropDownList" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource43">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Once at period">
                                                                        <RowLayoutColumnInfo OriginX="13" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="13" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ActiveDate" Key="ActiveDate" Width="12%" Type="Custom"
                                                                    EditorControlID="WebDateTimeEdit1" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource44">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Activate date">
                                                                        <RowLayoutColumnInfo OriginX="14" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="14" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ActiveDate_D" Hidden="True" Key="ActiveDate_D"
                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource45">
                                                                    <Header Caption="Remarks">
                                                                        <RowLayoutColumnInfo OriginX="15" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="15" />
                                                                    </Footer>
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
                                                            </Columns>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 3px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 3px">
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
