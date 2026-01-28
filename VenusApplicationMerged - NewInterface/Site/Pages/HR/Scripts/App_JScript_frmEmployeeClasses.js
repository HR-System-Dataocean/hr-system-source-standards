function GetControlIDFromTab(controlName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var control = igtab_getElementById(controlName, webTab.element)
    if (control != null) {
        return control.id
    }
    else {
        control = webTab.findControl(controlName)
        return control.id
    }
    //Examples
    /*
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
    return ;
    var gridid        =  igtab_getElementById("uwgVacations", webTab.element).id;     
    var grid          = igtbl_getGridById(gridid);
    var EStartDate    = igdrp_getComboById(GetControlIDFromTab("WebDateChooser3",webTab))
    var EStartTime    = igedit_getById(webTab.findControl("WebDateTimeEdit3").id)
    */
}

var cEnter = 13;
var cTab = 9;
var cF9 = 120;
var cUP = 38;
var cDown = 40;
var cDelete = 46;

var intNoOfEmptyRows = 1;
var firstColumnKey = "FromMonth"

var bUpdate = true;
var currActiveCell = null
var currGridName;
var blnNotActivateThisCell = false


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
    if (currCell != null) {
        bUpdate = false
        currCell.activate()
        currCell.select()
        currCell.beginEdit()
        bUpdate = true
    }
}

function getCorrectCell(gridName) {
    var grid = igtbl_getGridById(gridName)
    for (i = 0; i < grid.Rows.length; i++) {
        var currRow = grid.Rows.rows[i];
        if (currRow != null && (!currRow.getHidden()))
            if (currRow.getCellFromKey(firstColumnKey).getValue() == null)
            return currRow.getCellFromKey(firstColumnKey)
    }
}

function ShowAlert(strEnglishMsg, strArabicMsg) {
    var language = window.document.getElementById("hdnLang").value;
    if (language == "1") {
        alert(strArabicMsg)
    }
    else {
        alert(strEnglishMsg)
    }
}

function uwgVacationDetails_CellClickHandler(gridName, cellId, button) {
    var Cell = igtbl_getCellById(cellId)
    var Row = Cell.getRow()
    if (Row.getCellFromKey(firstColumnKey).getValue() == null) {
        currActiveCell = getCorrectCell(gridName)
        ActivateThisCell(currActiveCell)
    }
    else if (Cell.isEditable()) {
        currActiveCell = Cell
    }
}

function uwgVacationDetails_AfterEnterEditModeHandler(gridName, cellId) {
    currGridName = gridName
    var Cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId)
    var hdnVacationTypeID = window.document.getElementById("hdnVacationTypeID")
    if (Row != null)
        Row.getCellFromKey("VacationTypeID").setValue(hdnVacationTypeID.value);
    if (Row.getNextRow() == null) {
        CreateEmptyRows(gridName)
        ActivateThisCell(currActiveCell)
    }
}

function uwgVacationDetails_AfterExitEditModeHandler(gridName, cellId) {
    var grid = igtbl_getGridById(gridName);
    var Cell = igtbl_getCellById(cellId)
    var hdnVacationTypeID = window.document.getElementById("hdnVacationTypeID")
    var intVacationTypeID = ConvertToNumber(hdnVacationTypeID.value)
    var Row = igtbl_getRowById(cellId)
    if (Cell.Column.Key == "FromMonth") {
        for (i = 0; i < grid.Rows.length; i++) {
            var currRow = grid.Rows.rows[i]
            if (currRow != null) {
                if (currRow.getIndex() != Row.getIndex()
                && ConvertToNumber(currRow.getCellFromKey("VacationTypeID").getValue()) == intVacationTypeID
                && ConvertToNumber(currRow.getCellFromKey("VacationTypeID").getValue()) > 0) {

                    if (Row.getCellFromKey("FromMonth").getValue() != null && currRow.getCellFromKey("ToMonth").getValue() == null && currRow.getCellFromKey("FromMonth").getValue() != null) {
                        ShowAlert("Must Close Open Period ", "يجب إغلاق الفترات المفتوحة")
                        Row.getCellFromKey("FromMonth").setValue(null)
                        currActiveCell = currRow.getCellFromKey("ToMonth")
                        ActivateThisCell(currActiveCell);
                    }

                    if (Row.getCellFromKey("FromMonth").getValue() != null)
                        if ((ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) >= ConvertToNumber(currRow.getCellFromKey("FromMonth").getValue()) && ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) < ConvertToNumber(currRow.getCellFromKey("ToMonth").getValue()))
                        ||
                        ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) <= ConvertToNumber(currRow.getCellFromKey("FromMonth").getValue())
                        ) {
                            ShowAlert("Found Same Period for This Vacation ", "يوجد نفس الفترة لهذة الأجازة")
                            Row.getCellFromKey("FromMonth").setValue("")
                            currActiveCell = Row.getCellFromKey("FromMonth")
                            ActivateThisCell(currActiveCell);
                        }
                }
            }
        } //End For
    }
    else if (Cell.Column.Key == "ToMonth") {
        for (i = 0; i < grid.Rows.length; i++) {
            var currRow = grid.Rows.rows[i]
            if (currRow != null) {
                if (currRow.getIndex() != Row.getIndex()
                && ConvertToNumber(currRow.getCellFromKey("VacationTypeID").getValue()) == intVacationTypeID
                && ConvertToNumber(currRow.getCellFromKey("VacationTypeID").getValue()) > 0) {


                    if (Row.getCellFromKey("ToMonth").getValue() != null) {
                        if (ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) >= ConvertToNumber(Row.getCellFromKey("ToMonth").getValue())) {
                            ShowAlert("From Month greater that or equal To Month ", "من شهر أكبر من أو يساوى إلى شهر ")
                            Row.getCellFromKey("ToMonth").setValue(ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) + 1)
                        }
                        if ((ConvertToNumber(Row.getCellFromKey("ToMonth").getValue()) > ConvertToNumber(currRow.getCellFromKey("FromMonth").getValue()) && ConvertToNumber(Row.getCellFromKey("FromMonth").getValue()) < ConvertToNumber(currRow.getCellFromKey("ToMonth").getValue()))
                        ) {
                            ShowAlert("Found Same Period for This Vacation ", "يوجد نفس الفترة لهذة الأجازة")
                            Row.getCellFromKey("ToMonth").setValue("")
                            currActiveCell = Row.getCellFromKey("FromMonth")
                            ActivateThisCell(currActiveCell);
                        }
                    }
                }
            }
        } //End For

    }
    else if (Cell.Column.Key == "DurationDays") {

    }
}

function wneFromMonth_KeyDown(oEdit, keyCode, oEvent) {
    var e = window.event;
    if (keyCode == cEnter || keyCode == cTab) {
        var nextEditCell = getNextEditCell(currActiveCell.Id)
        if (currActiveCell.getValue() != null) {
            if (nextEditCell != null) {
                currActiveCell = nextEditCell
                ActivateThisCell(currActiveCell)
            }
        }
        else {
            ActivateThisCell(currActiveCell)
        }
    }
    else if (e.ctrlKey && keyCode == cDelete) {
        var Row = currActiveCell.getRow()
        var nextRow = Row.getNextRow();
        Row.remove()
        if (nextRow != null) {
            currActiveCell = nextRow.getCellFromKey(firstColumnKey)
            ActivateThisCell(currActiveCell)
        }
    }
}

function wneToMonth_KeyDown(oEdit, keyCode, oEvent) {
    if (keyCode == cEnter || keyCode == cTab) {
        var nextEditCell = getNextEditCell(currActiveCell.Id)
        if (nextEditCell != null) {
            currActiveCell = nextEditCell
            ActivateThisCell(currActiveCell)
        }
    }
}

function wneDurationDays_KeyDown(oEdit, keyCode, oEvent) {
    if (keyCode == cEnter || keyCode == cTab) {
        var nextEditCell = getNextEditCell(currActiveCell.Id)
        if (nextEditCell != null) {
            currActiveCell = nextEditCell
            ActivateThisCell(currActiveCell)
        }
    }
}

function uwgVacationTypes_AfterRowActivateHandler(gridName, rowId) {
    var hdnVacationTypeID = window.document.getElementById("hdnVacationTypeID")
    var Row = igtbl_getRowById(rowId);
    hdnVacationTypeID.value = Row.getCellFromKey("VacationTypeID").getValue()
    HideDetailsRows(hdnVacationTypeID.value, null)
}

function HideDetailsRows(intVacationTypeID, GridCtrl) {
    //    var webTab = igtab_getTabById("UltraWebTab1");
    //    if (webTab == null)
    //        return;
    //    var gridid = igtab_getElementById("uwgVacationDetails", webTab.element).id;
    var grid;
    var activeRowsCount = 0;
    if (GridCtrl == null)
        grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgVacationDetails");
    else
        grid = GridCtrl
    for (i = 0; i < grid.Rows.length; i++) {
        var currRow = grid.Rows.rows[i];
        if (currRow != null)
            if (ConvertToNumber(currRow.getCellFromKey("VacationTypeID").getValue()) == intVacationTypeID) {
            currRow.setHidden(false)
            activeRowsCount = activeRowsCount + 1
        }
        else {
            currRow.setHidden(true)
        }
    } //End For
    CreateEmptyRows("UltraWebTab1xxctl0xuwgVacationDetails")
    currActiveCell = getCorrectCell("UltraWebTab1xxctl0xuwgVacationDetails")
    ActivateThisCell(currActiveCell);

}

function uwg_AfterEnterEditModeHandler(gridName, cellId) {
    AddRow(gridName, cellId);
}

function AddRow(gridName, cellId) {
    var count = igtbl_getGridById(gridName).Rows.length - 1;
    var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

    if (rowIndex == count) {

        igtbl_addNew(gridName, 0, true, false);

    }
}