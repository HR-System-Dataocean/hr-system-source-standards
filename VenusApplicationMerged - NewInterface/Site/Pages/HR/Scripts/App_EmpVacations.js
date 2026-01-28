

var DoCheck = true
//------------From App_JScript_Emp.js----------------------------------------------------

function uwgEmployeeVacations_AfterRowActivateHandler(gridName, rowId) {
    var row = igtbl_getRowById(rowId)

    if (row == null)
        return;

    var txtEmployee = window.document.getElementById("txtEmployee")

    if (txtEmployee.value == "")
        return;

    var lbTotalVal = window.document.getElementById("lbTotalVal")
    var lbRemainVal = window.document.getElementById("lbRemainVal")
    var lbConsumeVal = window.document.getElementById("lbConsumeVal").value;

    var lbVactionID = window.document.getElementById("lbVactionID")

    var VacationType = window.document.getElementById("DdlVacationType")

    var hdnAnnualVacId = window.document.getElementById("hdnAnnualVacId")

    var EStartDate = igdrp_getComboById("WebDateChooser3")
    var EEndDate = igdrp_getComboById("WebDateChooser4")

    var StartDate = igdrp_getComboById("WebDateChooser1")
    var EndDate = igdrp_getComboById("WebDateChooser2")

    var txtRemarks = window.document.getElementById("txtVacationReason")

    VacationType.disabled = true;
    VacationType.value = row.getCellFromKey("VacationTypeID").getValue()
    EStartDate.setValue(row.getCellFromKey("ExpectedStartDate").getValue())

    EEndDate.setValue(row.getCellFromKey("ExpectedEndDate").getValue())

    StartDate.setValue(row.getCellFromKey("ActualStartDate").getValue())

    EndDate.setValue(row.getCellFromKey("ActualEndDate").getValue())


    if (row.getCellFromKey("ActualEndDate").getValue() == null) {

        //-----[0261]-----[Start]
        EStartDate.setEnabled(true)
        EEndDate.setEnabled(true)
        //----------------[End]
        StartDate.setEnabled(true)
        EndDate.setEnabled(true)

    }
    else {
        //-----[0261]-----[Start]
        EStartDate.setEnabled(false)
        EEndDate.setEnabled(false)
        //----------------[End]
        StartDate.setEnabled(false)
        EndDate.setEnabled(false)
    }

    if (row.getCellFromKey("EmployeeRequestRemarks").getValue() == null)
        txtRemarks.value = ""
    else
        txtRemarks.value = row.getCellFromKey("EmployeeRequestRemarks").getValue()

    lbVactionID.value = row.getCellFromKey("ID").getValue()

    var vDate = new Date()
    vDate = StartDate.getValue();
    PageMethods.GetEmpContractVacAjax(txtEmployee.value, VacationType.value, StartDate.getText(), lbVactionID.value, hdnAnnualVacId.value, callback_RetreiveDate2, OnSucceeded, OnFailed)
    PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermission, OnSucceeded, OnFailed)
    PageMethods.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfo, OnSucceeded, OnFailed)
}

function callback_GetRecordPermission(res) {
    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")

    var saveItem = tlbMain.Items.fromKey("Save")
    var delItem = tlbMain.Items.fromKey("Delete")
    var printItem = tlbMain.Items.fromKey("Print")

    var arr = res.split(",")
    if (arr[0] == "1")
        saveItem.setEnabled(true)
    else
        saveItem.setEnabled(false)

    if (arr[1] == "1")
        delItem.setEnabled(true)
    else
        delItem.setEnabled(false)

    if (arr[2] == "1")
        printItem.setEnabled(true)
    else
        printItem.setEnabled(false)
}

function call_backRecordInfo(res) {
    var tlbMain = igtbar_getToolbarById("TlbMainNavigation")

    var reguserItem = tlbMain.Items.fromKey("RegUserVal")
    var regdateItem = tlbMain.Items.fromKey("RegDateVal")
    var canceldateItem = tlbMain.Items.fromKey("CancelDateVal")

    var arr = res.split(",")

    reguserItem.Element.innerText = arr[0]
    regdateItem.Element.innerText = arr[1]
    canceldateItem.Element.innerText = arr[2]

}

function callback_RetreiveDate2(res) {
    var lbTotalVal = window.document.getElementById("lbTotalVal")
    var lbRemainVal = window.document.getElementById("lbRemainVal")
    var lbConsumeVal = window.document.getElementById("lbConsumeVal").value;

    //----------------------
    var oComboFrom = window.document.getElementById("ExpectedStartDate");
    var oAComboFrom = window.document.getElementById("ActualStartDate");
    //----------------------
    var oComboTo = window.document.getElementById("ExpectedEndDate");
    //----------------------
    var retStr = res;
    if (retStr == null)
        return 0;
    var arr = retStr.split(',')

    lbTotalVal.innerText = TrimTo3Decimal(arr[0])
    lbConsumeVal.innerText = TrimTo3Decimal(arr[2])
    lbRemainVal.innerText = TrimTo3Decimal(arr[1])

    if (ConvertToNumber(arr[2]) < 0) {
        lbRemainVal.className = "worningRemain"
    }
    else {
        lbRemainVal.className = "noramlRemain"
    }

    if (arr[3] != 0 && arr[4] != 0) {
    }
}

function DdlVacationTypeChange2() {

    var txtEmployee = window.document.getElementById("txtEmployee")
    var lbVactionID = window.document.getElementById("lbVactionID")
    var hdnAnnualVacId = window.document.getElementById("hdnAnnualVacId")
    //var DdlVacationType = window.document.getElementById("DdlVacationType")

    if (txtEmployee.value == "")
        return;
    var VacationType = window.document.getElementById("DdlVacationType")
    var StartDate = igdrp_getComboById("WebDateChooser1")
    IsDataChanged = "T"
    PageMethods.GetEmpContractVacAjax(txtEmployee.value, VacationType.value, StartDate.getText(), lbVactionID.value, hdnAnnualVacId.value, callback_RetreiveDate2, OnSucceeded, OnFailed)
}

function TrimTo3Decimal(str) {


    var retStr = ""
    var arr = str.split('.')
    retStr = arr[0]
    if (arr.length > 1) {
        retStr = retStr + "."
        if (arr[1].length > 3) {
            retStr = retStr + arr[1].substr(0, 3)
        }
        else {
            retStr = retStr + arr[1]
        }
    }
    return retStr;
}


function uwgEmployeeVacations_BeforeRowActivateHandler(gridName, rowId) {
    var row = igtbl_getRowById(rowId)
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


function ChangeIsDataChanged() {
    if (isFormChanged()) {
        IsDataChanged = "T";
    }
    else {
        IsDataChanged = "F";
    }
}





//=========================== Test
function Button1_onclick() {
    PageMethods.RetTable("EmpTable", OnSucceeded, OnFailed)
}





var hight;
var width;
var StartDate;
var txtSlice;
function btnVacationTransactionOn_Click(oButton, oEvent) {
    hight = window.screen.availHeight - 35;
    width = window.screen.availWidth - 10;
    StartDate = igdrp_getComboById("WebDateChooser1")
    var txtEmployee = window.document.getElementById("txtEmployee")
    PageMethods.GetEmployeeID(txtEmployee.value, OnSucceeded, OnFailed);
}

function DdlVacationTypeChange() {

    var txtEmployee = window.document.getElementById("txtEmployee")
    var lbVactionID = window.document.getElementById("lbVactionID")
    var hdnAnnualVacId = window.document.getElementById("hdnAnnualVacId")
    var hdnAnnualVacId1 = window.document.getElementById("hdnAnnualVacId")
    if (txtEmployee.value == "")
        return;
    var VacationType = window.document.getElementById("DdlVacationType")
    var VacationSlice = window.document.getElementById("DdlVacationSlice")
    hdnAnnualVacId.value = VacationType.value;
    hdnAnnualVacId1.value = VacationSlice.value;
    var StartDate = igdrp_getComboById("WebDateChooser1")
    IsDataChanged = "T"
    PageMethods.GetEmpContractVacAjax(txtEmployee.value, VacationType.value, StartDate.value, lbVactionID.value, hdnAnnualVacId.value, callback_RetreiveDate, OnSucceeded, OnFailed)
}

function callback_RetreiveDate(res) {
    var lbTotalVal = window.document.getElementById("lbTotalVal")
    var lbRemainVal = window.document.getElementById("lbRemainVal")
    var lbConsumeVal = window.document.getElementById("lbConsumeVal").value;

    //----------------------
    var oComboFrom = window.document.getElementById("ExpectedStartDate");
    var oAComboFrom = window.document.getElementById("ActualStartDate");
    //----------------------
    var oComboTo = window.document.getElementById("ExpectedEndDate");
    //----------------------

    var retStr = res;
    if (retStr == null)
        return 0;
    var arr = retStr.split(',')

    lbTotalVal.innerText = TrimTo3Decimal(arr[0])
    lbConsumeVal.innerText = TrimTo3Decimal(arr[1])
    lbRemainVal.innerText = TrimTo3Decimal(arr[2])

    if (ConvertToNumber(arr[2]) < 0) {
        lbRemainVal.className = "worningRemain"
    }
    else {
        lbRemainVal.className = "noramlRemain"
    }

    if (arr[3] != 0 && arr[4] != 0) {
        var arr1 = arr[3].split(" ")[0].split("/")
        var arr2 = arr[4].split(" ")[0].split("/")

        var date1 = new Date()
        var date2 = new Date()
        date1.setFullYear(arr1[2], arr1[1] - 1, arr1[0])
        date2.setFullYear(arr2[2], arr2[1] - 1, arr2[0])

        DoCheck = false
        oComboFrom.value = date1;
        oAComboFrom.value = date1;
        oComboTo.value = date2;
        DoCheck = true
    }
}

function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'RetTable') {
        var table = result
        var grid = igtbl_getGridById("uwgTest");
        var tbl = table;
        SetGridDataSource(grid, tbl)
        //grid.Element.dataSrc = tbl
    }
    else if (methodName == 'GetEmployeeID') {
        var IntEmpoyeeID = result;
        var hight = window.screen.availHeight - 35;
        var width = window.screen.availWidth - 10;
        var StartDate = igdrp_getComboById("WebDateChooser1")
        var lbConsumeVal = window.document.getElementById("lbConsumeVal").value;
        window.open("frmEmployeesVacationTransactions.aspx?Fisical=" + StartDate.getText() + "&ID=" + IntEmpoyeeID + "&Dys=" + lbConsumeVal, "_Parent", "height=" + hight + ",width=" + width + ",top=0,left=0,menubar=0,toolbar=0,scrollbars=1 ");
    }
}
function OnFailed(error) {
    alert(error.get_message());
}
        
        