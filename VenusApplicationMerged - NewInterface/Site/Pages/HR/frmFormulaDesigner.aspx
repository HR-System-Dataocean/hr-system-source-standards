<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFormulaDesigner.aspx.vb"
    Inherits="Interfaces_frmFormulaDesigner" UICulture="Auto" %>

<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formula Designer</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <script language="javascript" src="App_JScript.js"></script>
    <script language="javascript">

        var controlname;
        var controltype;

        function targetControl(ctlname, ctltype) {
            controlname = ctlname;
            controltype = ctltype;
        }

        function listboxClick() {
            var ele = window.document.getElementById("txtFormula")

        }
        function btnPlus_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '+'
        }

        function btnMinus_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '-'
        }

        function btnMultiply_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '*'
        }

        function btnDivide_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '/'
        }

        function btnLeftBracket_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '('
        }

        function btnRightBracket_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + ')'
        }

        function btnPower_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '^'
        }

        function btnClear_Click(oButton, oEvent) {
            var formula = window.document.getElementById("txtFormula")
            formula.value = ''
            error.value = ''
        }

        function btnCancel_Click(oButton, oEvent) {
            window.close();
        }

        //-------------------------------0257 MODIFIED-----------------------------------------
        function GetRowIndexFromCellId(cellId) {
            var arr = cellId.split('_');
            var tt = arr[2] - 1;
            tt = tt + 1;
            return tt;
        }
        //-------------------------------=============-----------------------------------------

        function uwgFieldName_DblClickHandler(gridName, cellId) {
            var row = igtbl_getRowById(cellId)
            //-------------------------------0257 MODIFIED-----------------------------------------
            var RowIndex = row.getIndex();
            var rowIndexC = GetRowIndexFromCellId(cellId);
            if (RowIndex != rowIndexC && RowIndex != -1) {
                RowIndex = rowIndexC;
            }
            //var cell = igtbl_getCellById(gridName + "_rc_" + row.getIndex() + "_1")
            var cell = igtbl_getCellById(gridName + "_rc_" + RowIndex + "_1");
            //-------------------------------=============-----------------------------------------





            var formula = window.document.getElementById("txtFormula")
            formula.value = formula.value + '<@' + cell.getValue() + '@>'
        }


        function uwgBenetitTemplet_DblClickHandler(gridName, cellId) {
            var Row = igtbl_getRowById(cellId)
            //-------------------------------0257 MODIFIED-----------------------------------------
            var RowIndex = Row.getIndex();
            var rowIndexC = GetRowIndexFromCellId(cellId);
            if (RowIndex != rowIndexC && RowIndex != -1) {
                RowIndex = rowIndexC;
            }
            //------------------------------============-------------------------------------------  
            //if (gridName + "_rc_" + Row.getIndex() + "_0" ==  cellId)
            if (gridName + "_rc_" + RowIndex + "_0" == cellId) {
                //var cell = igtbl_getCellById(gridName + "_rc_" + Row.getIndex() + "_0")
                var cell = igtbl_getCellById(gridName + "_rc_" + RowIndex + "_0")

                var formula = window.document.getElementById("txtFormula")
                formula.value = formula.value + '<$' + cell.getValue() + '$>'
            }
            else {
                //if (gridName + "_rc_" + Row.getIndex() + "_1" ==  cellId)
                if (gridName + "_rc_" + RowIndex + "_1" == cellId) {
                    //var cell = igtbl_getCellById(gridName + "_rc_" + Row.getIndex() + "_0")
                    var cell = igtbl_getCellById(gridName + "_rc_" + RowIndex + "_0")

                    var formula = window.document.getElementById("txtFormula")
                    formula.value = formula.value + '<$' + cell.getValue() + '$>'
                }
            }
        }

        function btnApply_Click(oButton, oEvent) {
            var error = window.document.getElementById("txtErrorDescription")
            var formula = window.document.getElementById("txtFormula")
            var fromulavlue = formula.value.replace(" ", "")
            var chkField = CheckExpression(fromulavlue)

            if (chkField == true) {
                error.value = "Syntax check successfully"
                if (controltype == 'T') {

                    window.opener.document.forms[0][controlname].value = fromulavlue;
                    window.opener.focus();
                    window.opener.document.forms[0][controlname].focus();
                    window.close();
                }
                else {

                    var cell = window.opener.igtbl_getCellById(controlname)
                    cell.setValue(fromulavlue)
                    window.opener.focus();
                    window.close();

                }

            }
        }

        function CheckExpression(exepression) {
            var error = window.document.getElementById("txtErrorDescription")
            var index = 0;
            var strcurrentstring = "";
            var arrchar = new Array();
            var exepressionarr = exepression.split("");



            if (exepression.length < 1) {
                error.value = "Please Spacify The Expression"
                return false
            }
            else {
                error.value = ""
            }


            for (intcounter = 0; intcounter < exepression.length; intcounter++) {
                switch (exepressionarr[intcounter]) {
                    case ("+"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("-"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("/"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("*"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("^"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("("):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case (")"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }
                            strcurrentstring = ""
                            arrchar[index] = exepressionarr[intcounter]
                            index = index + 1
                            break;
                        }

                    case ("<"):
                        {
                            if (strcurrentstring.length > 0) {
                                arrchar[index] = strcurrentstring
                                index = index + 1
                            }


                            strcurrentstring = "";
                            var currentindex = 0;
                            while (exepressionarr[intcounter + currentindex] != ">" && (intcounter + currentindex) < exepressionarr.length) {
                                strcurrentstring = strcurrentstring + exepressionarr[intcounter + currentindex]
                                currentindex = currentindex + 1
                            }

                            arrchar[index] = strcurrentstring + exepressionarr[intcounter + currentindex]

                            if (!checkfield(arrchar[index])) {
                                error.value = "Please Check Fields"
                                return false
                            }

                            index = index + 1
                            strcurrentstring = ""
                            intcounter = intcounter + currentindex

                            break;
                        }


                    default:
                        {
                            strcurrentstring = strcurrentstring + exepressionarr[intcounter]
                        }
                }
            }


            if (index == 0 && strcurrentstring.length > 0) {
                arrchar[index] = strcurrentstring
            }
            else {
                if (strcurrentstring.length > 0) {
                    arrchar[index] = strcurrentstring
                }
                else {
                    index = index - 1
                }

            }


            var chkbraket = CheckBraket(arrchar)
            if (chkbraket == false) {
                error.value = "Please Check Brackets"
                return false
            }
            else {
                error.value = ""
            }

            var chkOperation = CheckOperation(arrchar)
            if (chkOperation == false) {
                error.value = "Please Check Arithmetic syntax"
                return false
            }
            else {
                error.value = ""
                return true;
            }



        }


        function CheckBraket(Arr) {
            var openBraket = 0;

            for (intcounter = 0; intcounter < Arr.length; intcounter++) {
                switch (Arr[intcounter]) {
                    case ("("):
                        {
                            openBraket = openBraket + 1;
                            break;
                        }
                    case (")"):
                        {
                            if (openBraket > 0) {
                                openBraket = openBraket - 1;
                            }
                            else {
                                return false;
                            }
                            break;
                        }

                }

            }
            if (openBraket > 0) {
                return false;
            }
            else {
                return true;
            }
        }

        function CheckOperation(Arr) {

            var openStatment = 0;


            for (intcounter = 0; intcounter < Arr.length; intcounter++) {
                switch (Arr[intcounter]) {
                    case ("+"):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment - 1;
                            }
                            else {
                                return false;
                            }
                            break;
                        }

                    case ("-"):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment - 1;
                            }
                            else {
                                return false;
                            }
                            break;
                        }

                    case ("*"):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment - 1;
                            }
                            else {
                                return false;
                            }
                            break;
                        }

                    case ("/"):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment - 1;
                            }
                            else {
                                return false;
                            }
                            break;
                        }

                    case ("("):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment;
                            }
                            break;
                        }

                    case (")"):
                        {
                            if (openStatment > 0) {
                                openStatment = openStatment;
                            }
                            else {
                                return false;
                            }
                            break;
                        }


                    default:
                        {
                            var partarr = Arr[intcounter].split("");
                            if (partarr[0] != "<") {
                                if (isNaN(Arr[intcounter])) {
                                    return false;
                                }
                            }
                            openStatment = openStatment + 1;
                            break;
                        }

                }

            }


            if (openStatment == 1) {
                return true;
            }
            else {
                return false;
            }

        }

        function checkfield(Arr) {

            var partarr = Arr.split("")
            if (partarr[partarr.length - 1] == ">") {
                if (partarr[1] == "#") {

                    if (partarr[partarr.length - 2] != "#") {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (partarr[1] == "@") {
                        if (partarr[partarr.length - 2] != "@") {
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                    else {
                        if (partarr[partarr.length - 2] != "$") {
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }
            else {
                return false

            }
        }



    </script>
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebTab.css " rel="Stylesheet"
        type="text/css" />
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebPanel.css " rel="Stylesheet"
        type="text/css" />
</head>
<body bottommargin="0" dir="ltr" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <div>
            <div id="DIV" runat="server" align="left" dir="ltr" style="border-right: 1px outset; border-top: 1px outset; z-index: 102; left: 0px; background-image: url(#083e7c); border-left: 1px outset; width: 785px; border-bottom: 1px outset; position: absolute; top: 0px; height: 318px; background-color: inactivecaptiontext">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" CssClass="igwtMainBlue2k7" Height="314px"
                ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebTab/" Style="left: 0px; position: absolute; top: 0px"
                ThreeDEffect="False" Width="239px" SelectedTab="1">
                <Tabs>
                    <igtab:Tab Text="Transactions">
                        <ContentTemplate>
                            <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgBenetitTemplet" runat="server"  Height="251px"
                                Style="left: 15px; position: absolute; top: 30px" Width="220px">
                                <Bands>
                                    <igtbl:UltraGridBand CellClickAction="RowSelect">
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
                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" DataType="System.Double"
                                                FooterText="Total Amount : " FooterTotal="Sum" Format=" ###,###,##0.00 " HeaderText="Trans."
                                                Width="40px">
                                                <Header Caption="Trans.">
                                                </Header>
                                                <Footer Caption="Total Amount : " Total="Sum">
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EngName" HeaderText="Description"
                                                Key="Description" Width="150px">
                                                <Header Caption="Description">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" HeaderText="ID" Hidden="True" Key="ID">
                                                <Header Caption="ID">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                                <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                    BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" CellSpacingDefault="1"
                                    HeaderClickActionDefault="SortMulti" Name="uwgBenetitTemplet" RowHeightDefault="20px"
                                    RowSelectorsDefault="No" SelectTypeCellDefault="Single" SelectTypeRowDefault="Extended"
                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                    Version="4.00">
                                    <GroupByBox Hidden="True">
                                        <style backcolor="ActiveBorder" bordercolor="Window"></style>
                                    </GroupByBox>
                                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                    </GroupByRowStyleDefault>
                                    <ActivationObject BorderColor="White" BorderStyle="Outset" BorderWidth="">
                                    </ActivationObject>
                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </FooterStyleDefault>
                                    <RowStyleDefault BackColor="White" BorderStyle="Solid" BorderWidth="1px" Cursor="Hand">
                                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                        <Padding Left="3px" />
                                    </RowStyleDefault>
                                    <FilterOptionsDefault>
                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                            Font-Size="11px" Height="300px" Width="200px">
                                            <Padding Left="2px" />
                                        </FilterDropDownStyle>
                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                        </FilterHighlightRowStyle>
                                    </FilterOptionsDefault>
                                    <ClientSideEvents DblClickHandler="uwgBenetitTemplet_DblClickHandler" />
                                    <HeaderStyleDefault BackColor="#BFDBFF" BorderStyle="Solid" BorderWidth="1px" ForeColor="White"
                                        HorizontalAlign="Left">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </HeaderStyleDefault>
                                    <RowAlternateStyleDefault BackColor="whitesmoke" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px">
                                    </RowAlternateStyleDefault>
                                    <EditCellStyleDefault BackColor="#FFE0C0" BorderStyle="Inset" BorderWidth="1px">
                                    </EditCellStyleDefault>
                                    <FrameStyle BackColor="Transparent" BorderColor="White" BorderStyle="None" Font-Names="Microsoft Sans Serif"
                                        Font-Size="8.25pt" Height="251px" Width="220px">
                                    </FrameStyle>
                                    <Pager>
                                        <style backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White" > </BorderDetails >
                                        </style>
                                    </Pager>
                                    <AddNewBox>
                                        <style backcolor="Window" bordercolor="InactiveCaption" borderstyle="Solid" borderwidth="1px">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White" > </BorderDetails >
                                        </style>
                                    </AddNewBox>
                                </DisplayLayout>
                            </igtbl:UltraWebGrid>
                        </ContentTemplate>
                    </igtab:Tab>
                    <igtab:Tab Text="Additional Fields">
                        <ContentTemplate>
                            <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgFieldName" runat="server" Height="250px"
                                Style="left: 15px; position: absolute; top: 29px" Width="216px">
                                <Rows>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="No. of Days Per Period" Key="NoOfDaysPerPeriod">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Text="NDPP" Key="ID">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="No. of Hours Per Period" Key="NoOfHoursPerPeriod">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Text="NHPP" Key="ID">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="No. of Work Hours Per Day" Key="WHPD">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Text="WHPD" Key="ID">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Overtime Factor" Key="OVF">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Text="OVF" Key="ID">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Holiday Factor" Key="HOFactor">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Text="HOF" Key="ID">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Working units per period ">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="WUPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Overtime hours per period ">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="OHPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Overtime hours  (Holidays)">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="OHPH">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Holidays units per period ">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="HUPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Salary Price Per Hour">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="SPPH">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Salary Price Per Day">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="SPPD">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Begin / End of Contract">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="BEOC">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Project Working Units">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PWU">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Total Projects Working Units">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="TPWU">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="End Of Services">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="EOB">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Overtime Formula">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="OTF">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Holiday Formula">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="HDF">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Health Insurance Cost">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="HIC">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Tickets Cost">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="TIC">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Annual Vacation Cost">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="AVC">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="End of Service Cost">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="ESC">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Contract Period Months">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="CMO">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Current Ticket Value">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="TIV">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="One Ticket Value">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="OTIV">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Start Contract Ratio">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="RUPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Period Annual Vac Days">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="AVDPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Annual Vac Ratio">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="AVRPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Number Of Dependancies">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="NOD">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Factor Of Dependancies Has HI">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="FODHI">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Factor Of Dependancies Has Tickets">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="FODT">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project Sick">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PSick">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project Leave">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PLeave">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project Delay">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PDelay">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project Absent">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PAbsent">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project OT">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="POtime">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Hr Project HOT">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="PHOtime">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Empty">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="Empty">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Absent days">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="AbsentDays">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Vaction Duration Days">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="VDD">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Another Vacation Unpaid Days Per Period">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="ANVPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                    <igtbl:UltraGridRow Height="">
                                        <Cells>
                                            <igtbl:UltraGridCell Text="Sick Vacation Per Period">
                                            </igtbl:UltraGridCell>
                                            <igtbl:UltraGridCell Key="ID" Text="SVPP">
                                            </igtbl:UltraGridCell>
                                        </Cells>
                                    </igtbl:UltraGridRow>
                                </Rows>
                                <Bands>
                                    <igtbl:UltraGridBand>
                                        <RowEditTemplate>
                                            <br>
                                                <p align="center">
                                                    <input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px;"
                                                        type="button" value="OK"> &nbsp;
                                                        <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px;"
                                                            type="button" value="Cancel"> </input>
                                                    </input>
                                                </p>
                                            </br>
                                        </RowEditTemplate>
                                        <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                        </RowTemplateStyle>
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
                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" FooterText="Total Amount : "
                                                FooterTotal="Sum" Format="" HeaderText="Field Name" Width="150px">
                                                <Header Caption="Field Name">
                                                </Header>
                                                <Footer Caption="Total Amount : " Total="Sum">
                                                </Footer>
                                                <CellStyle Cursor="Hand">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" HeaderText="Abbrev." Key="ID" Width="55px">
                                                <Header Caption="Abbrev.">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                                <CellStyle Cursor="Hand">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                                <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                    BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" CellSpacingDefault="1"
                                    HeaderClickActionDefault="SortMulti" Name="uwgFieldName" RowHeightDefault="20px"
                                    RowSelectorsDefault="No" SelectTypeCellDefault="Single" SelectTypeRowDefault="Extended"
                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                    Version="4.00">
                                    <GroupByBox Hidden="True">
                                        <style backcolor="ActiveBorder" bordercolor="Window"></style>
                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                        </BoxStyle>
                                    </GroupByBox>
                                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                    </GroupByRowStyleDefault>
                                    <ActivationObject BorderColor="White" BorderStyle="Outset" BorderWidth="">
                                    </ActivationObject>
                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </FooterStyleDefault>
                                    <RowStyleDefault BackColor="White" BorderStyle="None">
                                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                        <Padding Left="3px" />
                                    </RowStyleDefault>
                                    <FilterOptionsDefault>
                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                            Font-Size="11px" Height="300px" Width="200px">
                                            <Padding Left="2px" />
                                        </FilterDropDownStyle>
                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                        </FilterHighlightRowStyle>
                                    </FilterOptionsDefault>
                                    <ClientSideEvents DblClickHandler="uwgFieldName_DblClickHandler" />
                                    <HeaderStyleDefault BackColor="#BFDBFF" BorderStyle="Solid" BorderWidth="1px" ForeColor="White"
                                        HorizontalAlign="Left">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </HeaderStyleDefault>
                                    <RowAlternateStyleDefault BackColor="whitesmoke" BorderStyle="Solid" BorderWidth="1px">
                                    </RowAlternateStyleDefault>
                                    <EditCellStyleDefault BackColor="#FFE0C0" BorderStyle="Inset" BorderWidth="1px">
                                    </EditCellStyleDefault>
                                    <FrameStyle BorderStyle="None" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt"
                                        Height="250px" Width="216px">
                                    </FrameStyle>
                                    <Pager>
                                        <style backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White" > </BorderDetails >
                                        </style>
                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </PagerStyle>
                                    </Pager>
                                    <AddNewBox>
                                        <style backcolor="Window" bordercolor="InactiveCaption" borderstyle="Solid" borderwidth="1px">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White" > </BorderDetails >
                                        </style>
                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </BoxStyle>
                                    </AddNewBox>
                                </DisplayLayout>
                            </igtbl:UltraWebGrid>
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>
                <DefaultTabStyle CssClass="igwtTabNormalBlue2k7" Height="22px">
                </DefaultTabStyle>
                <HoverTabStyle CssClass="igwtTabHoverBlue2k7">
                </HoverTabStyle>
                <RoundedImage FillStyle="LeftMergedWithCenter" HoverImage="igwt_tab_hover.jpg" LeftSideWidth="14"
                    NormalImage="none" RightSideWidth="14" SelectedImage="igwt_tab_selected.jpg" />
                <SelectedTabStyle CssClass="igwtTabSelectedBlue2k7">
                </SelectedTabStyle>
            </igtab:UltraWebTab>
                <asp:TextBox ID="txtErrorDescription" runat="server" BorderStyle="Solid" Height="46px"
                    ReadOnly="True" Style="left: 246px; position: absolute; top: 231px" TextMode="MultiLine"
                    Width="528px" BorderColor="#C0C0FF" BorderWidth="1px"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Height="16px" Style="left: 246px; position: absolute; top: 287px"
                    Width="318px" BorderColor="#C0C0FF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                &nbsp;&nbsp;
            <asp:TextBox ID="txtFormula" runat="server" BorderStyle="Solid" Height="167px" Style="left: 247px; position: absolute; top: 30px"
                TextMode="MultiLine" Width="526px" BorderColor="#C0C0FF"
                BorderWidth="1px"></asp:TextBox>
                <igtxt:WebImageButton ID="btnPlus" runat="server" Height="16px" Style="left: 247px; position: absolute; top: 207px"
                    Text="+" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnPlus_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnMinus" runat="server" Height="16px" Style="left: 274px; position: absolute; top: 207px"
                    Text="-" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnMinus_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnMultiply" runat="server" Height="16px" Style="left: 301px; position: absolute; top: 207px"
                    Text="*" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnMultiply_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnDivide" runat="server" Height="16px" Style="left: 328px; position: absolute; top: 207px"
                    Text="/" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnDivide_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnLeftBracket" runat="server" Height="16px" Style="left: 355px; position: absolute; top: 207px"
                    Text="(" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnLeftBracket_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnRightBracket" runat="server" Height="16px" Style="left: 382px; position: absolute; top: 207px"
                    Text=")" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <ClientSideEvents Click="btnRightBracket_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnPower" runat="server" Height="16px" Style="left: 409px; position: absolute; top: 207px"
                    Text="^" UseBrowserDefaults="False" Width="26px"
                    AutoSubmit="False">
                    <ClientSideEvents Click="btnPower_Click" />
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                </igtxt:WebImageButton>
                &nbsp;
            <igtxt:WebImageButton ID="btnClear" runat="server" Height="16px" Style="left: 674px; position: absolute; top: 207px"
                Text="Clear" UseBrowserDefaults="False" Width="102px"
                AutoSubmit="False">
                <ClientSideEvents Click="btnClear_Click" />
                <Appearance>
                    <style font-names="Times New Roman"></style>
                </Appearance>
                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                    HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                    PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                    WidthOfRightEdge="2" />
            </igtxt:WebImageButton>
                <div style="left: 241px; width: 544px; position: absolute; top: 0px; height: 22px; background-color: #bfdbff">
                </div>
                <igtxt:WebImageButton ID="btnCancel" runat="server" Height="16px" Style="left: 675px; position: absolute; top: 287px"
                    Text="Cancel" UseBrowserDefaults="False" Width="102px"
                    AutoSubmit="False">
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <ClientSideEvents Click="btnCancel_Click" />
                </igtxt:WebImageButton>
                <igtxt:WebImageButton ID="btnApply" runat="server" Height="16px" Style="left: 571px; position: absolute; top: 287px"
                    Text="Apply" UseBrowserDefaults="False" Width="102px"
                    AutoSubmit="False">
                    <Appearance>
                        <style font-names="Times New Roman"></style>
                    </Appearance>
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                        PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                        WidthOfRightEdge="2" />
                    <ClientSideEvents Click="btnApply_Click" />
                </igtxt:WebImageButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Style="left: 441px; position: absolute; top: 207px"></asp:TextBox>
            <igtxt:WebImageButton ID="WebImageButton2" runat="server" Height="16px" Style="left: 571px; position: absolute; top: 288px"
                Text="Cancel" UseBrowserDefaults="False" Width="102px"
                AutoSubmit="False">
                <Appearance>
                    <style font-names="Times New Roman"></style>
                </Appearance>
                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                    HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif" MaxHeight="40" MaxWidth="400"
                    PressedImageUrl="ig_butCRM2.gif" RenderingType="FileImages" HeightOfBottomEdge="2"
                    WidthOfRightEdge="2" />
                <ClientSideEvents Click="btnCancel_Click" />
            </igtxt:WebImageButton>
        </div>
    </form>
</body>
</html>
