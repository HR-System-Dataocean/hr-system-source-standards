var IsEdit = false;
var HDate = ""
var GDate = ""
var HCell
var GCell
function ShowAlert(strEnglishMsg, strArabicMsg) {
    var language = window.document.getElementById("txtLang").value;
    if (language != "Eng") {
        alert(strArabicMsg)
    }
    else {
        alert(strEnglishMsg)
    }
}
function uwg_AfterEnterEditModeHandler(gridName, cellId, value) {
    IsEdit = true
}
function uwgLanguage_AfterCellUpdateHandler(gridName, cellId) {
    if (IsEdit == true) {
        if (igtbl_getCellById(cellId).Column.Key == "Language_ID") {
            var cell = igtbl_getCellById(cellId);
            var row = igtbl_getRowById(cellId).Id;
            IsEdit = false;
            var count = igtbl_getRowById(cellId).OwnerCollection.length;
            count--;
            var i = 0;
            for (i = 0; i <= count; i++) {
                var cells = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                var rows = igtbl_getRowById(cells.Id).Id;
                if (cells.getValue() == cell.getValue() && row != rows) {
                    cell.setValue();
                }
            }
        }
    }
    AddRow(gridName, cellId)
}
function uwgCertifications_AfterCellUpdateHandler(gridName, cellId) {
    if (IsEdit == true) {
        
        
        if (igtbl_getCellById(cellId).Column.Key == "EDegree_ID") {
            var cell = igtbl_getCellById(cellId);
            var row = igtbl_getRowById(cellId).Id;
            IsEdit = false;
            var count = igtbl_getRowById(cellId).OwnerCollection.length;
            count--;
            var i = 0;
            for (i = 0; i <= count; i++) {
                var cells = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                var rows = igtbl_getRowById(cells.Id).Id;
                if (cells.getValue() == cell.getValue() && row != rows) {
                    cell.setValue();
                    return;
                }
            }
        }
        if (igtbl_getCellById(cellId).Column.Key == "GDateFrom" || igtbl_getCellById(cellId).Column.Key == "GDateTo") {
            GCell = igtbl_getCellById(cellId);
            HCell = igtbl_getCellById(GCell.NextSibling.id);
            var GDate = GCell.MaskedValue;
            PageMethods.Greg2Hijri(GDate, OnSucceeded, OnFailed)
            IsEdit = false;
            return;

        }
        if (igtbl_getCellById(cellId).Column.Key == "HDateFrom" || igtbl_getCellById(cellId).Column.Key == "HDateTo") {

            HCell = igtbl_getCellById(cellId);
            GCell = igtbl_getCellById(HCell.PrevSibling.id);
            var HDate = HCell.MaskedValue;
            PageMethods.Hijri2Greg(HDate, OnSucceeded, OnFailed);
            IsEdit = false;
            return;
        }
    }
    AddRow(gridName, cellId)
}
function uwgHistory_AfterCellUpdateHandler(gridName, cellId) {
    if (IsEdit == true) {
        if (igtbl_getCellById(cellId).Column.Key == "GDateFrom" || igtbl_getCellById(cellId).Column.Key == "GDateTo") {
            GCell = igtbl_getCellById(cellId);
            HCell = igtbl_getCellById(GCell.NextSibling.id);
            var GDate = GCell.MaskedValue;
            PageMethods.Greg2Hijri(GDate, OnSucceeded, OnFailed)
            IsEdit = false;
            return;
        }
        if (igtbl_getCellById(cellId).Column.Key == "HDateFrom" || igtbl_getCellById(cellId).Column.Key == "HDateTo") {

            HCell = igtbl_getCellById(cellId);
            GCell = igtbl_getCellById(HCell.PrevSibling.id);
            var HDate = HCell.MaskedValue;
            PageMethods.Hijri2Greg(HDate, OnSucceeded, OnFailed);
            IsEdit = false;
            return;
        }
    }
    AddRow(gridName, cellId)
}

function AddRow(gridName, cellId) {
    var count = igtbl_getGridById(gridName).Rows.length - 1;
    var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

    if (rowIndex == count) {

        igtbl_addNew(gridName, 0, true, false);

    }
}


function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'Greg2Hijri') {
        var arrValues = result.split("|")
        GCell.setValue(arrValues[0])
        HCell.setValue(arrValues[1])
    }
    else if (methodName == 'Hijri2Greg') {
        var arrValues = result.split("|")
        GCell.setValue(arrValues[0])
        HCell.setValue(arrValues[1])
    }
}
function OnFailed(error) {
    //alert(error.get_message());
}
