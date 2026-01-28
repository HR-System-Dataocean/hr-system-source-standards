
var cEnter = 13;
var cTab = 9;
var cF8 = 119;
var CDelete = 46;
var cF9 = 120;
var intNoofEmptyRows = 10;
var oActiveCell = null;
var bEnterEditMode = true;
var blnCreating = true;
var CodeCode = "";
function TrimString(str) {
    return str.replace(/^\s+|\s+$/g, '')
}
function CreateEmptyRows(gridName) {
    blnCreating = true;
    for (i = 0; i < intNoofEmptyRows - 1; i++) {
        igtbl_addNew(gridName, 0, true, false);
    }
    blnCreating = false;
}

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
}

function ShowAlert(strEnglishMsg, strArabicMsg) {
    var language = window.document.getElementById("txtLang").value;
    if (language != "Eng") {
        alert(strArabicMsg)
    }
    else {
        alert(strEnglishMsg)
    }

}
function ActivateOnThisCell(cell) {
    bEnterEditMode = false;
    cell.activate();
    cell.beginEdit();
    oActiveCell = cell;
    bEnterEditMode = true;
}
function ClearRow(Row) {
    Row.getCellFromKey('ID').setValue("")
    Row.getCellFromKey('EmployeeID').setValue("")
//    Row.getCellFromKey('Employees').setValue("")
    Row.getCellFromKey('EmployeesName').setValue("")
    Row.getCellFromKey('Days').setValue(0)
    Row.getCellFromKey('OverDue').setValue(0)
    Row.getCellFromKey('VacationBalance').setValue(0)
}

var totalDays;
var dblWorkingDays;
var dblVacationWorkingDays;
var mRow;

function ConvertToNumber(val) {
    val = val - 1
    return (val + 1)
}

var bExitEditMode = false;
function UWGEmployeesProjects_AfterExitEditModeHandler(gridName, cellId) {


}
var currIndexRow = -1
var currEmployeeCode = null
var txtLang;
function UWGEmployeesProjects_AfterEnterEditModeHandler(gridName, cellId) {

    var cell = igtbl_getCellById(cellId);
    var Row = cell.getRow();
    var rowindex = Row.getIndex();
    txtLang = window.document.getElementById("txtLang");
    DdlPeriods = window.document.getElementById("DdlPeriods")

    if (cell.Column.Key == "Employees") {
        currEmployeeCode = cell.getValue()
    }

    if (bEnterEditMode) {
        cellEmployeeID = Row.getCellFromKey("EmployeeID")
        var cellCode = Row.getCellFromKey("Employees")
        if (cellCode != null) {
            if (cellCode.getValue() != null && cellEmployeeID.getValue() != null && cellEmployeeID.getValue() != 0) {
                if (TrimString(cellCode.getValue()) != "") {
                    oActiveCell = cell;
                }
                else
                    ActivateOnThisCell(cellCode)
            }
            else
                ActivateOnThisCell(cellCode)
        }
    }

    var EmpCode = Row.getCellFromKey('Employees').getValue();
    if (EmpCode != null) {
        mRow = Row;
        PageMethods.GetEmployeeInformation(EmpCode, DdlPeriods.value, OnSucceeded, OnFailed);
    }
    else {
    }
}

function txtEmployee_KeyDown(oEdit, keyCode, oEvent) {

    cellCode = oActiveCell;
    var Row = cellCode.getRow()
    mRow = Row;
    var grid = igtbl_getGridById("UWGEmployeesProjects")
     DdlPeriods = window.document.getElementById("DdlPeriods")
     ddlProject = window.document.getElementById("ProjectsID")
     cellEmployeeID = Row.getCellFromKey("EmployeeID")
     hdnFiscalDays = window.document.getElementById("hdnFiscalDays")
    switch (keyCode) {

        case (cTab):
        case (cEnter):
            {
                if (grid.Rows.length - cellCode.Row.getIndex() <= 2)
                    CreateEmptyRows("UWGEmployeesProjects")


                if (cellCode.getValue() != null) {
                    if (TrimString(cellCode.getValue()) != "") {
                        PageMethods.GetEmployeeName(cellCode.getValue(), DdlPeriods.value, OnSucceeded, OnFailed);

                    } //if(TrimString(cellCode.getValue()) )
                    else {
                        if (!bExitEditMode) {
                            ShowAlert("You must enter code", "يجب إدخال الكود");
                            ClearRow(Row)
                            ActivateOnThisCell(cellCode)
                        }
                    }



                } // if (cellCode.getValue() != null)
                else {
                    if (!bExitEditMode) {
                        ShowAlert("You must enter code", "يجب إدخال الكود");
                        ClearRow(Row)
                        ActivateOnThisCell(cellCode)
                    }
                }

                break;
            }    //End Case (cEnter)
        case (cF9):
            {
                var winopen = window.open("frmSearchScreen.aspx?TargetControl=" + cellCode.Id + "&SearchID=90", "_Parent" + 1, "height=560,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");
                winopen.document.focus();
                break;
            }
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }

    } //End Switch

} //End function 

function wneWorkingUnits_KeyDown(oEdit, keyCode, oEvent) {

    var cellWorkingUnits = oActiveCell
    var Row = cellWorkingUnits.getRow();
    var grid = igtbl_getGridById("UWGEmployeesProjects")
    var cellWorkingDays = Row.getCellFromKey("WorkingUnits")
    var cellNumberofWorkingDaysValue = Row.getCellFromKey("NumberofWorkingDaysValue")
    //var cellVacationsTypesID = Row.getCellFromKey("VacationsTypesID")
    var cellOvertimeHours = Row.getCellFromKey("OvertimeHours")
    var cellOvertimeHoursValue = Row.getCellFromKey("OvertimeHoursValue")
    var hdnFiscalDays = window.document.getElementById("hdnFiscalDays")
     cellRWD = Row.getCellFromKey("RWD")
    if (Row.getCellFromKey('VacationWorkingUnits').getValue() == null)
        Row.getCellFromKey('VacationWorkingUnits').setValue(0)

    if (Row.getCellFromKey('VacationWorkingUnits').getValue() == null)
        Row.getCellFromKey('WorkingUnits').setValue(0)


    switch (keyCode) {

        case (cTab):
        case (cEnter):
            {
                if (cellWorkingUnits.getValue() != null) {
                    var counter = 0;
                    var strEmpCode = Row.getCellFromKey("Employees").getValue();
                    var IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
                    var fical = hdnFiscalDays.value;
                    var EmployeeID = Row.getCellFromKey('EmployeeID').getValue()
                    for (i = 0; i < grid.Rows.length; i++) {
                        var currRow = grid.Rows.rows[i];
                        if (currRow != undefined) {
                            if ((parseFloat(currRow.getCellFromKey("WorkingUnits").getValue()) > 0 && currRow.getCellFromKey("WorkingUnits").getValue() != "") && currRow.getCellFromKey("Employees").getValue() == strEmpCode) {
                                counter++;
                            }
                        }
                    }
                    if (counter > 1) {
                        Row.getCellFromKey('WorkingUnits').setValue(0);
                        Row.getCellFromKey('NumberofWorkingDaysValue').setValue(0)
                        ShowAlert("Working days should not be repeated more than one for an employee ", "لايجب إدخال ايام العمل اكثر من مرة لموظف واحد");


                    }
                    else if (GetTotalDays(EmployeeID) > hdnFiscalDays.value || GetTotalDays(EmployeeID) > cellRWD.getValue()) {
                        Row.getCellFromKey('WorkingUnits').setValue(0);
                        Row.getCellFromKey('NumberofWorkingDaysValue').setValue(0)
                        //Row.getCellFromKey('OvertimeHours').setValue(0);
                        //Row.getCellFromKey('OvertimeHoursValue').setValue(0)
                        ShowAlert("Available Working days:" + (cellRWD.getValue() - GetTotalDays(EmployeeID)), "أيام العمل المتاحه:" + (cellRWD.getValue() - GetTotalDays(EmployeeID)));

                        SetSummation(Row)
                        GetColSum("WorkingUnits")
                        GetColSum("NumberofWorkingDaysValue")
                        GetColSum("OvertimeHours")
                        GetColSum("OvertimeHoursValue")

                        bEnterEditMode = false;
                        cellWorkingDays.activate()
                        cellWorkingDays.select();
                        cellWorkingDays.beginEdit()
                        bEnterEditMode = true;
                        break;
                    }
                    else {
                        mRow = Row;
                        fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit = 1;
                        PageMethods.fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed(strEmpCode, IntFisicalPeriod, Row.getCellFromKey('WorkingUnits').getValue(), 0, 0, Row.getCellFromKey('lblSalaryPerHour').getValue(), Row.getCellFromKey('lblSalaryPerDay').getValue(), OnSucceeded, OnFailed)
                    }
                    SetSummation(Row)
                    GetColSum("WorkingUnits")
                    GetColSum("NumberofWorkingDaysValue")
                } // End if(cellWorkingUnits.getValue() != null)

                bEnterEditMode = false;
                /*
                cellVacationsTypesID.activate()
                cellVacationsTypesID.select();
                cellVacationsTypesID.beginEdit()
                oActiveCell = cellVacationsTypesID
                */
                cellOvertimeHours.activate()
                cellOvertimeHours.select();
                cellOvertimeHours.beginEdit()
                oActiveCell = cellOvertimeHours
                bEnterEditMode = true;
                break;

            }      //case(cEnter)
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }
    } //switch(keyCode)

}
var fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit;
var CheckVacationType_Is_Edit;
function cmbVacationsTypesID_EditKeyDown(webComboId, newValue, keyCode) {

    var cell = oActiveCell
    var Row = cell.getRow();
    var cmbVacationsTypesID = igcmbo_getComboById(webComboId)
    var grid = igtbl_getGridById("UWGEmployeesProjects")
    var cellVacationWorkingUnits = Row.getCellFromKey("VacationWorkingUnits")
    var cellVacationsTypesID = Row.getCellFromKey("VacationsTypesID")
    if (Row.getCellFromKey('VacationWorkingUnits').getValue() == null)
        Row.getCellFromKey('VacationWorkingUnits').setValue(0)

    if (Row.getCellFromKey('VacationWorkingUnits').getValue() == null)
        Row.getCellFromKey('WorkingUnits').setValue(0)

    var cmb = igcmbo_getComboById(webComboId)
    var selectedIndex = cmb.getSelectedIndex()
     cellEmployeeID = Row.getCellFromKey("EmployeeID")

    switch (keyCode) {
        case (cTab):
        case (cEnter):
            {
                var strEmpCode = Row.getCellFromKey("Employees").getValue();
                var strVacationType = cmbVacationsTypesID.getValue();
                var IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
                if (cmbVacationsTypesID.getValue() != 0) {

                    var bExitFinally = false;

                    for (i = 0; i < grid.Rows.length; i++) {
                        var currRow = grid.Rows.rows[i];
                        var EmployeeID = currRow.getCellFromKey("EmployeeID").getValue()
                        var VacTypEmployeeID = currRow.getCellFromKey("VacationsTypesID").getValue()
                        if (EmployeeID != null && EmployeeID != 0) {
                            if (VacTypEmployeeID != null && VacTypEmployeeID != 0 && VacTypEmployeeID == cmbVacationsTypesID.getValue() && EmployeeID == cellEmployeeID.getValue() && Row.getIndex() != currRow.getIndex()) {
                                //cmbVacationsTypesID.setValue(0);
                                cmbVacationsTypesID.selectedIndex = 0
                                Row.getCellFromKey('VacationsTypesID').setValue(0);
                                Row.getCellFromKey('VacationWorkingUnits').setValue(0);
                                Row.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(0));
                                //Row.getCellFromKey('VacationOvertimeHours').setValue(0);
                                //Row.getCellFromKey('CounterHourOverTimeHolidayValue').setValue(parseFloat(0));

                                GetColSum("VacationWorkingUnits")
                                GetColSum("CounterDayVacationValue")
                                GetColSum("VacationOvertimeHours")
                                GetColSum("CounterHourOverTimeHolidayValue")
                                SetSummation(Row)
                                ShowAlert("Vcation type must be unique for an employee", "أنواع الإجازات يجب عدم تكرار لموظف نفسه");

                                bEnterEditMode = false;
                                cell.activate()
                                cell.beginEdit();
                                oActiveCell = cell
                                bEnterEditMode = true;

                                bExitFinally = true
                                break;


                            }
                        } //Enf if (EmployeeID != null)
                    } //Enf for (i
                } // Enf if (Row.getCellFromKey('VacationsTypesID')
                fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit = 0;
                PageMethods.fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed(strEmpCode, IntFisicalPeriod, Row.getCellFromKey('VacationWorkingUnits').getValue(), 0, 0, Row.getCellFromKey('lblSalaryPerHour').getValue(), Row.getCellFromKey('lblSalaryPerDay').getValue(), OnSucceeded, OnFailed)
                mRow = Row;
                CheckVacationType_Is_Edit = 1;
                PageMethods.CheckVacationType(cmbVacationsTypesID.getValue(), OnSucceeded, OnFailed);

                if (cmbVacationsTypesID.getValue() == 0) {

                    Row.getCellFromKey('VacationWorkingUnits').setValue(0);
                    Row.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(0));
                    //Row.getCellFromKey('VacationOvertimeHours').setValue(0);
                    //Row.getCellFromKey('CounterHourOverTimeHolidayValue').setValue(parseFloat(0));

                    GetColSum("VacationWorkingUnits")
                    GetColSum("CounterDayVacationValue")
                    GetColSum("VacationOvertimeHours")
                    GetColSum("CounterHourOverTimeHolidayValue")
                    SetSummation(Row)

                }
                else
                    if (values2.value == 0)
                    Row.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(values.value));
                else
                    Row.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(0));

                if (bExitFinally)
                    break;
                SetSummation(Row)
                GetColSum("VacationWorkingUnits")
                GetColSum("CounterDayVacationValue")
                bEnterEditMode = false;
                cellVacationWorkingUnits.activate()
                cellVacationWorkingUnits.beginEdit();
                oActiveCell = cellVacationWorkingUnits
                bEnterEditMode = true;
                break;
            }
        case (32): //space
            {
                if (cmb.getDropDown()) {
                    cmbVacationsTypesID_EditKeyDown(webComboId, newValue, cEnter); //Can change with same event
                }
                else {
                    cmb.setDropDown(true)

                    cmb.focus()
                    try {
                        if (selectedIndex == -1)
                            cmb.setSelectedIndex(0);
                        else
                            cmb.setSelectedIndex(selectedIndex);
                    }
                    catch (e)
                { }
                }
                break;
            }
        case (27): //Escape
            {
                cmb.setDropDown(false)
                if (cell != null) {
                    cell.activate()
                    cell.select()
                    cell.beginEdit()
                }
                cmb.focus()
                break;
            }
            /*
            case 38://Up
            {
            try
            {
            cmb.setSelectedIndex(selectedIndex-1);
            }
            catch(e)
            {
            }
            break;
            }
            case 40://Down
            {
            try
            {
            if (selectedIndex == -1)
            cmb.setSelectedIndex(0); 
            else
            cmb.setSelectedIndex(selectedIndex+1);
            }
            catch(e)
            {
            }
            break;
            }
            */
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }
    }
}
function cmbVacationsTypesID_AfterSelectChange(webComboId) {
    cmbVacationsTypesID_EditKeyDown(webComboId, null, cEnter)
}
var EmployeeID;

function wneVacationWorkingUnits_KeyDown(oEdit, keyCode, oEvent) {
    var cellVacationWorkingUnits = oActiveCell
    var Row = cellVacationWorkingUnits.getRow();
    var cellOvertimeHours = Row.getCellFromKey("OvertimeHours")
    var grid = igtbl_getGridById("UWGEmployeesProjects")
    cellRWD = Row.getCellFromKey("RWD")
    var cellVacationWorkingUnits = Row.getCellFromKey("VacationWorkingUnits")
    var cellCounterDayVacationValue = Row.getCellFromKey("CounterDayVacationValue")
    var cellVacationOvertimeHours = Row.getCellFromKey("VacationOvertimeHours")
    var cellCounterHourOverTimeHolidayValue = Row.getCellFromKey("CounterHourOverTimeHolidayValue")
    var cellVacationsTypesID = Row.getCellFromKey("VacationsTypesID")
     hdnFiscalDays = window.document.getElementById("hdnFiscalDays")
    if (Row.getCellFromKey('VacationWorkingUnits').getValue() == null)
        Row.getCellFromKey('VacationWorkingUnits').setValue(0)

    switch (keyCode) {

        case (cTab):
        case (cEnter):
            {

                if (Row.getCellFromKey('VacationsTypesID').getValue() == null || Row.getCellFromKey('VacationsTypesID').getValue() == 0 || Row.getCellFromKey('VacationWorkingUnits').getValue() == 0) {

                    Row.getCellFromKey('VacationWorkingUnits').setValue(0);
                    Row.getCellFromKey('CounterDayVacationValue').setValue(0)

                    SetSummation(Row)
                    GetColSum("VacationWorkingUnits")
                    GetColSum("CounterDayVacationValue")
                    GetColSum("VacationOvertimeHours")
                    GetColSum("CounterHourOverTimeHolidayValue")

                    if ((Row.getCellFromKey('VacationsTypesID').getValue() == null || Row.getCellFromKey('VacationsTypesID').getValue() == 0) && Row.getCellFromKey('VacationWorkingUnits').getValue() != 0) {
                        bEnterEditMode = false;
                        cellVacationsTypesID.activate()
                        cellVacationsTypesID.select();
                        cellVacationsTypesID.beginEdit()
                        oActiveCell = cellVacationsTypesID
                        bEnterEditMode = true;
                        ShowAlert("Must select vacation type", "يجب إختيار نوع الاجازة أولا");
                        break;
                    }

                }
                if (Row.getCellFromKey('VacationsTypesID').getValue() != 0 && Row.getCellFromKey('VacationWorkingUnits').getValue() != null && Row.getCellFromKey('VacationWorkingUnits').getValue() != 0) {

                    var strEmpCode = Row.getCellFromKey("Employees").getValue();
                    var strVacationType = Row.getCellFromKey("VacationsTypesID").getValue();
                    var IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
                    EmployeeID = Row.getCellFromKey('EmployeeID').getValue();

                    var intVacationTypEmployeeID = 0
                    if (strVacationType != null) {
                        intVacationTypEmployeeID = strVacationType;
                        CheckVacationType_Is_Edit = 2;
                        mRow = Row;
                        PageMethods.CheckVacationType(intVacationTypEmployeeID, OnSucceeded, OnFailed);
                        break;
                    }
                    else {
                        mRow = Row;
                        fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit = 2;
                        PageMethods.fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed(strEmpCode, IntFisicalPeriod, Row.getCellFromKey('VacationWorkingUnits').getValue(), 0, 0, Row.getCellFromKey('lblSalaryPerHour').getValue(), Row.getCellFromKey('lblSalaryPerDay').getValue(), OnSucceeded, OnFailed)

                    }
                    SetSummation(Row)
                    GetColSum("VacationWorkingUnits")
                    GetColSum("CounterDayVacationValue")
                } // End If


                bEnterEditMode = false;
                /*
                cellOvertimeHours.activate()
                cellOvertimeHours.beginEdit()
                oActiveCell = cellOvertimeHours
                */
                cellVacationOvertimeHours.activate()
                cellVacationOvertimeHours.beginEdit()
                oActiveCell = cellVacationOvertimeHours
                bEnterEditMode = true;

                break;
            }         //End case
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }
    } //End Switch
}
var cellVacationsTypesID;
var GetEmployeesSalaryFactor_Is_Edit;
function wneOvertimeHours_KeyDown(oEdit, keyCode, oEvent) {
    var grid = igtbl_getGridById("UWGEmployeesProjects")
    var cellCode = oActiveCell
    var Row = cellCode.getRow()
    cellVacationsTypesID = Row.getCellFromKey("VacationsTypesID")
    var cellVacationOvertimeHours = Row.getCellFromKey("VacationOvertimeHours")
    var cellWorkingUnits = Row.getCellFromKey("WorkingUnits")
    var cellOvertimeHours = Row.getCellFromKey("OvertimeHours")
     cellHPD = Row.getCellFromKey("HPD")
    switch (keyCode) {

        case (cTab):
        case (cEnter):
            {
                strEmpCode = Row.getCellFromKey("Employees").getValue();
                var counter = 0
                for (i = 0; i < grid.Rows.length; i++) {
                    var currRow = grid.Rows.rows[i];
                    if (currRow != undefined) {
                        if ((parseFloat(currRow.getCellFromKey("OvertimeHours").getValue()) > 0 && currRow.getCellFromKey("OvertimeHours").getValue() != "") && currRow.getCellFromKey("Employees").getValue() == strEmpCode) {
                            counter++;
                        }
                    }
                }
                mRow = Row;
                GetEmployeesSalaryFactor_Is_Edit = 2;
                IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
                PageMethods.GetEmployeesSalaryFactor(strEmpCode, IntFisicalPeriod, OnSucceeded, OnFailed);
                break;
            }
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }
    }

}
var cellCode;
var cellEmployees;
var strEmpCode;
var IntFisicalPeriod;
function wneVacationOvertimeHours_KeyDown(oEdit, keyCode, oEvent) {
    cellCode = oActiveCell;
    var Row = cellCode.getRow()
    var nextRow = Row.getNextRow()
    cellEmployees = nextRow.getCellFromKey("Employees")
    var cellVacationWorkingUnits = Row.getCellFromKey("VacationWorkingUnits")
    var cellVacationOvertimeHours = Row.getCellFromKey("VacationOvertimeHours")
    var cellCounterHourOverTimeHolidayValue = Row.getCellFromKey("CounterHourOverTimeHolidayValue")

    switch (keyCode) {

        case (cTab):
        case (cEnter):
            {
                strEmpCode = Row.getCellFromKey("Employees").getValue();
                IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
                mRow = Row;
                GetEmployeesSalaryFactor_Is_Edit = 1;
                PageMethods.GetEmployeesSalaryFactor(strEmpCode, IntFisicalPeriod, OnSucceeded, OnFailed);
                break;
            }
        case (CDelete):
            {
                Row.deleteRow();
                break;
            }
    }

}





function GetNextActiveCell(cell) {
    var nextcell;
    nextcell = cell.getNextTabCell()
    if (nextcell == null) return cell;
    if (nextcell.isEditable() == false) {
        return GetNextActiveCell(nextcell)
    }
    else {
        return nextcell
    }
}


var strEmployeeName;
var cellEmployeeName;
var cellWorkingUnits;
var cellID;
var arrValues;
var IntFisicalPeriod;
var strInfo;
var str2;
var str3;
var strRWD;
var strHPD;
var CounterDayVacationValue1 = "";
var cellEmployeeID;
var cellRWD;
var cellHPD;
var DdlPeriods;
var hdnFiscalDays;
var ddlProject;
function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'GetVacationsTypesIsPaidValue') {
        if (CounterDayVacationValue1 == "") {
            var values2 = result;
            if (values2 == 0)
                totalDays = dblWorkingDays + dblVacationWorkingDays
            else
                totalDays = dblWorkingDays
        }
        else {
            mRow.getCellFromKey('IsPaid').setValue(parseFloat(result));
            if (values2 == 0)
                mRow.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(CounterDayVacationValue1));
            else
                mRow.getCellFromKey('CounterDayVacationValue').setValue(parseFloat(0));
        }

    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetAllEmployeeDataAfterTextChange_Ajax') {
        var totalSalary = result;
        mRow.getCellFromKey('Sum').setValue(parseFloat(totalSalary) + parseFloat(mRow.getCellFromKey('OvertimeHoursValue').getValue()) + parseFloat(mRow.getCellFromKey('CounterHourOverTimeHolidayValue').getValue()))

    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetEmployeeInformation') {
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetEmployeeName') {
        strEmployeeName = result;
        cellEmployeeName = mRow.getCellFromKey("EmployeesName")
        cellID = mRow.getCellFromKey("EmployeeID")
        if (strEmployeeName != "") {

            if (currEmployeeCode != cellCode.getValue())
                ClearRow(mRow)

            arrValues = strEmployeeName.split("|")
            cellEmployeeName.setValue(arrValues[0])
            cellID.setValue(arrValues[1])
        }
        IntFisicalPeriod = window.document.getElementById("DdlPeriods").value;
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetEmployeeGeneralInfromation') {
        strInfo = result;
        str2 = strInfo;
        if (str2 != "") {
            str3 = str2.split("/");
            mRow.getCellFromKey('lblBasicSalary').setValue(ConvertToNumber(str3[0]));
            mRow.getCellFromKey('lblSalaryPerDay').setValue(ConvertToNumber(str3[1]));
            mRow.getCellFromKey('lblSalaryPerHour').setValue(ConvertToNumber(str3[2]));
            mRow.getCellFromKey('lblOverTimePerHour').setValue(ConvertToNumber(str3[3]));
        }

        PageMethods.GetEmpReminingWorkingDays(cellEmployeeID.getValue(), DdlPeriods.value, hdnFiscalDays.value, ddlProject.value, OnSucceeded, OnFailed);
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetEmpReminingWorkingDays') {

        strRWD = result;
        cellRWD.setValue(parseFloat(strRWD))

        PageMethods.GetWorkingHours(cellEmployeeID.getValue(), DdlPeriods.value, OnSucceeded, OnFailed);

    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetWorkingHours') {
        strHPD = result;
        if (strHPD != 0) {
            cellHPD.setValue(parseFloat(strHPD))

            bEnterEditMode = false;
            cellWorkingUnits.activate()
            cellWorkingUnits.beginEdit()
            oActiveCell = cellWorkingUnits
            bEnterEditMode = true;
        }
        else {
            ShowAlert("This employee not found or may be in vacation", "هذا الموظف غير موجود أو ربما فى أجازة");
            ClearRow(mRow)
            ActivateOnThisCell(cellCode)
        }
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed') {
        var values = result;
        if (fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit == 1) {
            mRow.getCellFromKey('NumberofWorkingDaysValue').setValue(parseFloat(values));
            fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit = 0;
        } //=================================================================================
        else if (fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit == 2) {
            CounterDayVacationValue1 = result;
            PageMethods.GetVacationsTypesIsPaidValue(mRow.getCellFromKey('VacationsTypesID').getValue(), OnSucceeded, OnFailed);

            fnGetAllEmployeeDataAfterTextChange_Ajax_Distributed_Is_Edit = 0;
        } //=================================================================================
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'CheckVacationType') {
        var values = result;
        if (CheckVacationType_Is_Edit == 1) {
            mRow.getCellFromKey('IsPaid').setValue(parseFloat(values));
        } //=================================================================================
        else if (CheckVacationType_Is_Edit == 2) {

            if (GetTotalDays(EmployeeID) > cellRWD.getValue() && values == 0) {
                mRow.getCellFromKey('VacationWorkingUnits').setValue(0);
                mRow.getCellFromKey('CounterDayVacationValue').setValue(0);
                SetSummation(mRow)
                GetColSum("VacationWorkingUnits")
                GetColSum("CounterDayVacationValue")
                GetColSum("VacationOvertimeHours")
                GetColSum("CounterHourOverTimeHolidayValue")
                ShowAlert("Available Vacation days:" + (cellRWD.getValue() - GetTotalDays(EmployeeID)), "أيام الأجازة المتاحه:" + (cellRWD.getValue() - GetTotalDays(EmployeeID)));

                bEnterEditMode = false;
                cellVacationWorkingUnits.activate()
                cellVacationWorkingUnits.select();
                cellVacationWorkingUnits.beginEdit();
                oActiveCell = cellVacationWorkingUnits;
                bEnterEditMode = true;

            }
            CheckVacationType_Is_Edit = 0;
        } //=================================================================================
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    else if (methodName == 'GetEmployeesSalaryFactor') {
        if (GetEmployeesSalaryFactor_Is_Edit == 1) {
            var strFactor = result;
            var OverTimeFactor = strFactor.split("$")[0];
            var HolidayFactor = strFactor.split("$")[1];
            var CellValue = cellCode.getValue();
            var Total = 0;
            var dblSalaryPerHour = mRow.getCellFromKey('lblSalaryPerHour').getValue();
            Total = parseFloat(CellValue) * parseFloat(dblSalaryPerHour) * parseFloat(HolidayFactor);
            mRow.getCellFromKey('CounterHourOverTimeHolidayValue').setValue(parseFloat(Total))
            SetSummation(mRow)
            GetColSum("VacationOvertimeHours")
            GetColSum("CounterHourOverTimeHolidayValue")

            bEnterEditMode = false;
            cellEmployees.activate()
            cellEmployees.beginEdit()
            oActiveCell = cellEmployees
            bEnterEditMode = true;
            CheckVacationType_Is_Edit = 0;
        } //=================================================================================
        else if (CheckVacationType_Is_Edit == 2) {
            var strFactor = result;
            var OverTimeFactor = strFactor.split("$")[0];
            var HolidayFactor = strFactor.split("$")[1];
            var CellValue = cellCode.getValue();
            var Total = 0;
            var dblSalaryPerHour = mRow.getCellFromKey('lblSalaryPerHour').getValue();
            Total = parseFloat(CellValue) * parseFloat(dblSalaryPerHour) * parseFloat(OverTimeFactor);
            mRow.getCellFromKey('OvertimeHoursValue').setValue(parseFloat(Total))
            SetSummation(mRow)
            GetColSum("OvertimeHours")
            GetColSum("OvertimeHoursValue")

            bEnterEditMode = false;

            cellVacationsTypesID.activate()
            cellVacationsTypesID.select()
            cellVacationsTypesID.beginEdit()
            oActiveCell = cellVacationsTypesID
            bEnterEditMode = true;
            CheckVacationType_Is_Edit = 0;
        } //=================================================================================
    } //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
function OnFailed(error) {
    //alert(error.get_message());
}