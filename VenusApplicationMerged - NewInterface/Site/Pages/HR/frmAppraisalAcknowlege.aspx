<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraisalAcknowlege.aspx.vb"
    Inherits="frmAppraisalEvaluation" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~FrmAnnual Vacation Request Action</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>

    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var IsEdit = true;
        function recalcAppraisalTotal() {
            try {
                var grid = igtbl_getGridById('uwgEmployeeAppraisal');
                if (!grid) return;
                var rows = grid.getRows ? grid.getRows() : null;
                var len = rows && rows.getLength ? rows.getLength() : 0;
                var total = 0;
                for (var i = 0; i < len; i++) {
                    var r = rows.getRow(i);
                    if (!r) continue;
                    var c = r.getCellFromKey('AppraisalScore');
                    if (!c) continue;
                    var v = c.getValue ? c.getValue() : c.getText();
                    v = parseFloat(v);
                    if (!isNaN(v)) total += v;
                }
                var tb = document.querySelector('.js-total-percent');
                if (tb) tb.value = total;
            } catch (e) { /* swallow */ }
        }
        function uwgEmployeeAppraisal_AfterCellUpdate(gridName, cellId) {
            try {
                var cell = igtbl_getCellById(cellId);
                if (!cell) { recalcAppraisalTotal(); return; }
                var key = cell.Column ? cell.Column.Key : null;
                if (key == 'AppraisalScore') {
                    recalcAppraisalTotal();
                }
            } catch (e) { /* swallow */ }
        }
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
                if (HTotalAbsent == true) {
                    ++TotalAbsent;

                }
                else if (HTotalAbsent == false) {
                    --TotalAbsent;

                }
                igtab_getElementById("lblDescTotalAbsent", ultraTab.element).innerText = TotalAbsent;
            }
        }
        var Row;
        var IsEdit = true;
        function uwg_AfterCellUpdateHandler(gridName, cellId) {
            if (IsEdit == true) {
                var cell = igtbl_getCellById(cellId);
                Row = igtbl_getRowById(cellId);

                var count = igtbl_getGridById(gridName).Rows.length - 1;
                var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

                if (rowIndex == count) {

                    igtbl_addNew(gridName, 0, true, false);

                }
            }
        }

        var cell;
        function uwgEnterCellEdit(gridName, cellId) {
            cell = cellId;

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
        function CloseMe() {
            parent.CloseIt("");
            parent.location.reload();

        }
        // Initial calculation after page renders
        if (window.addEventListener) {
            window.addEventListener('load', recalcAppraisalTotal, false);
        } else if (window.attachEvent) {
            window.attachEvent('onload', recalcAppraisalTotal);
        } else {
            window.onload = recalcAppraisalTotal;
        }
        function ClosWindow() {
            self.CloseMe();
            parent.location.reload();
            window.reload();

        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="FrmAnnualVacationRequestAction" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
      
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%; height:100%">
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="تقييم الموظفين" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top" cellspacing="0">
    <tr>
        <td style="height: 18px">
            <table style="width: 100%; height: 18px; vertical-align: middle; border-bottom: 1px solid silver" cellspacing="0">
                <tr>
                    <td style="width: 5px"></td>
                    <td style="width: 90px; text-align: center;">
                        <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;" Height="18px"
                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSaveResource1">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </td>
                    <td style="width: 50px; text-align: center;">
                        <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1" Text="|"></asp:Label>
                    </td>
                    <td class="spacearea" style="width: 20px"></td>
                    <td class="spacearea" style="width: 20px"></td>
                    <td style="width: 40px; text-align: center;"></td>
                    <td style="width: 5px">&nbsp;</td>
                    <td style="width: 40px; text-align: center;">&nbsp;</td>
                    <td style="text-align: center;">
                        <asp:Label ID="lblMSG" runat="server" meta:resourcekey="lblMSGResource1" SkinID="Label_WarningBold" Width="100%"></asp:Label>
                    </td>
                </tr>
            </table>
            
            <table style="width: 100%; vertical-align: top; border-bottom: 1px solid silver" cellspacing="0">
                <tr>
                    <td style="width: 100%; vertical-align: top">
                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                            <tr>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal" meta:resourcekey="lblCodeResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:TextBox ID="txtEmployee" runat="server" SkinID=" TextBox_DefaultNormalC" MaxLength="30"
                                                    AutoPostBack="True" meta:resourcekey="txtEmployeeResource1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 6%; vertical-align: top"></td>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 120px;   ">
                                                <asp:Label ID="labelDescEnglishName" runat="server" Text="اسم الموظف" SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:TextBox ID="txtDescEnglishName" runat="server" SkinID=" TextBox_DefaultNormalC"
                                                    ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1" TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="Label2" runat="server" Text="الوظيفة" SkinID="Label_DefaultNormal" meta:resourcekey="lblPositionResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:TextBox ID="TxtPosition" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                    ReadOnly="True" MaxLength="255" meta:resourcekey="TxtPositionResource1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 6%; vertical-align: top"></td>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 120px;   ">
                                                <asp:Label ID="Label9" runat="server" Text="القسم" SkinID="Label_DefaultNormal" meta:resourcekey="lblDepartmentResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:TextBox ID="TxtDepartment" runat="server" SkinID=" TextBox_DefaultNormalC" MaxLength="30"
                                                    AutoPostBack="True" meta:resourcekey="TxtDepartmentResource1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblAppraisalType" runat="server" Width="70px" SkinID="Label_LargtNormal"
                                                    Text="AppraisalType" meta:resourcekey="lblAppraisalTypeResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:DropDownList ID="DdlAppraisalType" runat="server" width="500px" AutoPostBack="True" SkinID="DropDownList_DefaultNormal"
                                                    meta:resourcekey="DdlAppraisalTypeResource1" TabIndex="3"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 6%; vertical-align: top"></td>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblLastAppDate" runat="server" Width="120px" SkinID="Label_LargtNormal"
                                                    Text="LastAppDate" meta:resourcekey="lblLastAppDateResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <igtxt:WebMaskEdit ID="txtLastAppDate" runat="server" InputMask="##/##/####" 
                                                    SkinID="WebMaskEdit_Fix" Width="120px"></igtxt:WebMaskEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblFromDate" runat="server" Width="50px" SkinID="Label_LargtNormal"
                                                    Text="FromDate" meta:resourcekey="lblFromDateResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <igtxt:WebMaskEdit ID="txtFromDate" runat="server" InputMask="##/##/####" 
                                                    SkinID="WebMaskEdit_Fix" Width="220px"></igtxt:WebMaskEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 6%; vertical-align: top"></td>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblToDate" runat="server" Width="120px" SkinID="Label_LargtNormal"
                                                    Text="FromDate" meta:resourcekey="lblToDateResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <igtxt:WebMaskEdit ID="txtToDate" runat="server" InputMask="##/##/####" 
                                                    SkinID="WebMaskEdit_Fix" Width="120px"></igtxt:WebMaskEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 47%; vertical-align: top">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="LabelArea" style="min-width: 90px;   ">
                                                <asp:Label ID="lblTotalPercent" runat="server" Text="TotalPercen" meta:resourcekey="lblTotalPercenResource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="  ">
                                                <asp:TextBox ID="txtTotalPercent" runat="server" SkinID=" TextBox_DefaultNormalC" CssClass="js-total-percent"
                                                    ReadOnly="True" MaxLength="255" meta:resourcekey="txtTotalPercenResource1"></asp:TextBox>
                                            </td>
                                            <td class="SeparArea" style="width: 5px"></td>
                                            <td>
                                                 <asp:Label ID="LblAcknowledge" runat="server" SkinID="Label_DefaultNormal" Text="Acknowledge"
     meta:resourcekey="lblAcknowledgeResource1"></asp:Label>

 <asp:CheckBox ID="ChkAcknowledge" runat="server" meta:resourcekey="ChkAcknowledgeResource1"
     AutoPostBack="True" />


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 6%; vertical-align: top"></td>
                                <td style="width: 47%; vertical-align: top"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="height: 300px; vertical-align: top">
                            <igtbl:UltraWebGrid  Browser="UpLevel"  ID="uwgEmployeeAppraisal" runat="server" EnableAppStyling="False"
          Height="100%" meta:resourcekey="uwgEmployeeAppraisalResource1" SkinID="Default"
          Width="99%">
         
          <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
              AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
              CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
              HeaderClickActionDefault="SortMulti" Name="uwgEmployeeAppraisal" RowHeightDefault="15px"
              SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
              TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
              <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                  BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                  Width="99%">
              </FrameStyle>
             <ClientSideEvents AfterCellUpdateHandler="uwgEmployeeAppraisal_AfterCellUpdate"
              ClickCellButtonHandler="uwgEmployeeAppraisal_ClickCellButtonHandler" />
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
              <HeaderStyleDefault BackColor="#e2eefa" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                  Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                  <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
              </HeaderStyleDefault>
              <RowSelectorStyleDefault Font-Size="7pt" Width="40px">
              </RowSelectorStyleDefault>
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
                  <Columns>
                            
                           <igtbl:UltraGridColumn BaseColumnName="AppraisalID" DataType="System.Int32" Hidden="True" 
                               Key="AppraisalID" meta:resourcekey="UltraGridColumnResource1"  >
       
                               <Header Caption="AppraisalID">
                               </Header>
                           </igtbl:UltraGridColumn>
                      <igtbl:UltraGridColumn BaseColumnName="CriteriaGroupID" DataType="System.Int32" Hidden="True" AllowUpdate="No"
    Key="CriteriaGroupID" meta:resourcekey="CriteriaGroupIDResource1"  >
       
    <Header Caption="CriteriaID">
    </Header>
</igtbl:UltraGridColumn>

                      
                                           <igtbl:UltraGridColumn BaseColumnName="CriteriaGroupName" DataType="System.String" AllowUpdate="No"
                                              Key="CriteriaGroupName" meta:resourcekey="CriteriaGroupNameColumnResource4"
                                              Width="25%">
                                              <Header Caption=" CriteriaGroup">
                                                  <RowLayoutColumnInfo OriginX="3" />
                                              </Header>
                                                <CellStyle HorizontalAlign="Center">
                                          </CellStyle>
                                              <Footer>
                                                  <RowLayoutColumnInfo OriginX="3" />
                                              </Footer>
                                          </igtbl:UltraGridColumn>




                         
                       <igtbl:UltraGridColumn BaseColumnName="CriteriaID" DataType="System.Int32" Hidden="True" AllowUpdate="No"
    Key="CriteriaID" meta:resourcekey="CriteriaIDResource1"  >
       
    <Header Caption="CriteriaID">
    </Header>
</igtbl:UltraGridColumn>

                      
                                           <igtbl:UltraGridColumn BaseColumnName="CriteriaName" DataType="System.String" AllowUpdate="No"
                                              Key="CriteriaName" meta:resourcekey="CriteriaNameColumnResource4"
                                              Width="70%">
                                              <Header Caption=" معيار التقييم">
                                                  <RowLayoutColumnInfo OriginX="3" />
                                              </Header>
                                                <CellStyle HorizontalAlign="Center">
                                          </CellStyle>
                                              <Footer>
                                                  <RowLayoutColumnInfo OriginX="3" />
                                              </Footer>
                                          </igtbl:UltraGridColumn>
                                          <igtbl:UltraGridColumn BaseColumnName="MinimumScore" DataType="System.Int32" Hidden="False" AllowUpdate="No"
                                                  Key="MinimumScore" meta:resourcekey="MinimumScoreResource1"  Width="15%" >
     
                                                  <Header Caption="الحد الادني للتقييم">
                                                    <RowLayoutColumnInfo OriginX="3" />

                                                  </Header>
                                                    <CellStyle HorizontalAlign="Center">
                                                     </CellStyle>
                                               <Footer>
                                                   <RowLayoutColumnInfo OriginX="3" />
                                               </Footer>
                                         </igtbl:UltraGridColumn>
                      
                                           
                                      <igtbl:UltraGridColumn BaseColumnName="MaximumScore" DataType="System.Int32" Hidden="False" AllowUpdate="No"
         Key="MaximumScore" meta:resourcekey="MaximumScoreResource1"  Width="15%" >
     
         <Header Caption="الحد الاقصي للتقييم">
           <RowLayoutColumnInfo OriginX="3" />

         </Header>
           <CellStyle HorizontalAlign="Center">
            </CellStyle>
      <Footer>
          <RowLayoutColumnInfo OriginX="3" />
      </Footer>
</igtbl:UltraGridColumn>  

                                     <igtbl:UltraGridColumn BaseColumnName="AppraisalScore" AllowUpdate="No" DataType="System.Int32"
                                           Key="AppraisalScore" meta:resourcekey="AppraisalScoreColumnResource4" 
                                          Width="15%">
                                          <Header Caption=" التقييم الفعلي">
                                              <RowLayoutColumnInfo OriginX="3" />
                                          </Header>
                                            <CellStyle HorizontalAlign="Center">
                                      </CellStyle>
                                          <Footer>
                                              <RowLayoutColumnInfo OriginX="3" />
                                          </Footer>
                                      </igtbl:UltraGridColumn>  
                         <igtbl:UltraGridColumn BaseColumnName="HasObjection" AllowUpdate="Yes" type="CheckBox"
       Key="HasObjection" meta:resourcekey="HasObjectionColumnResource4"
      Width="15%">
      <Header Caption="  Send Objection">
          <RowLayoutColumnInfo OriginX="3" />
      </Header>
        <CellStyle HorizontalAlign="Center">
  </CellStyle>
      <Footer>
          <RowLayoutColumnInfo OriginX="3" />
      </Footer>
  </igtbl:UltraGridColumn>  

                                       <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="ObjectionDetails" DataType="System.String"
                                           Key="ObjectionDetails" meta:resourcekey="ObjectionDetailsColumnResource4"
                                          Width="35%">
                                          <Header Caption=" Objection Details">
                                              <RowLayoutColumnInfo OriginX="3" />
                                          </Header>
                                            <CellStyle HorizontalAlign="Center">
                                      </CellStyle>
                                          <Footer>
                                              <RowLayoutColumnInfo OriginX="3" />
                                          </Footer>
                                      </igtbl:UltraGridColumn>  
                          <igtbl:UltraGridColumn BaseColumnName="NotificationID" DataType="System.Int32" Hidden="True" 
      Key="NotificationID" meta:resourcekey="UltraGridColumnResource1"  >
       
      <Header Caption="NotificationID">
      </Header>
  </igtbl:UltraGridColumn>

                          <igtbl:UltraGridColumn BaseColumnName="App_EmployeeID" DataType="System.Int32" Hidden="True" 
      Key="App_EmployeeID" meta:resourcekey="UltraGridColumnResource1"  >
       
      <Header Caption="App_EmployeeID">
      </Header>
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

            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="width: 47%; vertical-align: top">
                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                            <tr>
                                <td class="SeparArea" style="width: 5px"></td>
                                <td class="LabelArea" style="min-width: 90px;    vertical-align: top;">
                                    <asp:Label ID="lblWeaknessPoints" runat="server" Text="WeaknessPoints" SkinID="Label_DefaultNormal" meta:resourcekey="lblWeaknessPointsResource1"></asp:Label>
                                </td>
                                <td class="DataArea" style="  ">
                                    <asp:TextBox ID="txtWeaknessPoints" runat="server" Width="350px" Height="50px" MaxLength="8000"
                                        TextMode="MultiLine" meta:resourcekey="txtWeaknessPointsResource1" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 6%; vertical-align: top"></td>
                    <td style="width: 47%; vertical-align: top">
                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                            <tr>
                                <td class="SeparArea" style="width: 5px"></td>
                                <td class="LabelArea" style="min-width: 90px;    vertical-align: top;">
                                    <asp:Label ID="lblStrengthPoints" runat="server" Text="StrengthPoints" SkinID="Label_DefaultNormal" meta:resourcekey="lblStrengthPointsResource1"></asp:Label>
                                </td>
                                <td class="DataArea" style="  ">
                                    <asp:TextBox ID="txtStrengthPoints" runat="server" Width="350px" Height="50px" MaxLength="8000"
                                        TextMode="MultiLine" meta:resourcekey="txtStrengthPointsResource1" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="width: 100%; vertical-align: top">
                        <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                            <tr>
                                <td style="vertical-align: bottom;   ">
                                    <asp:Label ID="lblImprovemen" runat="server" meta:resourcekey="LblImprovementResource2" SkinID="Label_DefaultBold"
                                        Text="Improvemen"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="height: 100px; vertical-align: top">
                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                            <tr>
                                <td>
                                                  <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" EnableAppStyling="False"
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
                        <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwgEnterCellEdit" />
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
                        <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1"
                            AllowAdd="Yes">
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                            </FilterOptions>
                            <Columns>
                               
                             <igtbl:UltraGridColumn BaseColumnName="GroupName" DataType="System.String"
    Key="GroupName" meta:resourcekey="GroupColumnResource4"
    Width="20%">
    <Header Caption=" المجموعة">
        <RowLayoutColumnInfo OriginX="3" />
    </Header>
      <CellStyle HorizontalAlign="Center">
</CellStyle>
    <Footer>
        <RowLayoutColumnInfo OriginX="3" />
    </Footer>
</igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="ImproveName" DataType="System.String"
    Key="ImproveName" meta:resourcekey="ImprovementNameColumnResource4"
    Width="20%">
    <Header Caption=" ImproveName">
        <RowLayoutColumnInfo OriginX="3" />
    </Header>
      <CellStyle HorizontalAlign="Center">
</CellStyle>
    <Footer>
        <RowLayoutColumnInfo OriginX="3" />
    </Footer>
</igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="Remarks" DataType="System.String"
    Key="Remarks" meta:resourcekey="RemarksColumnResource4"
    Width="20%">
    <Header Caption=" Remarks">
        <RowLayoutColumnInfo OriginX="3" />
    </Header>
      <CellStyle HorizontalAlign="Center">
</CellStyle>
    <Footer>
        <RowLayoutColumnInfo OriginX="3" />
    </Footer>
</igtbl:UltraGridColumn>
                            </Columns>
                        </igtbl:UltraGridBand>
                    </Bands>
                </igtbl:UltraWebGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
    </td>
     
</tr>


 

 
                        
        </table>
    </td>
    <td style="width: 6%; height: 1px; vertical-align: top">
    </td>
    <td style="width: 47%; height: 1px; vertical-align: top">
      
    </td>
</tr>

                                        
                                                </table>
                                            </td>
                                                       
                                                    </tr>
                                                </table>
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
