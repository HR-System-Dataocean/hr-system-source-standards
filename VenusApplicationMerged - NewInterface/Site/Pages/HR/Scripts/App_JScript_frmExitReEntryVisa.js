var rbtnResidentIssue
//-----------------
var chkNew
var chkLost
var chkReNew
var chkDamaged
//=================
var rbtnTravelVisa
//-----------------
var chkTravelOnce
var chkTravelMore
var chkFinalExit
//=================
var rbtnTransferBail
//-----------------
var chkFirstTime
var chkSecondTime
var chkThirdTime
//=================
var rbtnAddDependant
//==============================
var chkResidentGovernment
var chkResidentInstitutions
var chkResidentCompanies
var chkResidentPersons

var chkTransferBailGovernment
var chkTransferBailInstitutions
var chkTransferBailCompanies
var chkTransferBailPersons

function VisaTypeFirst(strCtrlID) {
    var hdnFirstTime = window.document.getElementById("hdnFirstTime")
    if (hdnFirstTime.value == "1") {
        VisaTypeChecked(strCtrlID)
        hdnFirstTime.value = "0"
    }
}

function VisaTypeChecked(strCtrlID) {

    var webTab = igtab_getTabById("UltraWebTab1")
    
    rbtnResidentIssue = igtab_getElementById("rbtnResidentIssue", webTab.element)

    chkNew = igtab_getElementById("chkNew", webTab.element)
    chkLost = igtab_getElementById("chkLost", webTab.element)
    chkReNew = igtab_getElementById("chkReNew", webTab.element)
    chkDamaged = igtab_getElementById("chkDamaged", webTab.element)
    //**********
    rbtnTravelVisa = igtab_getElementById("rbtnTravelVisa", webTab.element)

    chkTravelOnce = igtab_getElementById("chkTravelOnce", webTab.element)
    chkTravelMore = igtab_getElementById("chkTravelMore", webTab.element)
    chkFinalExit = igtab_getElementById("chkFinalExit", webTab.element)
    //**********
    rbtnTransferBail = igtab_getElementById("rbtnTransferBail", webTab.element)

    chkFirstTime = igtab_getElementById("chkFirstTime", webTab.element)
    chkSecondTime = igtab_getElementById("chkSecondTime", webTab.element)
    chkThirdTime = igtab_getElementById("chkThirdTime", webTab.element)
    //**********
    rbtnAddDependant = igtab_getElementById("rbtnAddDependant", webTab.element)

    switch (strCtrlID) {
        case "rbtnResidentIssue":
            ClearResidentIssue(false)
            ClearTravelVisa(true)
            ClearTransferBail(true)
            rbtnTravelVisa.checked = false;
            rbtnTransferBail.checked = false;
            rbtnAddDependant.checked = false;
            break;
        case "rbtnTravelVisa":
            ClearResidentIssue(true)
            ClearTravelVisa(false)
            ClearTransferBail(true)
            rbtnResidentIssue.checked = false;
            rbtnTransferBail.checked = false;
            rbtnAddDependant.checked = false;    
            break;
        case "rbtnTransferBail":
            ClearResidentIssue(true)
            ClearTravelVisa(true)
            ClearTransferBail(false)
            rbtnResidentIssue.checked = false;
            rbtnTravelVisa.checked = false;
            rbtnAddDependant.checked = false; 
            break;
        case "rbtnAddDependant":
            ClearResidentIssue(true)
            ClearTravelVisa(true)
            ClearTransferBail(true)
            rbtnResidentIssue.checked = false;
            rbtnTravelVisa.checked = false;
            rbtnTransferBail.checked = false;
            break;
        //Resident Issue
        case "chkNew":
            var bln = chkNew.checked
            ClearResidentIssue(false)
            chkNew.checked = bln
            break;
        case "chkLost":
            var bln = chkLost.checked
            ClearResidentIssue(false)
            chkLost.checked = bln
            break;
        case "chkReNew":
            var bln = chkReNew.checked
            ClearResidentIssue(false)
            chkReNew.checked = bln
            break;
        case "chkDamaged":
            var bln = chkDamaged.checked
            ClearResidentIssue(false)
            chkDamaged.checked = bln
            break;
        
        //Travel Visa
        case "chkTravelOnce":
            var bln = chkTravelOnce.checked
            ClearTravelVisa(false)
            chkTravelOnce.checked = bln
            break;
        case "chkTravelMore":
            var bln = chkTravelMore.checked
            ClearTravelVisa(false)
            chkTravelMore.checked = bln
            break;
        case "chkFinalExit":
            var bln = chkFinalExit.checked
            ClearTravelVisa(false)
            chkFinalExit.checked = bln
            break;
        
        //Transfer Bail
        case "chkFirstTime":
            var bln = chkFirstTime.checked
            ClearTransferBail(false)
            chkFirstTime.checked = bln
            break;
        case "chkSecondTime":
            var bln = chkSecondTime.checked
            ClearTransferBail(false)
            chkSecondTime.checked = bln
            break;
        case "chkThirdTime":
            var bln = chkThirdTime.checked
            ClearTransferBail(false)
            chkThirdTime.checked = bln
            break;
    }
}

function ClearResidentIssue(blnDisabled) {
    chkNew.disabled = blnDisabled;
    chkNew.checked = false

    chkLost.disabled = blnDisabled;
    chkLost.checked = false;

    chkReNew.disabled = blnDisabled;
    chkReNew.checked = false;

    chkDamaged.disabled = blnDisabled;
    chkDamaged.checked = false;
}

function ClearTravelVisa(blnDisabled) {
    chkTravelOnce.disabled = blnDisabled;
    chkTravelOnce.checked = false;

    chkTravelMore.disabled = blnDisabled;
    chkTravelMore.checked = false;

    chkFinalExit.disabled = blnDisabled;
    chkFinalExit.checked = false;
}

function ClearTransferBail(blnDisabled) {
    chkFirstTime.disabled = blnDisabled;
    chkFirstTime.checked = false;

    chkSecondTime.disabled = blnDisabled;
    chkSecondTime.checked = false;

    chkThirdTime.disabled = blnDisabled;
    chkThirdTime.checked = false;
}

function chkResident(ctrlID) {
    var webTab = igtab_getTabById("UltraWebTab1")
    chkResidentGovernment = igtab_getElementById("chkResidentGovernment", webTab.element)
    chkResidentInstitutions = igtab_getElementById("chkResidentInstitutions", webTab.element)
    chkResidentCompanies = igtab_getElementById("chkResidentCompanies", webTab.element)
    chkResidentPersons = igtab_getElementById("chkResidentPersons", webTab.element)

    chkResidentGovernment.checked = false;
    chkResidentInstitutions.checked = false;
    chkResidentCompanies.checked = false;
    chkResidentPersons.checked = false;

    switch (ctrlID) {
        case "chkResidentGovernment":
        chkResidentGovernment.checked = true
        break
    case "chkResidentInstitutions":
        chkResidentInstitutions .checked = true
        break
    case "chkResidentCompanies":
        chkResidentCompanies.checked = true
        break
    case "chkResidentPersons":
        chkResidentPersons.checked = true
        break 
    }
    
}

function chkTransferBail(ctrlID) {
    var webTab = igtab_getTabById("UltraWebTab1")
    chkTransferBailGovernment = igtab_getElementById("chkTransferBailGovernment", webTab.element)
    chkTransferBailInstitutions = igtab_getElementById("chkTransferBailInstitutions", webTab.element)
    chkTransferBailCompanies = igtab_getElementById("chkTransferBailCompanies", webTab.element)
    chkTransferBailPersons = igtab_getElementById("chkTransferBailPersons", webTab.element)

    chkTransferBailGovernment.checked = false;
    chkTransferBailInstitutions.checked = false;
    chkTransferBailCompanies.checked = false;
    chkTransferBailPersons.checked = false;
    
    switch (ctrlID) {
        case "chkTransferBailGovernment":
            chkTransferBailGovernment.checked = true
            break
        case "chkTransferBailInstitutions":
            chkTransferBailInstitutions.checked = true 
           break
       case "chkTransferBailCompanies":
           chkTransferBailCompanies.checked = true
            break
        case "chkTransferBailPersons":
            chkTransferBailPersons.checked = true
            break
    }
}


//Navigation

var cEnter = 13;
var cTab = 9;
var cF9 = 120;
var cUP = 38;
var cDown = 40;
var cDelete = 46;
var intNoOfEmptyRows = 2;
var bUpdate = true;
var currActiveCell = null
var currGridName;
var firstColumnKey = "Name"

function CreateEmptyRows(gridName) {
    for (i = 0; i < intNoOfEmptyRows - 1; i++)
        igtbl_addNew(gridName, 0, true, false);
}

function getNextEditCell(cellId) {
    var Cell = igtbl_getCellById(cellId)
    if (Cell == null)
        return null
    var Row = igtbl_getRowById(cellId)
    var nextCell = Cell.getNextTabCell()
    if (nextCell != null) {
        if (nextCell.isEditable())
            return nextCell
        else
            return getNextEditCell(nextCell.Id)
    }
    else {
        try {
            var nextRow = Row.getNextRow()
            if (nextRow != null)
                return getNextEditCell(nextRow.getCellFromKey(firstColumnKey).Id)
            else
                return null
        }
        catch (ex) {
            return null
        }
    }
}

function getPrevEditCell(cellId) {
    var Cell = igtbl_getCellById(cellId)
    if (Cell == null)
        return null
    var Row = igtbl_getRowById(cellId)
    var prevCell = Cell.getPrevCell()
    if (prevCell != null) {
        if (prevCell.isEditable())
            return prevCell
        else
            return getPrevEditCell(prevCell.Id)
    }
    /*else {
    try {
    var prevRow = Row.getPrevRow()
    if (prevRow != null) {
    if (prevRow.cells[prevRow.cells.length - 1].isEditable())
    return prevRow.cells[prevRow.cells.length - 1]
    else
    return getPrevEditCell(prevRow.cells[prevRow.cells.length - 1].Id)
    }
    else
    return null
    } 
    catch (ex) {
    return null
    }
    }*/
}

function getUpCell(cellId) {
    var Cell = igtbl_getCellById(cellId)
    if (Cell == null) return null
    var Row = igtbl_getRowById(cellId)
    var prevRow = Row.getPrevRow()
    var upCell = null;
    if (prevRow != null)
        upCell = prevRow.getCellFromKey(Cell.Column.Key)
    return upCell
}

function getDownCell(cellId) {
    var Cell = igtbl_getCellById(cellId)
    if (Cell == null) return null
    var Row = igtbl_getRowById(cellId)
    var nextRow = Row.getNextRow()
    var downCell = null
    if (nextRow != null)
        downCell = nextRow.getCellFromKey(Cell.Column.Key)
    return downCell
}

function ActivateThisCell(currCell) {
    bUpdate = false
    currCell.activate()
    currCell.select()
    currCell.beginEdit()
    bUpdate = true
}
// start
function getRightActiveCell(gridName, firstColumnKey) {
    var grid = igtbl_getGridById(gridName)
    for (i = 0; i < grid.Rows.length; i++) {
        var currRow = grid.Rows.rows[i]
        var firstCell = currRow.getCellFromKey(firstColumnKey)
        if (firstCell.getValue() == null)
            return firstCell
        else
            if (firstCell.getValue() == "")
            return firstCell
    }
    return null
}

function uwgDependents_EditKeyDownHandler(gridName, cellId, key) {
    if (!bUpdate) return
    var Cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId)
    var e = window.event
    currGridName = gridName
    if (e.ctrlKey && key == cDelete) {
        Row.remove()
        var rightCell = getRightActiveCell(gridName, firstColumnKey)
        currActiveCell = rightCell
        ActivateThisCell(rightCell)
        return;
    }
    if (e.ctrlKey && key == 9) {
        var prevEditCell = getPrevEditCell(cellId)
        if (prevEditCell != null)
            currActiveCell = prevEditCell
    }
    else if (key == 13 || key == 9) {
        //key == 39 Right
        var nextEditCell = getNextEditCell(cellId)
        if (nextEditCell != null) {
            currActiveCell = nextEditCell
            //ActivateThisCell(currActiveCell)
        }
    }
    /*else if (key == 38) { //Up
        var upCell = getUpCell(cellId)
        if (upCell != null) {
            currActiveCell = upCell
        }
    }
    else if (key == 40) { //Down
        var downCell = getDownCell(cellId)
        if (downCell != null) {
            currActiveCell = downCell
        }
    }*/
}

function uwgDependents_EditKeyUpHandler(gridName, cellId, key) {
    if (!bUpdate) return
    var Cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId)
    var e = window.event

    /*if (key == 38) { //Up
        var upCell = getUpCell(cellId)
        if (upCell != null) {
            currActiveCell = upCell
            ActivateThisCell(upCell)
        }
    }
    else if (key == 40) { //Down
        var downCell = getDownCell(cellId)
        if (downCell != null) {
            currActiveCell = downCell
            ActivateThisCell(downCell)
        }
    }*/
    //if (key == 38 || key == 40)
    //uwgDependents_AfterCellUpdateHandler(gridName, cellId) // To validate when up and down
}

function uwgDependents_BeforeCellChangeHandler(gridName, cellId) {
    if (!bUpdate) return
    if (currActiveCell != null) {
        ActivateThisCell(currActiveCell)
        return
    }
}

function uwgDependents_CellChangeHandler(gridName, cellId) {
    if (!bUpdate) return
    if (currActiveCell != null) {
        ActivateThisCell(currActiveCell)
        return
    }
}

function uwgDependents_CellClickHandler(gridName, cellId, button) {
    var Cell = igtbl_getCellById(cellId)
    if (Cell.isEditable()) {
        currActiveCell = Cell
        ActivateThisCell(currActiveCell)
    }
}

function uwgDependents_AfterCellUpdateHandler(gridName, cellId) {
    if (!bUpdate) return
    var Cell = igtbl_getCellById(cellId)
    if (Cell.Column.Key == "CBirthDate" || Cell.Column.Key == "CExpiryDate") {
        if (Cell.getValue() != "        ")
            CheckCellDate(Cell)
    }
}

function uwgDependents_AfterEnterEditModeHandler(gridName, cellId) {
    //if (!bUpdate) return
    var Cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId)
    var grid = igtbl_getGridById(gridName)

    if (Row.getNextRow() == null) {
        CreateEmptyRows(gridName)
    }
    if (Cell.Column.Key == firstColumnKey) 
        for (i = 0; i < grid.Rows.length; i++) {
            var currRow = grid.Rows.rows[i]
            //currRow.getCellFromKey("No").setValue(i+1)
        }

    if (Row.getCellFromKey(firstColumnKey).getValue() == null || Row.getCellFromKey(firstColumnKey).getValue() == "") {
        var rightCell = getRightActiveCell(gridName, firstColumnKey)
        currActiveCell = rightCell
        ActivateThisCell(currActiveCell)
    }
}
//Edit Control event
//(webComboId, newValue, keyCode) 
function wme_EditKeyDown (oEdit, keyCode, oEvent) {
    if (keyCode == cEnter || keyCode == cTab)
        uwgDependents_EditKeyDownHandler(currGridName, currActiveCell.Id, keyCode)
}

function CheckCellDate(Cell){
    var strEMessage = "InValid Date!"
    var strAMessage = "تاريخ غير صحيح"
    var strVal = Interfaces_frmExitReEntryVisa.CheckDate(Cell.getElement().innerText).value
    var strArr = strVal.split(",")
    var intLang = ConvertToNumber(strArr[0])
    var intValid = ConvertToNumber(strArr[1])
    if (intValid == 0) {
        bUpdate = false
        Cell.setValue("")
        ActivateThisCell(Cell)
        bUpdate = true 
        if (intLang == 1)
            alert(strAMessage)
        else
            alert(strEMessage)

    }
}

function PrintReport(RFN,RN,P) {
    var hight = window.screen.availHeight - 35;
    var width = window.screen.availWidth - 10;
    //var win = window.open("frmReportsGridViewer.aspx?Language=true&Criteria=FamilyMemberID&preview=1&ReportCode=" + ReportName + "&sq0=''&v=" + FamilyMemberID, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
    var win = window.open("../../Interfaces/frmReportForm.aspx?RFN=" + RFN+"&RN="+RN+"&P="+P, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
    win.moveTo(0, 0);
    win.focus();
}
