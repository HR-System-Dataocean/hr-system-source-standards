

//===================== frmEmployeeLoans [Start]
var loading = 0;
function btnRecalculate_Click(oButton, oEvent) {
  
    ReCalculateInstalment()
}




function btnAmount_Click(oButton, oEvent) {
    ReCalculateAmount()
}



//   ========================================================================
//   ProcedureName  :  ReCalculateAmount 
//   Screen         :  frmEmployeePeriodicalTransactions
//   Project        :  Venus V.
//   Description    :  Calculate  the instalment Amount and Modify the Grid.
//   fn. Arguments  :  ctrlID 
//   Modifications  :  B#001 19-05-2008 [0260] Change the way to Find Controls on tab 
//                     and Calculate for remaining Amount or the original Amount
//   ///////////////////////////////// 
function ReCalculateAmount() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    //-------------------------------0257 MODIFIED-----------------------------------------
    //var totaltransaction        = window.document.getElementById("txtTransactionValue")\
    var totaltransaction = igtab_getElementById("txtTransactionAmount", webTab.element);
    var totaltransactionremain = igtab_getElementById("txtTransactionValue", webTab.element);

    var totaltransactionvalue = 0;
    if (totaltransaction.disabled == false) {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransaction.value))
    }
    else {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransactionremain.value))
    }
    //-------------------------------=============-----------------------------------------

    var InstalmentAmount = igtab_getElementById("txtInstalmentAmount", webTab.element)
    var oCombo = igtab_getElementById('WebDateChooser1', webTab.element);
    var totaltransactionvalue2 = 0;
    var instalmentAmountvalue = RoundDecimalNumber(Math.abs(InstalmentAmount.value));
    var oCombovalue = oCombo.value;
    var Arr = oCombovalue.split("-");

    //var item = igtbar_getItemById("TlbMainToolbar_Item_2")

    
    if (totaltransactionvalue == '') { alert("Transaction Amount can't be Zero               "); return false; }
    if (instalmentAmountvalue == '') { alert("Transaction Amount can't be Zero               "); return false; }
    if (totaltransactionvalue == 0) { alert("Transaction Amount can't be Zero               "); return false; }
    if (instalmentAmountvalue == 0) { alert("Transaction Amount can't be Zero               "); return false; }
    if (totaltransactionvalue < instalmentAmountvalue) { alert("Transaction Amount shoud be grater than amount "); return false; }

    var noofinstalmentvalue = RoundDecimalNumber(Math.ceil(totaltransactionvalue / instalmentAmountvalue));

    var Grid = igtbl_getGridById('UltraWebTab1__ctl0_uwgScheduleTemplet')
    var noofinstalment = igtab_getElementById("txtNoofInstalment", webTab.element)

    var intPaidInstalment = 0;
    if (Grid.Rows.length > 0) {
        //DeleteRow()
        var RowIndices = "_";
        for (iCount = 0; iCount < Grid.Rows.length; iCount++) {
            var Row = Grid.Rows.rows[iCount];
            if (Row == null || Row == undefined)
                Row = igtbl_getRowById(Grid.Id + "_r_" + iCount);
            var RowId = Row.Id;
            var Str = RowId.split("_");
            var Cell = igtbl_getCellById(Str[0] + "_rc_" + Str[2] + "_3");
            if (Cell != null)
                if (Cell.getValue() != "True")
                RowIndices = RowIndices + Str[2] + "_";
            else
                intPaidInstalment++;

        }
        var RowIndexs = RowIndices.split("_")
        for (iCount2 = 0; iCount2 < RowIndexs.length; iCount2++) {
            if (RowIndexs[iCount2] != "")
                igtbl_deleteRow(Grid.Id, Grid.Id + "_r_" + RowIndexs[iCount2]);
        }

    }


    var yearindex = 0;
    var monthindex = 0;
    var dayindex = 0;

    //-------------------------------0257 MODIFIED-----------------------------------------
    /*var currentmonth            = Math.abs(Arr[0]);
    var currentday              = Math.abs(Arr[1]);
    var currentyear             = Math.abs(Arr[2]);*/
    var currentmonth = Math.abs(Arr[1]);
    var currentday = Math.abs(Arr[2]);
    var currentyear = Math.abs(Arr[0]);
    //-------------------------------=============-----------------------------------------
    loading = 1;
    var limit = 0;
    for (intcounter = 0; intcounter < noofinstalmentvalue; intcounter++) {
        var currentrow = igtbl_addNew('UltraWebTab1__ctl0_uwgScheduleTemplet', 0, true, true)
        var firstcell = currentrow.cells[0]
        var secondcell = currentrow.cells[1]
        var thirdcell = currentrow.cells[2]
        if (currentmonth >= 12 && intcounter > 0) {
            currentyear = currentyear + 1
            currentmonth = 1
        }
        else {
            if (intcounter == 0) {
                currentmonth = currentmonth
            }
            else {
                currentmonth = currentmonth + 1
            }

        }

        if (totaltransactionvalue >= instalmentAmountvalue) {
            firstcell.setValue(instalmentAmountvalue)
            totaltransactionvalue = RoundDecimalNumber(totaltransactionvalue - instalmentAmountvalue)
            //totaltransactionvalue = totaltransactionvalue + instalmentAmountvalue
            totaltransactionvalue2 = RoundDecimalNumber(totaltransactionvalue2 + instalmentAmountvalue);
        }
        else {

            firstcell.setValue(totaltransactionvalue)
            instalmentAmountvalue = RoundDecimalNumber(totaltransactionvalue);
            totaltransactionvalue2 = RoundDecimalNumber(totaltransactionvalue2 + instalmentAmountvalue);
        }
        //-------------------------------0257 MODIFIED-----------------------------------------
        var newInstalmentDate = new Date();
        newInstalmentDate.setFullYear(currentyear, (currentmonth - 1), currentday);
        secondcell.setValue(newInstalmentDate);
        //secondcell.setValue(currentday + '/' + currentmonth + '/' + currentyear)
        //-------------------------------=============-----------------------------------------
        thirdcell.setValue("False");
    }
    noofinstalment.value = Grid.Rows.length - intPaidInstalment;
    loading = 0;
    var ofooterInstalment = window.document.all.item("lblCurrentInstalmentvalue");
    var ofooter = window.document.all.item("lbltotalTransactionvalue");
    ofooter.innerText = 0
    ofooterInstalment.innerText = totaltransactionvalue2;

    ofooterInstalment.style.color = ""
    var Transval = 0;
    if (totaltransaction.getEnabled() == true) {
        Transval = Math.abs(totaltransaction.value)
    }
    else {
        Transval = Math.abs(totaltransactionremain.value)
    }

    if (RoundDecimalNumber(Math.abs(totaltransactionvalue2)) == RoundDecimalNumber(Transval)) {
        //item.setEnabled(true);
    }
    else {
        //item.setEnabled(false);
    }
    //
}




//   ========================================================================
//   ProcedureName  :  ReCalculateInstalment() 
//   Screen         :  frmEmployeePeriodicalTransactions
//   Project        :  Venus V.
//   Description    :  Calculate  the instalment Amount and create the Grid.
//   Modifications  :  B#001 19-05-2008 [0260] Change the way to Find Controls on tab 
//                     and Calculate for remaining Amount or the original Amount
//   ///////////////////////////////// 
var difference = 0
function ReCalculateInstalment() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var totaltransaction = igtab_getElementById("txtTransactionAmount", webTab.element);
    var totaltransactionremain = igtab_getElementById("txtTransactionValue", webTab.element);
    if (totaltransaction.disabled == false) {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransaction.value));
        totaltransactionremain.value = RoundDecimalNumber(totaltransactionvalue);
    }
    else {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransactionremain.value))
    }
    var noofinstalment = igtab_getElementById("txtNoofInstalment", webTab.element)
    var endDateid = igtab_getElementById("wdcEndDate", webTab.element)

    var oCombo = igtab_getElementById('WebDateChooser1', webTab.element);
    var noofinstalmentvalue = RoundDecimalNumber(noofinstalment.value)
    var oCombovalue = oCombo.value;
    var Arr = oCombovalue.split("-");
 

    
    //var item = igtbar_getItemById("TlbMainToolbar_Item_2")

    
    if (totaltransactionvalue == '') { alert("Transaction Amount can't be Zero        "); return false; }
    if (noofinstalment == '') { alert("No of Instalment can't be Zero          "); noofinstalment.value = 1; return false; }
    if (totaltransactionvalue == 0) { alert("Transaction Amount can't be Zero        "); return false; }
    if (noofinstalmentvalue <= 0) { alert("No of Instalment can't be Zero or Minus "); noofinstalment.value = 1; return false; }
    if (noofinstalmentvalue <= 0) { alert("No of Instalment can't be Zero or Minus "); noofinstalment.value = 1; return false; }
    if (noofinstalmentvalue > totaltransactionvalue) { alert("No of Instalment can't be greater than the transaction Amount "); noofinstalment.focus(); noofinstalment.value = 1; return false; }

    var instalmentvalue = Math.floor(RoundDecimalNumber(totaltransactionvalue / noofinstalmentvalue))
    difference = RoundDecimalNumber((instalmentvalue * noofinstalmentvalue) - totaltransactionvalue)
    //difference=RoundDecimalNumber(totaltransactionvalue / noofinstalmentvalue)-RoundDecimalNumber(totaltransactionvalue / noofinstalmentvalue)
    var Grid = igtbl_getGridById('UltraWebTab1__ctl0_uwgScheduleTemplet')
    var InstalmentAmount = igtab_getElementById("txtInstalmentAmount", webTab.element);
    InstalmentAmount.value = instalmentvalue;

    var intPaidInstalment = 0;
    if (Grid.Rows.length > 0) {
        //DeleteRow()
        var RowIndices = "_";
        for (iCount = 0; iCount < Grid.Rows.length; iCount++) {
            var Row = Grid.Rows.rows[iCount];
            if (Row == null || Row == undefined)
                Row = igtbl_getRowById(Grid.Id + "_r_" + iCount);
            var RowId = Row.Id;
            var Str = RowId.split("_");
            var Cell = igtbl_getCellById(Str[0] + "_rc_" + Str[2] + "_3");
            if (Cell != null)
                if (Cell.getValue() != "True")
                RowIndices = RowIndices + Str[2] + "_";
            else
                intPaidInstalment++;
        }
        var RowIndexs = RowIndices.split("_")
        for (iCount2 = 0; iCount2 < RowIndexs.length; iCount2++) {
            if (RowIndexs[iCount2] != "")
                igtbl_deleteRow(Grid.Id, Grid.Id + "_r_" + RowIndexs[iCount2]);
        }

    }
    //DeleteRow();



    var yearindex = 0;
    var monthindex = 0;
    var dayindex = 0;

    //-------------------------------0257 MODIFIED-----------------------------------------
    /*var currentmonth            = Math.abs(Arr[0]);
    var currentday              = Math.abs(Arr[1]);
    var currentyear             = Math.abs(Arr[2]);*/
    var currentyear = Math.abs(Arr[0]);
    currentmonth = Math.abs(Arr[1]);
    var currentday = Math.abs(Arr[2]);
  
    
    //-------------------------------=============-----------------------------------------

    loading = 1;

    for (intcounter = 0; intcounter < noofinstalmentvalue; intcounter++) {

        var currentrow = igtbl_addNew('UltraWebTab1__ctl0_uwgScheduleTemplet', 0, true, true)
        var firstcell = currentrow.cells[0]
        var secondcell = currentrow.cells[1]
        var thirdCell = currentrow.cells[2]


        if (currentmonth >= 12 && intcounter > 0) {
            currentyear = currentyear + 1
            currentmonth = 1
        }
        else {
            if (intcounter == 0) {
                currentmonth = currentmonth
            }
            else {
                currentmonth = currentmonth + 1
            }

        }

        firstcell.setValue(instalmentvalue)

        //-------------------------------0257 MODIFIED-----------------------------------------
        var newInstalmentDate = new Date();
        newInstalmentDate.setFullYear(currentyear, (currentmonth - 1), currentday);
        //newInstalmentDate.setFullYear(currentyear, currentday, currentmonth );
  
        secondcell.setValue(newInstalmentDate);
        //secondcell.setValue("26/08/2019");
        //1
        //var ss = newInstalmentDate.toLocaleString()
        //secondcell.setValue(ss);
        //alert(ss);
        //secondcell.setValue(currentday + '/' + currentmonth + '/' + currentyear)
        //-------------------------------=============-----------------------------------------
        

        thirdCell.setValue("False");
        if (noofinstalmentvalue == 1) {
            var Cellv = instalmentvalue;
            firstcell.setValue(RoundDecimalNumber(Cellv - difference));
            continue;
        }
        if (intcounter == noofinstalmentvalue - 1) {
            var cell = Grid.Rows.rows[intcounter].cells[0]
            var Cellv = cell.getValue();
            var diff = RoundDecimalNumber((difference * noofinstalmentvalue));
            firstcell.setValue(RoundDecimalNumber(Cellv - difference));
            continue;
        }
    }
    noofinstalment.value = Grid.Rows.length - intPaidInstalment;
    loading = 0;

    var ofooterInstalment = window.document.all.item("lblCurrentInstalmentvalue");
    var ofooter = window.document.all.item("lbltotalTransactionvalue");
    ofooter.innerText = 0
    ofooterInstalment.innerText = RoundDecimalNumber(totaltransactionvalue)
    ofooterInstalment.style.color = ""

    var Transval = 0;
    if (totaltransaction.disabled == false) {
        Transval = RoundDecimalNumber(Math.abs(totaltransaction.value))
    }
    else {
        Transval = RoundDecimalNumber(Math.abs(totaltransactionremain.value))
    }

    if (RoundDecimalNumber(Math.abs(totaltransactionvalue)) == RoundDecimalNumber(Transval)) {
        //item.setEnabled(true);

    }
    else {
        //item.setEnabled(false);
    }


}




function uwgScheduleTemplet_AfterCellUpdateHandler(gridName, cellId) {
   
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var grid = igtbl_getGridById(gridName)
    var length = grid.Rows.length
    var totalvalue = 0;
    var cell;
    var totaltransactionvalue = 0;
    var totaltransaction = igtab_getElementById("txtTransactionAmount", webTab.element);
    var totaltransactionremain = igtab_getElementById("txtTransactionValue", webTab.element);
    if (totaltransaction.disabled == false) {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransaction.value))
    }
    else {
        totaltransactionvalue = RoundDecimalNumber(Math.abs(totaltransactionremain.value))
    }
    //var item = igtbar_getItemById("TlbMainToolbar_Item_2")

    var startDate = igtab_getElementById("WebDateChooser1", webTab.element)
    var ccell = igtbl_getCellById(cellId)


    if (ccell.Column.Index == 1) {
        if (ccell.getValue() < startDate.value || ccell.getValue() == "Null") {
            ccell.setValue(ccell._oldValue)
        }
    }

    if (loading == 0) {

        for (intcounter = 0; intcounter < length; intcounter++) {
            //   cell            =   grid.Rows.rows[intcounter].cells[0]
            cell = igtbl_getCellById(gridName + "_rc_" + intcounter + "_0");
            if (cell == "undefined" || cell == null) {
                cell = grid.Rows.rows[intcounter].cells[0];
            }
            if (cell.getValue() == null) { }
            else
                totalvalue = totalvalue + cell.getValue();
            //                    if(intcounter == length-1)
            //                      totalvalue = RoundDecimalNumber(totalvalue -(difference*length));
            //                    
        }

        var ofooterInstalment = window.document.all.item("lblCurrentInstalmentvalue");
        var ofooter = window.document.all.item("lbltotalTransactionvalue");

        ofooterInstalment.innerText = RoundDecimalNumber(totalvalue)
        ofooter.innerText = RoundDecimalNumber(RoundDecimalNumber(totaltransactionvalue) - RoundDecimalNumber(totalvalue))

        var ofooterInstalmentvalue = RoundDecimalNumber(Math.abs(ofooterInstalment.innerText))
        var ofootervalue = RoundDecimalNumber(Math.abs(totaltransactionvalue))

        if (ofootervalue != ofooterInstalmentvalue) {
            ofooterInstalment.style.color = "red"
          //  item.setEnabled(false)
        }
        else {
            ofooterInstalment.style.color = ""
           // item.setEnabled(true)
        }

    }


}



//'''
function txtTransactionValue_Change() {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    //var lbVal       = window.document.getElementById("lblCurrentInstalmentvalue");
    var txtVal = igtab_getElementById("txtTransactionAmount", webTab.element);
    var grid = igtbl_getGridById("UltraWebTab1__ctl0_uwgScheduleTemplet");
    var txtvalue = igedit_getById("UltraWebTab1__ctl0_txtTransactionValue");
    var txtAmountVal = igedit_getById("UltraWebTab1__ctl0_txtTransactionAmount");
    var length = grid.Rows.length;
    var item = igtbar_getItemById("TlbMainToolbar_Item_2")

    var transVal = txtVal.value - 1;
    transVal = transVal + 1;
    transVal = RoundDecimalNumber(transVal);
    txtvalue.setValue(transVal);
    txtAmountVal.setValue(transVal);
    var totalvalue = 0;

    for (intcounter = 0; intcounter < length; intcounter++) {
        cell = grid.Rows.rows[intcounter].cells[0]
        if (cell.getValue() != null)
        { totalvalue = RoundDecimalNumber(totalvalue + cell.getValue()) }
    }

    if (transVal != totalvalue && length > 0) {
        alert("Transaction Amount changed ,Please recreate Instalments Amounts");
        item.setEnabled(false)
    }
    else if (transVal == totalvalue && length > 0) {
        item.setEnabled(true)
    }
}



function frmEmpLoansWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent) {

    var oCombo = igdrp_getComboById("UltraWebTab1__ctl0_txtTransactionDate");
    if (newValue < oCombo.getValue()) {
        oDateChooser.setValue(oCombo.getValue());
    }
    if (newValue == null) {
        oDateChooser.setValue(oCombo.getValue());
    }
}

function txtTransactionDate_ValueChanged(oDateChooser, newValue, oEvent) {

    if (newValue == null) {
        oDateChooser.setValue(oDateChooser._oldV);
    }
    else {
        var oCombo = igdrp_getComboById("UltraWebTab1__ctl0_WebDateChooser1");
        oCombo.setValue(newValue);
    }
}

function frmEmpLoanstxtInstalmentAmount_ValueChange(oEdit, oldValue, oEvent) {
    ReCalculateAmount();
}
//===================== frmEmployeeLoans [ End ]


//   ========================================================================
//   ProcedureName  :  btnOpenSettlements_Click 
//   Screen         :  frmEmployeeLoans
//   Project        :  Venus V.
//   Description    :  open the Settlements Screen.
//   Developer      :  [0260]
//   Date Created   :  19-05-2008
//   ///////////////////////////////// 

function btnOpenSettlements_Click() {
    //Add code to handle your event here.
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    
    var ctrlId = window.document.getElementById("hdnID");

    var hdnSettelment = window.document.getElementById("hdnSettelment");
    hdnSettelment.value = "1"

    if (ctrlId.value != 0) {
        var winopen = window.open("frmPayReceive.aspx?ID=" + ctrlId.value + "&Mode=E", "_Parent" + 1, "height=490,width=700,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        winopen.document.focus();
    }

}



function uwgEmployeeLoans_AfterRowActivateHandler(gridName, id) {
    var Grid = igtbl_getGridById(gridName);
    var row = igtbl_getRowById(id)
    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
        PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermissionEmployeeLoans, OnSucceeded, OnFailed)
    }
    else
        return false;



}
var Confirm = false;
function uwgEmployeeLoans_BeforeRowDeActivateHandler(gridName, rowId) {
    var row = igtbl_getRowById(rowId)
    var grid = igtbl_getGridById(gridName)
    Confirm = false;
    if (isFormChanged() == true) {
        var msg = returnDiscardMsg();
        Confirm = true;
        if (window.confirm(msg)) {

            IsDataChanged = "F";

            return 0;
        }
        else {

            return 1;
        }
    }

}


function callback_GetRecordPermissionEmployeeLoans(res) {
    var saveItem = window.document.all.item("ImageButton_Save")
    var saveItemN1 = window.document.all.item("ImageButton_SaveN")
    var saveItemN2 = window.document.all.item("LinkButton_SaveN")
    var delItem = window.document.all.item("ImageButton_Delete")
    var printItem = window.document.all.item("ImageButton_Print")

    if (res == null) return;

    var arr = res.split(",");
    if (arr[0] == "1") {
        saveItem.disabled = false;
        saveItemN1.disabled = false;
        saveItemN2.disabled = false;
    }
    else {
        saveItem.disabled = true;
        saveItemN1.disabled = true;
        saveItemN2.disabled = true;
    }

    if (arr[1] == "1")
        delItem.disabled = false;
    else
        delItem.disabled = true;

    if (arr[2] == "1")
        printItem.disabled = false;
    else
        printItem.disabled = true;


}

function TrimTo2Decimal(str) {


    var retStr = ""
    var arr = str.split('.')
    retStr = arr[0]
    if (arr.length > 1) {
        retStr = retStr + "."
        if (arr[1].length > 2) {
            retStr = retStr + arr[1].substr(0, 2)
        }
        else {
            retStr = retStr + arr[1]
        }
    }
    return retStr;
}


function RoundDecimalNumber(dblNumber) {
    dblNumber = dblNumber.toString();
    var arrNumber;
    var IntNumber;
    var DecimalNumber = 0;
    var TempDecimal;
    var intLenght = 0;
    var tempNumber;
    var ReturnNumber;
    var intMul = 1;
    if (dblNumber.indexOf("-") != -1) {
        intMul = -1;
    }
    else {
        intMul = 1;
    }
    dblNumber = Math.abs(dblNumber).toString();
    if (dblNumber.indexOf(".") != -1) {
        arrNumber = dblNumber.split(".");
        IntNumber = ConvertToNumber(arrNumber[0]);
        intLenght = arrNumber[1].length;
        var arrdecimal;
        if (intLenght > 2) {
            DecimalNumber = arrNumber[1].toString().substring(0, 2);
            TempDecimal = ConvertToNumber(arrNumber[1].toString().substring(2, 3));
            if (TempDecimal > 5) {
                DecimalNumber = ConvertToNumber(DecimalNumber) + 1;
                DecimalNumber = ConvertToNumber(DecimalNumber)
                if (DecimalNumber >= 100) {
                    tempNumber = ConvertToNumber(DecimalNumber.toString().substring(0, 1));
                    DecimalNumber = ConvertToNumber(DecimalNumber.toString().substring(1, 3));
                    IntNumber = ConvertToNumber(IntNumber) + ConvertToNumber(tempNumber);
                }
                else if (DecimalNumber < 10) {
                    DecimalNumber = "0" + DecimalNumber.toString()

                }
            }

        }
        else {
            DecimalNumber = arrNumber[1];
        }
        return ReturnNumber = parseFloat(ConvertToNumber(IntNumber) + "." + DecimalNumber) * intMul;
    }
    else {
        return parseFloat(dblNumber) * intMul;
    }
}


function OnSucceeded(result, userContext, methodName) {
    if (methodName == '') {

    }
}
function OnFailed(error) {
    alert(error.get_message());
}

