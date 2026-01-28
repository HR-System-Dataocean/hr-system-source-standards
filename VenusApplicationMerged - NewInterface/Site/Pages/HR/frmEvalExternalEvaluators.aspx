<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvalExternalEvaluators.aspx.vb"
    Inherits="frmEvalRondomEvaluators" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
    <title>* Venus Payroll * ~Eval Rondom Evaluators</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script type="text/javascript" id="Infragistics">
        function ShowAlert(strEnglishMsg, strArabicMsg) {
            var language = window.document.getElementById("txtLang").value;
            if (language != "Eng") {
                alert(strArabicMsg)
            }
            else {
                alert(strEnglishMsg)
            }
        }
        function GetControlIDFromTab(controlName, webTab) {
            var control = igtab_getElementById(controlName, webTab.element)
            if (control != null) {
                return control.id
            }
            else {
                control = webTab.findControl(controlName)
                return control.id
            }
        }
        function btnVacationTransaction_Click(oButton, oEvent) {
            var webTab = igtab_getTabById("UltraWebTab1");
            var ddlPosition = window.document.getElementById(GetControlIDFromTab("DdlEmployee", webTab)).value
            if (ddlPosition == 0) {
                ShowAlert("You must enter Employee", "يجب إختيار موظف");
                return
            }
            var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgInterviewsDetail0")
            var Aray = ddlPosition
            for (i = 0; i < grid.Rows.length; i++) {
                row1 = grid.Rows.getRow(i);
                currCodeCell = row1.getCell(3);
                if (currCodeCell.getValue() == true) {
                    Aray = Aray + "|" + row1.getCell(0).getValue()
                }
            }
            window.opener.document.forms[0]["txtReturned1"].value = Aray;
            window.opener.focus();
            window.opener.document.forms[0].submit();
            window.close();
        }
    </script>
    
</head>
<body style="height: 100%; margin: 0; padding: 0;" >
    <form id="frmEvalRondomEvaluators" runat="server">
    <div style="display: none">

    <asp:TextBox ID="txtCode" runat="server" BackColor="Silver" BorderColor="White" meta:resourcekey="txtCodeRecourcekey"
            BorderStyle="Solid" BorderWidth="1px" Height="0px" Style="z-index: 106; left: 168px;
            position: absolute; top: 441px; width: 34px;" Width="0px" MaxLength="50" AutoPostBack="True"></asp:TextBox>
    </div> 
    <asp:HiddenField ID="txtLang" runat="server" Value="Eng" />

    <div class="Div_MasterContainer" runat="server" id="DIV">
    
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    
                                
                                        <table style="width: 100%;  vertical-align: top"
                                        cellspacing="0">
                                            <tr>
                                                <td style="height: 18px">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 5px">
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnVacationTransaction" runat="server" AutoSubmit="False" 
                                                                    Height="18px" meta:resourcekey="btnSaveResource1" Overflow="NoWordWrap" 
                                                                    Style="cursor: pointer;" UseBrowserDefaults="False" Width="24px">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                    </Appearance>
                                                                    <ClientSideEvents Click="btnVacationTransaction_Click" />
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                    Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <asp:ImageButton ID="ImageButton_Print" runat="server" CommandArgument="Print" 
                                                                    Height="16px" meta:resourcekey="ImageButton_PrintResource1" 
                                                                    SkinID="HrPrint_Command" Width="16px" />
                                                            </td>
                                                            <td style="width: 5px">
                                                                &nbsp;</td>
                                                            <td style="width: 40px; text-align: center;">
                                                                &nbsp;</td>
                                                  
                                                            
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            </table>
                                        <table style="width: 100%; height: 100%;vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbl0" runat="server" Width="95px" Text="Evaluator  Name" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="lbl0Resource1"  ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                      
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;</td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            &nbsp;</td>
                                                        <td class="DataArea">
                                                            &nbsp;</td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label5" runat="server" Width="95px" Text="Evaluator Mail" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label5Resource1"  ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtMail" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtMailResource1"></asp:TextBox>
                                                        </td>
                                                      
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;</td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            &nbsp;</td>
                                                        <td class="DataArea">
                                                            &nbsp;</td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label1" runat="server" Width="95px" Text="From Position" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label1Resource1"  ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DDLFromPosition" runat="server" 
                                                                skinID="DropDownList_LargNormal" meta:resourcekey="DDLFromPositionResource1" >
                                                            </asp:DropDownList>
                                                            </td>
                                                      
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultNormal" Text="To Position" 
                                                                Width="95px" meta:resourcekey="Label3Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DDLToPosition" runat="server" 
                                                                skinID="DropDownList_LargNormal" meta:resourcekey="DDLToPositionResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" Width="95px" Text="From Department" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label2Resource1"  ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DDLFromDepartment" runat="server" 
                                                                skinID="DropDownList_LargNormal" meta:resourcekey="DDLFromDepartmentResource1" >
                                                            </asp:DropDownList>
                                                            </td>
                                                      
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="To Department" 
                                                                Width="95px" meta:resourcekey="Label4Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DDLToDepartment" runat="server" 
                                                                skinID="DropDownList_LargNormal" 
                                                                meta:resourcekey="DDLToDepartmentResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Add External Evaluators"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: middle;">
                                                
                                                <igtxt:WebImageButton ID="btnFilter" runat="server" Height="5px" 
                                                    meta:resourcekey="btnFindRes" Overflow="NoWordWrap" 
                                                    style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;color:Black" 
                                                    Text=" Search " UseBrowserDefaults="False" Width="80px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="./img/forum_search.gif" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" 
                                                            ColorRight="160, 160, 160" ColorTop="White" StyleBottom="Solid" 
                                                            StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid" WidthBottom="1px" 
                                                            WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">

                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgInterviewsDetail0" runat="server" EnableAppStyling="False"
                                                                Height="250px" Width="98%" SkinID="Default" 
                                                                meta:resourcekey="uwgInterviewsDetail0Resource1" >
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                    AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px"
                                                                        Width="98%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" HorizontalAlign="Center"
                                                                        Height="20px" VerticalAlign="Middle" Font-Names="tahoma" Font-Size="9pt">
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
                                <igtbl:UltraGridBand meta:resourcekey="ZResourceZUltraGridBandResource1">
                                    <RowEditTemplate>
                                        <p align="right">
                                            <br />
                                        </p>
                                        <br />
                                        <p align="center">
                                        </p>
                                    </RowEditTemplate>
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
                                        <igtbl:UltraGridColumn BaseColumnName="ID"
                                            Hidden="True" Key="ID" Width="0px" AllowResize="Fixed" 
                                            DataType="System.Int32" meta:resourcekey="UltraGridColumnResource1">
                                            <Header Caption="">
                                            </Header>
                                            <CellButtonStyle HorizontalAlign="Left">
                                            </CellButtonStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Code"
                                            Width="100px" meta:resourcekey="UltraGridColumnResource2">
                                            <Header Caption="Code">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Employee_Name"
                                            Key="Employee_Name" Width="100%" AllowResize="Fixed" AllowUpdate="No" 
                                            meta:resourcekey="UltraGridColumnResource3">
                                            <Header Caption="Employee Name">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Apply"
                                            Key="Apply" Width="100px" AllowResize="Fixed" AllowUpdate="Yes" DataType="System.Boolean"
                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource4">
                                            <Header Caption="Is Apply">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                    </RowTemplateStyle>
                                </igtbl:UltraGridBand>
                            </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            

                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
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
