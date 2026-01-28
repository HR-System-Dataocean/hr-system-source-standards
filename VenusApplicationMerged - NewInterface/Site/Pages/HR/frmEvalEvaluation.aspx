<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvalEvaluation.aspx.vb"
    Inherits="Interfaces_frmEvalEvaluation" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ &#1615;Evaluation Recommendations</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="App_JScript.js"></script>
    <script language="javascript" src="App_JScript_M.js"></script>
    <script language="javascript" src="App_JScript_PayRoll.js"></script>
    <script language="javascript" src="App_OtherFields_JScript.js"></script>
    <script language="javascript" src="App_Search_JScript.js"></script>
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebTab.css " rel="Stylesheet"
        type="text/css" />
    <style type="text/css">
.igwgFrameBlue2k7
{
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
	background-color: Transparent; 
	border: solid 1px #000000;
}
.igwgHdrBlue2k7
{
	    border-style: none;
            border-color: inherit;
            border-width: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
	        font-size: 11px;
	        background-color:#D4D7DB;
	background-image: url('../ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/igwgHeader.jpg');
	        background-repeat: repeat-x;
	        height: 23px;
	        font-weight: normal;
	
	
	
	        cursor: hand;
}

.igwgRowBlue2k7
{
	border-top: 1px solid white;
	border-bottom: 1px solid #E3EFFF;
	font-size: 11px;;
		
}
.igwgRowAltBlue2k7
{
	border-top: 1px solid #F9F9F9;
	border-bottom: 1px solid #E3EFFF;	
	background-color: #f9f9f9;
	font-size: 11px;
}

    </style>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <div id="Div" runat="server" style="margin: auto; width: 950px; padding: 0px 10px;
        height: 100%;">
        <form id="frmEvalEvaluation" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <igtxt:WebTextEdit ID="WebTextEdit1" runat="server">
        </igtxt:WebTextEdit>
        <asp:HiddenField ID="HiddenField_EvaluationID" runat="server" />
        <asp:HiddenField ID="HiddenField_EvalTypeID" runat="server" />
        <table id="Table1" align="center" runat="server" cellpadding="0" cellspacing="0"
            style="width: 100%;">
            <tr>
                <td runat="server" style="width: 100%; direction: ltr; vertical-align: top; text-align: center;">
                    <table runat="server" cellpadding="0" cellspacing="0" style="width: 900px; background-color: #F8F8FF;">
                        <tr>
                            <td style="direction: ltr; vertical-align: middle; text-align: right; height: 22px"
                                colspan="3">
                                &nbsp; &nbsp;</t&nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="direction: ltr; vertical-align: middle; text-align: right; height: 22px"
                                colspan="3">
                                <asp:Button ID="Button_Reload" runat="server" Text="Reload" Width="100px" Visible="False" />
                                &nbsp;
                                <asp:Button ID="Button_Save" runat="server" Text="Save" Width="100px" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="direction: ltr; vertical-align: middle; text-align: center; height: 22px"
                                colspan="3">
                                <asp:Label ID="Label_Evaluationtype" runat="server" Text="Label" ForeColor="#CC3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                height: 22px">
                                <asp:Label ID="Label1" runat="server" Text="Who Is Evaluate"></asp:Label>
                            </td>
                            <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>
                            </td>
                            <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                <asp:Label ID="Label_Dest_EmployeeID" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                height: 22px">
                                <asp:Label ID="Label4" runat="server" Text="Evaluate Whom"></asp:Label>
                            </td>
                            <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                <asp:Label ID="Label5" runat="server" Text=":"></asp:Label>
                            </td>
                            <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                <asp:Label ID="Label_Target_EmployeeID" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                height: 22px">
                                <asp:Label ID="Label7" runat="server" Text="English Notes"></asp:Label>
                            </td>
                            <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                <asp:Label ID="Label8" runat="server" Text=":"></asp:Label>
                            </td>
                            <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                <asp:TextBox ID="TextBox_EnNotes" runat="server" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                height: 22px">
                                <asp:Label ID="Label3" runat="server" Text="Arabic Notes"></asp:Label>
                            </td>
                            <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                            </td>
                            <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                <asp:TextBox ID="TextBox_ArNotes" runat="server" Width="550px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="direction: ltr; vertical-align: middle; text-align: center; height: 10px"
                                colspan="3">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td runat="server" style="width: 100%; direction: ltr; vertical-align: top; text-align: center;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" BorderColor="#949C9C" BorderStyle="Solid"
                                BorderWidth="1px" Style="text-align: left" ThreeDEffect="False" Width="900px" SelectedTab="1">
                                <Tabs>
                                    <igtab:Tab Text="Recommendation">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList_Recomm" runat="server" AutoPostBack="True" Width="800px">
                                            </asp:DropDownList>
                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPointsRecomm" runat="server" Height="100px"
                                                ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/" meta:resourcekey="ZResourceZuwgGradesTransactionsResource1"
                                                Style="height: 242px;" TabIndex="9" Width="850px">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" HeaderText="ID" Hidden="True" Key="ID"
                                                                Width="0px">
                                                                <Header Caption="ID">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn HeaderText="Transaction Type" BaseColumnName="PointsEnglish"
                                                                Key="PointsEnglish" Width="400px">
                                                                <Header Caption="Recommendation English" Title="">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="PointsArabic" Width="400px" EditorControlID="WebTextEdit1"
                                                                Type="Custom">
                                                                <Header Caption="Recommendation Arabic " Title="">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                        </RowTemplateStyle>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                    BorderCollapseDefault="Separate" CellClickActionDefault="CellSelect"  
                                                    GridLinesDefault="NotSet" HeaderClickActionDefault="SortMulti" Name="uwgQuestionAnswers"
                                                    RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeCellDefault="Single"
                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                    <FilterOptionsDefault DropDownRowCount="15">
                                                        <FilterDropDownStyle CssClass="igwgFltrDrpDwnBlue2k7">
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle CssClass="igwgFltrRowHiLtBlue2k7">
                                                        </FilterHighlightRowStyle>
                                                    </FilterOptionsDefault>
                                                    <FrameStyle BorderStyle="None" CssClass="igwgFrameBlue2k7" Height="100px" Width="850px">
                                                    </FrameStyle>
                                                    <Images ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/">
                                                    </Images>
                                                    <RowAlternateStyleDefault CssClass="igwgRowAltBlue2k7">
                                                    </RowAlternateStyleDefault>
                                                    <Pager>
                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </PagerStyle>
                                                    </Pager>
                                                    <EditCellStyleDefault CssClass="igwgCellEdtBlue2k7">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault CssClass="igwgFooterBlue2k7">
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault CssClass="igwgHdrBlue2k7" CustomRules="font-weight:normal;" Height="23px">
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault CssClass="igwgRowSlctrBlue2k7">
                                                    </RowSelectorStyleDefault>
                                                    <SelectedHeaderStyleDefault CssClass="igwgHdrSelBlue2k7">
                                                    </SelectedHeaderStyleDefault>
                                                    <RowStyleDefault CssClass="igwgRowBlue2k7">
                                                        <Padding Left="3px" />
                                                        <BorderDetails WidthBottom="1px" WidthTop="1px" />
                                                    </RowStyleDefault>
                                                    <GroupByRowStyleDefault CssClass="igwgGrpRowBlue2k7">
                                                    </GroupByRowStyleDefault>
                                                    <SelectedGroupByRowStyleDefault CssClass="igwgGrpRowSelBlue2k7">
                                                    </SelectedGroupByRowStyleDefault>
                                                    <SelectedRowStyleDefault CssClass="igwgRowSelBlue2k7">
                                                    </SelectedRowStyleDefault>
                                                    <GroupByBox Hidden="True">
                                                        <BandLabelStyle CssClass="igwgGrpBoxBandLblBlue2k7">
                                                        </BandLabelStyle>
                                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                        </BoxStyle>
                                                    </GroupByBox>
                                                    <AddNewBox Location="Top" Prompt="">
                                                        <ButtonStyle CssClass="igwgAddNewBtnBlue2k7">
                                                        </ButtonStyle>
                                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </BoxStyle>
                                                    </AddNewBox>
                                                    <ActivationObject BorderColor="181, 196, 223" BorderWidth="">
                                                        <BorderDetails WidthLeft="0px" WidthRight="0px" />
                                                    </ActivationObject>
                                                    <RowExpAreaStyleDefault CssClass="igwgRowExpBlue2k7">
                                                    </RowExpAreaStyleDefault>
                                                    <FixedCellStyleDefault CssClass="igwgCellFxdBlue2k7">
                                                    </FixedCellStyleDefault>
                                                    <FixedHeaderStyleDefault CssClass="igwgHdrFxdBlue2k7">
                                                    </FixedHeaderStyleDefault>
                                                    <FixedFooterStyleDefault CssClass="igwgFtrFxdBlue2k7">
                                                    </FixedFooterStyleDefault>
                                                    <AddNewRowDefault Visible="Yes">
                                                    </AddNewRowDefault>
                                                    <FormulaErrorStyleDefault CssClass="igwgFormulaErrBlue2k7">
                                                    </FormulaErrorStyleDefault>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </ContentTemplate>
                                    </igtab:Tab>
                                    <igtab:TabSeparator>
                                    </igtab:TabSeparator>
                                    <igtab:Tab Text="Strength Points">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList_SPoints" runat="server" AutoPostBack="True" Width="800px">
                                            </asp:DropDownList>
                                            <igtbl:UltraWebGrid  Browser="UpLevel"  ID="uwgSPoints" runat="server"  Height="100px" ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/"
                                                meta:resourcekey="ZResourceZuwgGradesTransactionsResource1" Style="height: 242px;"
                                                TabIndex="9" Width="850px">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" HeaderText="ID" Hidden="True" Key="ID"
                                                                Width="0px">
                                                                <Header Caption="ID">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="PointsEnglish" HeaderText="Transaction Type"
                                                                Key="PointsEnglish" Width="400px">
                                                                <Header Caption="Strength Points English" Title="">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="PointsArabic" EditorControlID="WebTextEdit1"
                                                                Type="Custom" Width="400px">
                                                                <Header Caption="Strength Points Arabic " Title="">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                        </RowTemplateStyle>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                    BorderCollapseDefault="Separate" CellClickActionDefault="CellSelect"  
                                                    GridLinesDefault="NotSet" HeaderClickActionDefault="SortMulti" Name="uwgQuestionAnswers"
                                                    RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeCellDefault="Single"
                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                    <FilterOptionsDefault DropDownRowCount="15">
                                                        <FilterDropDownStyle CssClass="igwgFltrDrpDwnBlue2k7">
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle CssClass="igwgFltrRowHiLtBlue2k7">
                                                        </FilterHighlightRowStyle>
                                                    </FilterOptionsDefault>
                                                    <Pager>
                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </PagerStyle>
                                                    </Pager>
                                                    <AddNewBox Location="Top" Prompt="">
                                                        <ButtonStyle CssClass="igwgAddNewBtnBlue2k7">
                                                        </ButtonStyle>
                                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </BoxStyle>
                                                    </AddNewBox>
                                                    <GroupByBox Hidden="True">
                                                        <BandLabelStyle CssClass="igwgGrpBoxBandLblBlue2k7">
                                                        </BandLabelStyle>
                                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                        </BoxStyle>
                                                    </GroupByBox>
                                                    <FrameStyle BorderStyle="None" CssClass="igwgFrameBlue2k7" Height="100px" Width="850px">
                                                    </FrameStyle>
                                                    <Images ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/">
                                                    </Images>
                                                    <RowAlternateStyleDefault CssClass="igwgRowAltBlue2k7">
                                                    </RowAlternateStyleDefault>
                                                    <EditCellStyleDefault CssClass="igwgCellEdtBlue2k7">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault CssClass="igwgFooterBlue2k7">
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault CssClass="igwgHdrBlue2k7" CustomRules="font-weight:normal;" Height="23px">
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault CssClass="igwgRowSlctrBlue2k7">
                                                    </RowSelectorStyleDefault>
                                                    <SelectedHeaderStyleDefault CssClass="igwgHdrSelBlue2k7">
                                                    </SelectedHeaderStyleDefault>
                                                    <RowStyleDefault CssClass="igwgRowBlue2k7">
                                                        <Padding Left="3px" />
                                                        <BorderDetails WidthBottom="1px" WidthTop="1px" />
                                                    </RowStyleDefault>
                                                    <GroupByRowStyleDefault CssClass="igwgGrpRowBlue2k7">
                                                    </GroupByRowStyleDefault>
                                                    <SelectedGroupByRowStyleDefault CssClass="igwgGrpRowSelBlue2k7">
                                                    </SelectedGroupByRowStyleDefault>
                                                    <SelectedRowStyleDefault CssClass="igwgRowSelBlue2k7">
                                                    </SelectedRowStyleDefault>
                                                    <ActivationObject BorderColor="181, 196, 223" BorderWidth="">
                                                        <BorderDetails WidthLeft="0px" WidthRight="0px" />
                                                    </ActivationObject>
                                                    <RowExpAreaStyleDefault CssClass="igwgRowExpBlue2k7">
                                                    </RowExpAreaStyleDefault>
                                                    <FixedCellStyleDefault CssClass="igwgCellFxdBlue2k7">
                                                    </FixedCellStyleDefault>
                                                    <FixedHeaderStyleDefault CssClass="igwgHdrFxdBlue2k7">
                                                    </FixedHeaderStyleDefault>
                                                    <FixedFooterStyleDefault CssClass="igwgFtrFxdBlue2k7">
                                                    </FixedFooterStyleDefault>
                                                    <AddNewRowDefault Visible="Yes">
                                                    </AddNewRowDefault>
                                                    <FormulaErrorStyleDefault CssClass="igwgFormulaErrBlue2k7">
                                                    </FormulaErrorStyleDefault>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </ContentTemplate>
                                    </igtab:Tab>
                                    <igtab:TabSeparator>
                                    </igtab:TabSeparator>
                                    <igtab:Tab Text="Weakness Points">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList_WPoints" runat="server" AutoPostBack="True" Width="800px">
                                            </asp:DropDownList>
                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgWPoints" runat="server" Height="100px" ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/"
                                                meta:resourcekey="ZResourceZuwgGradesTransactionsResource1" Style="height: 242px;"
                                                TabIndex="9" Width="850px">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" HeaderText="ID" Hidden="True" Key="ID"
                                                                Width="0px">
                                                                <Header Caption="ID">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="PointsEnglish" HeaderText="Transaction Type"
                                                                Key="PointsEnglish" Width="400px">
                                                                <Header Caption="Weakness Points English" Title="">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="PointsArabic" EditorControlID="WebTextEdit1"
                                                                Type="Custom" Width="400px">
                                                                <Header Caption="Weakness Points Arabic " Title="">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                        </RowTemplateStyle>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                    BorderCollapseDefault="Separate" CellClickActionDefault="CellSelect"  
                                                    GridLinesDefault="NotSet" HeaderClickActionDefault="SortMulti" Name="uwgQuestionAnswers"
                                                    RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeCellDefault="Single"
                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                    <FilterOptionsDefault DropDownRowCount="15">
                                                        <FilterDropDownStyle CssClass="igwgFltrDrpDwnBlue2k7">
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle CssClass="igwgFltrRowHiLtBlue2k7">
                                                        </FilterHighlightRowStyle>
                                                    </FilterOptionsDefault>
                                                    <Pager>
                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </PagerStyle>
                                                    </Pager>
                                                    <AddNewBox Location="Top" Prompt="">
                                                        <ButtonStyle CssClass="igwgAddNewBtnBlue2k7">
                                                        </ButtonStyle>
                                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </BoxStyle>
                                                    </AddNewBox>
                                                    <GroupByBox Hidden="True">
                                                        <BandLabelStyle CssClass="igwgGrpBoxBandLblBlue2k7">
                                                        </BandLabelStyle>
                                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                        </BoxStyle>
                                                    </GroupByBox>
                                                    <FrameStyle BorderStyle="None" CssClass="igwgFrameBlue2k7" Height="100px" Width="850px">
                                                    </FrameStyle>
                                                    <Images ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/">
                                                    </Images>
                                                    <RowAlternateStyleDefault CssClass="igwgRowAltBlue2k7">
                                                    </RowAlternateStyleDefault>
                                                    <EditCellStyleDefault CssClass="igwgCellEdtBlue2k7">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault CssClass="igwgFooterBlue2k7">
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault CssClass="igwgHdrBlue2k7" CustomRules="font-weight:normal;" Height="23px">
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault CssClass="igwgRowSlctrBlue2k7">
                                                    </RowSelectorStyleDefault>
                                                    <SelectedHeaderStyleDefault CssClass="igwgHdrSelBlue2k7">
                                                    </SelectedHeaderStyleDefault>
                                                    <RowStyleDefault CssClass="igwgRowBlue2k7">
                                                        <Padding Left="3px" />
                                                        <BorderDetails WidthBottom="1px" WidthTop="1px" />
                                                    </RowStyleDefault>
                                                    <GroupByRowStyleDefault CssClass="igwgGrpRowBlue2k7">
                                                    </GroupByRowStyleDefault>
                                                    <SelectedGroupByRowStyleDefault CssClass="igwgGrpRowSelBlue2k7">
                                                    </SelectedGroupByRowStyleDefault>
                                                    <SelectedRowStyleDefault CssClass="igwgRowSelBlue2k7">
                                                    </SelectedRowStyleDefault>
                                                    <ActivationObject BorderColor="181, 196, 223" BorderWidth="">
                                                        <BorderDetails WidthLeft="0px" WidthRight="0px" />
                                                    </ActivationObject>
                                                    <RowExpAreaStyleDefault CssClass="igwgRowExpBlue2k7">
                                                    </RowExpAreaStyleDefault>
                                                    <FixedCellStyleDefault CssClass="igwgCellFxdBlue2k7">
                                                    </FixedCellStyleDefault>
                                                    <FixedHeaderStyleDefault CssClass="igwgHdrFxdBlue2k7">
                                                    </FixedHeaderStyleDefault>
                                                    <FixedFooterStyleDefault CssClass="igwgFtrFxdBlue2k7">
                                                    </FixedFooterStyleDefault>
                                                    <AddNewRowDefault Visible="Yes">
                                                    </AddNewRowDefault>
                                                    <FormulaErrorStyleDefault CssClass="igwgFormulaErrBlue2k7">
                                                    </FormulaErrorStyleDefault>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </ContentTemplate>
                                    </igtab:Tab>
                                </Tabs>
                                <RoundedImage FillStyle="LeftMergedWithCenter" HoverImage="ig_tab_winXPs2.gif" LeftSideWidth="7"
                                    NormalImage="ig_tab_winXPs3.gif" RightSideWidth="6" SelectedImage="ig_tab_winXPs1.gif"
                                    ShiftOfImages="2" />
                                <SelectedTabStyle>
                                    <Padding Bottom="2px" />
                                </SelectedTabStyle>
                                <DefaultTabStyle BackColor="GhostWhite" Font-Names="Microsoft Sans Serif" Font-Size="8pt"
                                    ForeColor="Black" Height="22px">
                                    <Padding Top="2px" />
                                </DefaultTabStyle>
                            </igtab:UltraWebTab>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td runat="server" style="width: 100%; direction: ltr; vertical-align: top; text-align: center;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table runat="server" cellpadding="0" cellspacing="0" style="width: 900px; background-color: #F8F8FF;">
                                <tr>
                                    <td style="direction: ltr; vertical-align: middle; text-align: center; height: 30px"
                                        colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                        height: 22px">
                                        <asp:Label ID="Label9" runat="server" Text="Over All Result"></asp:Label>
                                    </td>
                                    <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                        <asp:Label ID="Label10" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                        <asp:Label ID="Label_OvResult" runat="server" Text="Label"></asp:Label>
                                        &nbsp;<asp:Label ID="Label_OvResult0" runat="server" Text="/"></asp:Label>
                                        &nbsp;<asp:Label ID="Label_EvalPower" runat="server" ForeColor="#CC3300" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; direction: ltr; vertical-align: middle; text-align: right;
                                        height: 22px">
                                        <asp:Label ID="Label11" runat="server" Text="Result Scale"></asp:Label>
                                    </td>
                                    <td style="width: 3%; direction: ltr; vertical-align: middle; text-align: center;">
                                        <asp:Label ID="Label12" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td style="width: 77%; direction: ltr; vertical-align: middle; text-align: left;">
                                        <asp:Label ID="Label_ResultScale" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="direction: ltr; vertical-align: middle; text-align: center; height: 22px"
                                        colspan="3">
                                        <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEvaluation" runat="server" Width="900px" Height="400px">
                                            <Bands>
                                                <igtbl:UltraGridBand Expandable="No" GridLines="NotSet" GroupByColumnsHidden="Yes">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn BaseColumnName="Group_ID" DataType="System.Int32" Hidden="True"
                                                            Key="Group_ID" Width="50px" AllowGroupBy="No" SortIndicator="Disabled">
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="QuestionID" DataType="System.Int32" Hidden="True"
                                                            Key="QuestionID" Width="50px" AllowGroupBy="No" SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                            Key="ID" Width="50px" AllowGroupBy="No" SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn DataType="System.Int32" Key="Check" Type="CheckBox" Width="50px"
                                                            SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="MainID" Hidden="True" Key="MainID" Width="0px"
                                                            SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Evaluation_Group" Key="Evaluation_Group" Width="50px"
                                                            SortIndicator="Disabled">
                                                            <Header Caption="Evaluation Group">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Question_Name" Key="Question_Name" Width="50px"
                                                            SortIndicator="Disabled">
                                                            <Header Caption="Question Name">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="AnswerEName"
                                                            Key="AnswerEName" Width="750px" SortIndicator="Disabled">
                                                            <Header Caption="Answer Name">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="GPower" DataType="System.Int32" Hidden="True"
                                                            Key="GPower" Width="50px" SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="QPower" DataType="System.Int32" Hidden="True"
                                                            Key="QPower" Width="50px" SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="APower" DataType="System.Int32" Hidden="True"
                                                            Key="APower" Width="50px" SortIndicator="Disabled">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Rank1" DataType="System.Int32" Hidden="True"
                                                            Key="Rank1" Width="20px" SortIndicator="Ascending">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Rank2" DataType="System.Int32" Hidden="True"
                                                            Key="Rank2" Width="20px" SortIndicator="Ascending">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Rank3" DataType="System.Int32" Hidden="True"
                                                            Key="Rank3" Width="20px" SortIndicator="Ascending">
                                                            <Header>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                BorderCollapseDefault="Separate" CellPaddingDefault="1" CellSpacingDefault="1"
                                                ColHeadersVisibleDefault="No" GroupByRowDescriptionMaskDefault="[value]" HeaderClickActionDefault="NotSet"
                                                Name="UltraWebGrid1" RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeColDefault="Single"
                                                SelectTypeRowDefault="Extended" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                Version="4.00" ViewType="OutlookGroupBy" CellTitleModeDefault="Always">
                                                <FrameStyle BackColor="White" BorderColor="#E6E6E6" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Names="Trebuchet MS,Verdana,Arial,sans-serif" Font-Size="X-Small" Width="900px"
                                                    Height="400px">
                                                </FrameStyle>
                                                <Images>
                                                    <CollapseImage Url="/ig_common/images/Themes/Aero/ig_treeArrowMinus.png" />
                                                    <ExpandImage Url="/ig_common/images/Themes/Aero/ig_treeArrowPlus.png" />
                                                    <CurrentRowImage Url="/ig_common/images/Themes/Aero/ig_CurrentRow.gif" />
                                                    <FixedHeaderOffImage Url="/ig_common/images/Themes/Aero/ig_tblFixedOff.gif" />
                                                    <FixedHeaderOnImage Url="/ig_common/images/Themes/Aero/ig_tblFixedOn.gif" />
                                                </Images>
                                                <Pager>
                                                    <PagerStyle BackColor="Red" BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </PagerStyle>
                                                </Pager>
                                                <EditCellStyleDefault BackColor="White" BorderStyle="None" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                                                    Font-Size="X-Small" BorderWidth="0px">
                                                    <Margin Bottom="0px" Left="4px" Right="0px" Top="0px" />
                                                    <Padding Bottom="0px" Left="2px" Right="0px" Top="0px" />
                                                </EditCellStyleDefault>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </FooterStyleDefault>
                                                <HeaderStyleDefault BackgroundImage="/ig_common/images/Themes/Aero/grid_header_bg.jpg"
                                                    BorderStyle="None" Cursor="Hand" Font-Bold="True" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                                                    Font-Size="X-Small" ForeColor="#555555" Height="23px" HorizontalAlign="Left"
                                                    BackColor="LightGray">
                                                    <Padding Left="5px" />
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </HeaderStyleDefault>
                                                <RowSelectorStyleDefault BackColor="White" BackgroundImage="none">
                                                </RowSelectorStyleDefault>
                                                <SelectedHeaderStyleDefault BackgroundImage="/ig_common/images/Themes/Aero/grid_header_selected_bg.jpg">
                                                </SelectedHeaderStyleDefault>
                                                <RowStyleDefault BackColor="Window" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                                                    Font-Size="X-Small" Height="19px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                                                    <Padding Left="6px" />
                                                    <BorderDetails ColorLeft="230, 230, 230" StyleLeft="Solid" WidthLeft="1px" WidthTop="0px"
                                                        ColorTop="Window" />
                                                </RowStyleDefault>
                                                <GroupByRowStyleDefault BackColor="#F4FBFE">
                                                </GroupByRowStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#E0F1F9" BackgroundImage="/ig_common/images/Themes/Aero/row_selected_bg.jpg"
                                                    BorderStyle="None" BorderWidth="0px" CustomRules="background-repeat: repeat-x;">
                                                    <Padding Left="7px" />
                                                </SelectedRowStyleDefault>
                                                <GroupByBox Hidden="True">
                                                    <BandLabelStyle BackColor="#6372D4" ForeColor="White">
                                                    </BandLabelStyle>
                                                    <BoxStyle BackColor="#3C7FB1" BackgroundImage="/ig_common/images/Themes/Aero/groupBy_bg.jpg"
                                                        BorderColor="Window" Font-Bold="True" ForeColor="#3C7FB1" Height="40px">
                                                    </BoxStyle>
                                                </GroupByBox>
                                                <AddNewBox Hidden="False">
                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </BoxStyle>
                                                </AddNewBox>
                                                <ActivationObject BorderColor="204, 237, 252" BorderStyle="Solid" BorderWidth="1px">
                                                    <BorderDetails WidthLeft="0px" WidthRight="0px" />
                                                </ActivationObject>
                                                <FilterOptionsDefault>
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px" Width="200px">
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
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="direction: ltr; vertical-align: middle; text-align: center; height: 10px"
                                        colspan="3">
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
