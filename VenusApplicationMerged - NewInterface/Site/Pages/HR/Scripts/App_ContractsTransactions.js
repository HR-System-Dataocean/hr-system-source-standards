
var currentrow;
var intTransactionTypeID;
var ctrGridId;
var intRowId;

var DdlTransactionType;
var hdnTransactionTypeID;
var txtAmount;
var DdlActive;
var ddlIntervalType;
var ddlPaidAtVacation;
var ddlOncePerPeriod;
var ctrActiveDate;
var ctrGrid;
var lang;
var ContractsTransactoionsgrid;
var uwgContractsTransactoions;
var hdnCurrDate;

var ctrlFormula;
var ctrlFormulaDesc;
var ctrlDiv;
var ctrFormula;
var ctrFormulaDesc;
var Formula;
var FormulaDesc;

//var PopupContract_Panel = "WebAsyncRefreshPanel1";
//var PopupContract_Id = "OfficePopup_ContractTrans_";
//var PopupContract$Id = "OfficePopup_ContractTrans$";
//var PopupContractId = "OfficePopup_ContractTrans";
//var PopupContract_txtId = "igtxtOfficePopup_ContractTrans_";
var uwgContractsTransactoionsId = "UltraWebTab1__ctl0_uwgContractsTransactoions";

function uwgContractsTransactoions_AfterRowActivateHandler(gridName, rowId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var Grid = igtbl_getGridById(gridName);
    var row = igtbl_getRowById(rowId);

    var ctrId = igtab_getElementById("txtId", webTab.element);
    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
        //PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermission, OnSucceeded, OnFailed);
        //PageMethods.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfo, OnSucceeded, OnFailed);
    }
}

function uwgContractsTransactoions_AfterSelectChangeHandler(gridName, id) {


    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var hdnTransactionTypeId = window.document.getElementById("hdnTransactionTypeID");
    var ctrId = window.document.getElementById("txtId");
    var ctrTransactionType = igtab_getElementById("DdlTransactionType", webTab.element);
    var ctrAmount = igtab_getElementById("txtAmount", webTab.element);
    var ctrStatus = igtab_getElementById("DdlActive", webTab.element);
    var ctrIntervalType = igtab_getElementById("ddlIntervalType", webTab.element);
    var ctrPaidAtVacation = igtab_getElementById("ddlPaidAtVacation", webTab.element);
    var ctrOncePerPeriod = igtab_getElementById("ddlOncePerPeriod", webTab.element);
    var ctrActiveDate = igtab_getElementById("wdcActiveDate", webTab.element);
    var delItem = igtab_getElementById("btnDelete", webTab.element);
    
    //var PopupContract = window.document.all.item(PopupContract_Id + PopupContract_Panel);
    
//    var hdnTransactionTypeId = window.document.getElementById("hdnTransactionTypeID");
//    var ctrId = window.document.getElementById("txtId");
//    var ctrTransactionType =  PopupContract.all.item(PopupContract$Id + "DdlTransactionType");
//    var ctrAmount = PopupContract.all.item(PopupContract_txtId + "txtAmount"); 
//    var ctrStatus = PopupContract.all.item(PopupContract$Id + "DdlActive"); 
//    var ctrIntervalType = PopupContract.all.item(PopupContract$Id + "ddlIntervalType"); 
//    var ctrPaidAtVacation = PopupContract.all.item(PopupContract$Id + "ddlPaidAtVacation"); 
//    var ctrOncePerPeriod = PopupContract.all.item(PopupContract$Id + "ddlOncePerPeriod"); 
//    var ctrActiveDate = PopupContract.all.item(PopupContract_Id + "wdcActiveDate"); 
//    var delItem = PopupContract.all.item(PopupContract$Id + "btnDelete"); 

    ctrGrid = igtbl_getGridById(gridName);
    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(id);

    //Get Row Data
    var IntID = Row.getCell(0).getValue();
    var IntTransactionTypeId = Row.getCell(10).getValue();
    var blActive = Row.getCell(11).getValue();
    var dblAmount;
    if (Row.getCell(3).getValue() == null)
        dblAmount = 0;
    else
        dblAmount = Row.getCell(3).getValue();
    var blPaidOnce = Row.getCell(14).getValue();
    var blPaidAtVacation = Row.getCell(13).getValue();
    var IntIntervalType;
    if (Row.getCell(12).getValue() == null)
        IntIntervalType = 0;
    else
        IntIntervalType = Row.getCell(12).getValue();
    var RegUserID = Row.getCell(5).getValue();
    var RegDate = Row.getCell(6).getValue();
    var CancelDate = Row.getCell(7).getValue();
    var activeDate = Row.getCellFromKey("ActiveDate").getElement().innerText;


    //Boolean Variables Conversions
    if (blActive == true)
        blActive = 0;
    else
        blActive = 1;

    if (blPaidOnce == true)
        blPaidOnce = 0;
    else
        blPaidOnce = 1;

    //Set Row Data To Controls  
    ctrId.value = IntID;
    ctrTransactionType.value = IntTransactionTypeId;
    hdnTransactionTypeId.value = IntTransactionTypeId;
    ctrAmount.value = dblAmount;
    ctrStatus.value = blActive;
    ctrStatus.selectedIndex = blActive;
    ctrIntervalType.value = IntIntervalType;
    ctrPaidAtVacation.value = blPaidAtVacation;
    ctrPaidAtVacation.selectedIndex = blPaidAtVacation;
    ctrOncePerPeriod.value = blPaidOnce;
    ctrOncePerPeriod.selectedIndex = blPaidOnce;
    ctrActiveDate.Object.setValue(activeDate);

    ddlTrans_IndexChanged("DdlTransactionType");


    if (CancelDate != null) {
        delItem.enabled = false;
    }

    ctrTransactionType.disabled = true;

}

function FormatDate(dte1) {
    if (dte1 == null || dte1 == "")
        return "";
    var strArr = dte1.split(" ")[0].split("/");
    strReturn = strArr(0) + "/" + strArr(1) + "/" + strArr(2);
    return strReturn;
}


function ddlTrans_IndexChanged(ctrlID) {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrl = igtab_getElementById(ctrlID, webTab.element);
    var ctrlFormula = igtab_getElementById("lblFormula", webTab.element);
    var ctrlFormulaDesc = igtab_getElementById("lblFormulaDesc", webTab.element);
    var ctrFormula = igtab_getElementById("lbFormula", webTab.element);
    var ctrFormulaDesc = igtab_getElementById("lbFormulaDesc", webTab.element);
    var ctrlDiv = igtab_getElementById("DivFormula", webTab.element);

//    var PopupContract = window.document.all.item(PopupContract_Id + PopupContract_Panel);

//    var ctrl = PopupContract.all.item(PopupContract$Id + ctrlID); 
//    var ctrlFormula = PopupContract.all.item(PopupContract_Id + "lblFormula"); 
//    var ctrlFormulaDesc = PopupContract.all.item(PopupContract_Id + "lblFormulaDesc"); 
//    var ctrFormula = PopupContract.all.item(PopupContract_Id + "lbFormula"); 
//    var ctrFormulaDesc = PopupContract.all.item(PopupContract_Id + "lbFormulaDesc");
//    var ctrlDiv = PopupContract.all.item(PopupContract_Id + "DivFormula"); 

    if (ctrl.value != 0)
        getFormula(ctrl.value);
    else {
        ctrFormulaDesc.value = "";
        ctrFormulaDesc.innerText = "";
        ctrFormula.value = "";
        ctrFormula.innerText = "";

        ctrlDiv.style.borderRight = "";
        ctrlDiv.style.borderTop = "";
        ctrlDiv.style.borderLeft = "";
        ctrlDiv.style.borderBottom = "";

        ctrlFormula.value = "";
        ctrlFormula.innerText = "";
        ctrlFormulaDesc.value = "";
        ctrlFormulaDesc.innerText = "";
    }

}

function getFormula(val) {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

     ctrlFormula = igtab_getElementById("lblFormula", webTab.element);
     ctrlFormulaDesc = igtab_getElementById("lblFormulaDesc", webTab.element);
     ctrFormula = igtab_getElementById("lbFormula", webTab.element);
     ctrFormulaDesc = igtab_getElementById("lbFormulaDesc", webTab.element);
     ctrlDiv = igtab_getElementById("DivFormula", webTab.element);

    var lang = window.document.getElementById("hdnLang").value;
    Formula = "";
    FormulaDesc = "";
    if (lang == 1) {
        Formula = "الصيغة"//"&#1575;&#1604;&#1605;&#1593;&#1575;&#1583;&#1604;&#1577;";
        FormulaDesc = "الوصف"//"&#1575;&#1604;&#1608;&#1589;&#1601;";

    }
    else {
        Formula = "Formula";
        FormulaDesc = "Description";

    }


    if (val != "")
        PageMethods.GetFormula(val, lang, OnSucceeded, OnFailed);

}

function checkBasicSalary(transactionId) {

    PageMethods.IsBasicSalaryTransaction(transactionId, OnSucceeded, OnFailed);

}

function NavigationToolBar_Click() {
    blSave = false;
}


function uwgContractsTransactoions_BeforeRowActivateHandler(gridName, rowId) {
    var row = igtbl_getRowById(rowId);
    if (isFormChanged()) {
        var msg = returnDiscardMsg();
        if (window.confirm(msg)) {
            IsDataChanged = "F";
        }
        else {
            return 1
        }
    }
}


//*******************************************************
function callback_GetRecordPermission(res) {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var saveItem = igtab_getElementById("btnSave", webTab.element);
    var delItem = igtab_getElementById("btnDelete", webTab.element);
    var strFormPermission = igtab_getElementById("txtFormPermission", webTab.element);

    saveItem.enabled = true;
    delItem.enabled = true;

    if (strFormPermission != "") {
        var arrform = strFormPermission.split(",");
        if (arrform[0] != "0")
            saveItem.enabled = false;
        if (arrform[1] != "0")
            delItem.enabled = false;
    }


    var arr = res.split(",");
    if (arr[0] != "1")
        saveItem.enabled = false;
    if (arr[1] != "1")
        delItem.enabled = false;
}

function ToolBarOperations() {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtId");
    lang = window.document.getElementById("hdnLang").value;
    hdnCurrDate = window.document.getElementById("hdnCurrDate");
    hdnTransactionTypeID = window.document.getElementById("hdnTransactionTypeID");

    DdlTransactionType = igtab_getElementById("DdlTransactionType", webTab.element);
    txtAmount = igtab_getElementById("txtAmount", webTab.element);
    DdlActive = igtab_getElementById("DdlActive", webTab.element);
    ddlIntervalType = igtab_getElementById("ddlIntervalType", webTab.element);
    ddlPaidAtVacation = igtab_getElementById("ddlPaidAtVacation", webTab.element);
    ddlOncePerPeriod = igtab_getElementById("ddlOncePerPeriod", webTab.element);
    ctrActiveDate = igtab_getElementById("wdcActiveDate", webTab.element);
    ContractsTransactoionsgrid = uwgContractsTransactoionsId;
    uwgContractsTransactoions = igtbl_getGridById(ContractsTransactoionsgrid);
    ctrGrid = igtbl_getGridById(ContractsTransactoionsgrid);


//    DdlTransactionType = PopupContract.all.item(PopupContract$Id + "DdlTransactionType");
//    txtAmount = PopupContract.all.item(PopupContract_txtId + "txtAmount");
//    DdlActive = PopupContract.all.item(PopupContract$Id + "DdlActive");
//    ddlIntervalType = PopupContract.all.item(PopupContract$Id + "ddlIntervalType");
//    ddlPaidAtVacation = PopupContract.all.item(PopupContract$Id + "ddlPaidAtVacation");
//    ddlOncePerPeriod = PopupContract.all.item(PopupContract$Id + "ddlOncePerPeriod");
//    ctrActiveDate = PopupContract.all.item(PopupContract_Id + "wdcActiveDate");
//    ContractsTransactoionsgrid = PopupContract.all.item(uwgContractsTransactoionsId);
//    uwgContractsTransactoions = igtbl_getGridById(ContractsTransactoionsgrid);
//    ctrGrid = igtbl_getGridById(ContractsTransactoionsgrid.id);

    intTransactionTypeID = hdnTransactionTypeID.value;
}

function btnSave() {
    ToolBarOperations();
    var txtId = window.document.getElementById("txtId");

    if (DdlTransactionType.value == "" || DdlTransactionType.value == "0") {
        if (lang == 1)
            alert("الرجاء اختيار نوع البند أولا");
        else
            alert("Please Choose Transaction Type First");
        return 0;
    }
    if (txtId.value > 0) {
        intRowId = GetGridIndex(txtId.value);
        PageMethods.EditRecordInSession(DdlTransactionType.value, lang, txtId.value, txtAmount.value, DdlTransactionType.value, DdlActive.value, ddlIntervalType.value, ddlPaidAtVacation.value, ddlOncePerPeriod.value, ctrActiveDate.value, OnSucceeded, OnFailed);
    }
    else {
        PageMethods.AddRecordToSession(DdlTransactionType.value, lang, txtAmount.value, DdlTransactionType.value, DdlActive.value, ddlIntervalType.value, ddlPaidAtVacation.value, ddlOncePerPeriod.value, intTransactionTypeID, ctrActiveDate.value, OnSucceeded, OnFailed);
        ctrGridId = ctrGrid.Id;

    }
}

function btnDelete() {
    ToolBarOperations()
    var txtId = window.document.getElementById("txtId");

    if (txtId.value > 0) {
        PageMethods.GetRecordPermissionAjax(txtId.value, OnSucceeded, OnFailed);
        intRowId = GetGridIndex(txtId.value);

    }
    else {
        if (lang == 1)
            alert("لا يمكنك حذف هذا البند");
        else
            alert("Sorry, You Can't Delete This Transaction");
    }
}

function btnNew() {
    ToolBarOperations()
    var txtId = window.document.getElementById("txtId");
    txtId.value = "";
    DdlTransactionType.selectedIndex = 0;
    hdnTransactionTypeID.value = 0;
    txtAmount.value = 0;
    DdlActive.selectedIndex = 0;
    ddlIntervalType.selectedIndex = 0;
    ddlPaidAtVacation.selectedIndex = 0;
    ddlOncePerPeriod.selectedIndex = 0;
    ctrActiveDate.value = hdnCurrDate.value;
    ddlTrans_IndexChanged("DdlTransactionType");
    DdlTransactionType.disabled = false;
}



function OnSave(currentrow, transCode, transName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var Employeecode = igtab_getElementById("lblDescEmployeeCode", webTab.element);
    currentrow.getCellFromKey("TransactionCode").setValue(transCode);
    currentrow.getCellFromKey("TransactionName").setValue(transName);
    currentrow.getCellFromKey("Amount").setValue(txtAmount.value);
    currentrow.getCellFromKey("TransactionTypeID").setValue(DdlTransactionType.value);
    currentrow.getCellFromKey("Active").setValue(DdlActive.value);
    currentrow.getCellFromKey("IntervalID").setValue(ddlIntervalType.value);
    currentrow.getCellFromKey("PaidAtVacation").setValue(ddlPaidAtVacation.value);
    currentrow.getCellFromKey("OnceAtPeriod").setValue(ddlOncePerPeriod.value);
    var strArr = ctrActiveDate.value.split("-");
    var strMdy = strArr[1] + "/" + strArr[2] + "/" + strArr[0];
    currentrow.getCellFromKey("ActiveDate").setValue(strMdy);
    SetGridValue(Employeecode.innerText, txtAmount.value, transCode);

}


function GetGridIndex(intId) {
    var Counter = 0;
    var ctrGrid = igtbl_getGridById(ContractsTransactoionsgrid);
    var strRowID;
    if (ctrGrid.Rows.length > 0) {
        for (Counter = 0; Counter <= ctrGrid.Rows.length - 1; Counter++) {
            if (ctrGrid.Rows.rows[Counter] != undefined) {
                if (ctrGrid.Rows.rows[Counter].cells[0] != undefined) {
                    var valCell = ctrGrid.Rows.rows[Counter].cells[0].getValue();
                    if (valCell != null && valCell != "" && valCell == intId) {
                        return ctrGrid.Rows.rows[Counter].Id;
                    }
                }
            }
        }
    }
}


/* =============================================
-- Author        : [0261]
-- Date Created  : 10-02-2009 
-- Description   : Set Contract Transaction Amount to the calculated amount 
==============================================*/

function SetGridValue(strEmpCode, Amount, TransactionCode) {


    for (i = 0; i < ctrGrid.Rows.length; i++) {
        var currentrow;
        currentrow = igtbl_getRowById(ctrGrid.Id + "_r_" + i);
        PageMethods.RecalculateValue(strEmpCode, Amount, TransactionCode, currentrow.getCellFromKey("TransactionTypeID").getValue(), OnSucceeded, OnFailed);

    }
}


function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'EditRecordInSession') {
        var arr = result.split("$$");

        var transCode = arr[1];
        var transName = arr[2];

        currentrow = igtbl_getRowById(intRowId);
        OnSave(currentrow, transCode, transName);
        //btnNew();
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'AddRecordToSession') {
        var arr = result.split("$$");

        var intId = arr[0];
        var transCode = arr[1];
        var transName = arr[2];
        if (intId == -1) {
            if (lang == 1)
                alert("غير مسموح تكرار البند");
            else
                alert("Not allow to entre duplicate transactions");
           
            return;
        }
        currentrow = igtbl_addNew(ctrGridId, 0, true, true);
        currentrow.cells[0].setValue(intId);

        OnSave(currentrow, transCode, transName);
        btnNew();
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'GetRecordPermissionAjax') {
        if (ctrGrid.Rows.length > 0) {
            currentrow = igtbl_getRowById(intRowId);
            var strPermission;
            strPermission = result;
            var arrPermission = strPermission.split(",");
            if (arrPermission[1] == "1") {
                PageMethods.DeleteRecordfromSession(currentrow.cells[0].getValue(), intTransactionTypeID, OnSucceeded, OnFailed);
            }
        }
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'DeleteRecordfromSession') {
        currentrow.remove()
        if (ctrGrid.Rows.length == 0) {

            //btnNew();
            blIsNew = false;
            blSave = false;
        }
        else {

            uwgContractsTransactoions_AfterSelectChangeHandler(ctrGrid.Id, ctrGrid.Id + '_r_0')
        }
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'IsBasicSalaryTransaction') {
        var res = '';
        res = result;
        if (res == "True") {
            return true;
        }
        else {
            return false;
        }
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'RecalculateValue') {
        var incomingAmount;
        incomingAmount = result;
        if (incomingAmount != 0 && incomingAmount != null) {
            currentrow.getCellFromKey("Amount").setValue(incomingAmount);
        }
    }
    //----------------------------------------------------------------------------------------
    else if (methodName == 'GetFormula') {
        var res = '';
        res = result;

        if (res != "" && res != "=" && res != null) {
            var arrres = res.split("=");
            ctrFormulaDesc.value = FormulaDesc;
            ctrFormulaDesc.innerText = FormulaDesc;
            ctrFormula.value = Formula;
            ctrFormula.innerText = Formula;

            //            ctrlDiv.style.borderRight = " 1px solid";
            //            ctrlDiv.style.borderTop = " 1px solid";
            //            ctrlDiv.style.borderLeft = " 1px solid";
            //            ctrlDiv.style.borderBottom = " 1px solid";

            ctrlFormulaDesc.value = "[ " + arrres[1] + " ]";
            ctrlFormulaDesc.innerText = "[ " + arrres[1] + " ]";
            ctrlFormula.value = "[ " + arrres[0] + " ]";
            ctrlFormula.innerText = "[ " + arrres[0] + " ]";

        }
        else {
            ctrFormulaDesc.value = "";
            ctrFormulaDesc.innerText = "";
            ctrFormula.value = "";
            ctrFormula.innerText = "";

            ctrlDiv.style.borderRight = "";
            ctrlDiv.style.borderTop = "";
            ctrlDiv.style.borderLeft = "";
            ctrlDiv.style.borderBottom = "";


            ctrlFormulaDesc.value = "";
            ctrlFormulaDesc.innerText = "";
            ctrlFormula.value = "";
            ctrlFormula.innerText = "";
        }
    }
    else if (methodName == 'GetRelatedDepartment') {
        strBranches = result.split("|")[0];
        strSectors = result.split("|")[1];

        for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
            var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j);
            currRow.setHidden(false);
            currRow.getCellFromKey("V").setValue("");
        }
        if (strBranches != null && strBranches != "") {
            var arrBranches = strBranches.split(',');
            for (i = 0; i < arrBranches.length; i++) {
                for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
                    var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j);
                    if (ConvertToNumber(arrBranches[i]) == currRow.getCellFromKey("ID").getValue()) {
                        currRow.setHidden(false);
                        currRow.getCellFromKey("V").setValue("T");
                    }
                    else {
                        if (currRow.getCellFromKey("V").getValue() != "T" && currRow.getCellFromKey("ID").getValue() != 0)
                            currRow.setHidden(true);
                    }
                } //End for (j = 0; j < ddlBranch.length ...
            } //End for (i = 0; i < arrBranches.length ...     
        }
        else {
            for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
                var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j)
                if (currRow.getCellFromKey("ID").getValue() != 0)
                    currRow.setHidden(true);
            }
        }

        //----------------------------------------------------------------ddlSectors
        for (j = 0; j < ddlSectors.grid.Rows.length; j++) {
            var currRow = igtbl_getRowById(ddlSectors.grid.Id + "_r_" + j);
            currRow.setHidden(false);
            currRow.getCellFromKey("V").setValue("");
        }
        if (strSectors != null && strSectors != "") {
            var arrBranches = strSectors.split(',');
            for (i = 0; i < arrBranches.length; i++) {
                for (j = 0; j < ddlSectors.grid.Rows.length; j++) {
                    var currRow = igtbl_getRowById(ddlSectors.grid.Id + "_r_" + j);
                    if (ConvertToNumber(arrBranches[i]) == currRow.getCellFromKey("ID").getValue()) {
                        currRow.setHidden(false);
                        currRow.getCellFromKey("V").setValue("T");
                    }
                    else {
                        if (currRow.getCellFromKey("V").getValue() != "T" && currRow.getCellFromKey("ID").getValue() != 0)
                            currRow.setHidden(true);
                    }
                } //End for (j = 0; j < ddlSectors.length ...
            } //End for (i = 0; i < arrBranches.length ...     
        }
        else {
            for (j = 0; j < ddlSectors.grid.Rows.length; j++) {
                var currRow = igtbl_getRowById(ddlSectors.grid.Id + "_r_" + j)
                if (currRow.getCellFromKey("ID").getValue() != 0)
                    currRow.setHidden(true);
            }
        }


    }
}

function OnFailed(error) {
    //alert(error);
}