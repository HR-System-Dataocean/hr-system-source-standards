<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMaxCodeInfo.aspx.vb"
    Inherits="Interfaces_frmMaxCodeInfo" Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~MaxCodeInfo</title>
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
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
   
</head>
<body style="width: 798px; height: 500px" onload='adjustHeight()' >
    <form id="frmMaxCodeInfo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
    <%--<div style="width: 799px; height: 141px">--%>
       
        
         <div class="Div_MasterContainer" runat="server" id="DIV1" style="height: 100%;">
         <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 10px;"></td>
                                                                <td style="width: 10%;">
                                                                   <asp:Label ID="lblPrefix" runat="server" meta:resourcekey="lblPrefixResource1" SkinID="Label_DefaultNormal"
                                                                Text="ÇáÊÑÞíã ÍÓÈ"></asp:Label>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                   <asp:DropDownList ID="ddlprefix" runat="server" AutoPostBack="True" meta:resourcekey="ddlOvertimeResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                
                                                                <asp:ListItem meta:resourcekey="ListItemResource6" Text="ÇáÝÑÚ" Value="0"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource7" Text="ÇáÅÏÇÑÉ" Value="1"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource8" Text="ÇáæÙíÝÉ" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 10%;">
                                                                   <%-- <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                        SkinID="Label_DefaultNormal" Text="Department" Width="80px"></asp:Label>--%>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                    <%--<asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                        SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                                    </asp:DropDownList>--%>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                  <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                        Text="Branch" Width="80px"></asp:Label>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 20px;"></td>
                                                                <td style="width: 10%;">
                                                                  
                                                                </td>
                                                                <td style="width: 39%;">
                                                                   
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>

                                                             <tr>
                                                                <td></td>
                                                                <td>
                                                                  <asp:Label ID="lblEmp" runat="server" SkinID="Label_DefaultNormal" Text="Employee"
                                                                        Width="80px" meta:resourcekey="lblEmpResource1"></asp:Label>
                                                                </td>
                                                               <td>
                                                                    <asp:textbox ID="TxtCode" ReadOnly="true" Enabled="true" runat="server" >
                                                                    </asp:textbox>
                                                                   <asp:textbox ID="TxtName" ReadOnly="true" Enabled="true" runat="server" SkinID="TextBox_LargeBoldC">
                                                                    </asp:textbox>
                                                                </td>
                                                                <td>
                                                                     
                                                                </td>
                                                                <td>
                                                                    
                                                                </td>
                                                               <td>
                                                                  
                                                                </td>

                                                                <td></td>
                                                            </tr>


              
                                                    <tr>
                                                                <td></td>
                                                                <td>
                                                                     <asp:Label ID="lblAccepts" runat="server" meta:resourcekey="Label_Title2Resource2" SkinID="Label_DefaultBold"
                                                                   Text="---------------------"></asp:Label>
                                                           </td>
                                                                <td style="width: 39%;">
                                                                  <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title2Resource2" SkinID="Label_DefaultBold"
                                                                   Text=" ----------------------------------------------"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px;"></td>
                                                                <td style="width: 10%;">
                                                                  
                                                                </td>
                                                                <td style="width: 39%;">
                                                                   
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>
                                        
             <tr>
                                                                <td></td>
                                                                <td>
                                                                   <asp:Label ID="lblSearch" runat="server" SkinID="Label_DefaultNormal" Text="Search"
                                                                        Width="80px" meta:resourcekey="lblSearchResource1"></asp:Label>
                                                                </td>
                                                               <td>
                                                                   <asp:textbox ID="txtSearch"  Enabled="true" runat="server" >
                                                                    </asp:textbox>
                                                                    
                                                                </td>
                                                                <td style="width: 20px;">
                                                                    <igtxt:WebImageButton ID="btnSearch" runat="server" Height="5px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnSearch"
                                                    Overflow="NoWordWrap" Text="Search" UseBrowserDefaults="False" Width="60px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="~/Common/Images/DocumentWF/ToolBox/Srch.png" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                 
                                                                </td>
                                                                <td style="width: 39%;">
                                                                   
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>

                                                <tr>
                                            <td style="height: 10px; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid   Browser="UpLevel"   autopostback="true" ID="UwgSearchEmployees" runat="server" EnableAppStyling="True"
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
                                                                           
                                                                             
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code"
                                                                                meta:resourcekey="UltraGridColumnCode" Width="20%">
                                                                                <Header Caption="ßæÏ ÇáãæÙÝ">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                             <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EmpName" Key="EmpName"
                                                                                meta:resourcekey="UltraGridColumnEmpName" Width="70%">
                                                                                <Header Caption="ÇÓã ÇáãæÙÝ">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
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

                              

                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>

         </table>
          
           
            
            
            
            
            
        </div>
    <%--</div>--%>
    </form>
</body>
</html>
