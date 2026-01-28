


function wdcEndOfServiceDate_ValueChanged(oDateChooser, newValue, oEvent) {
    if (newValue == null) {
        var nDate = new Date();
        oDateChooser.setValue(nDate);
    }
}


function txtRegularHoursLostFocusToGrid() {
    var grid = igtbl_getGridById("uwgVacations");
    var nextEditCell;

    if (grid.Rows.rows.length == 0)
        nextEditCell = igtbl_getCellById("uwgVacations_anc_1");
    else
        nextEditCell = igtbl_getCellById("uwgVacations_rc_0_1");

    if (nextEditCell != null) {
        nextEditCell.activate();
        nextEditCell.beginEdit();
    }
}


/////////////////////
var strCode = '';
var Control;
function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId, button) {

    var Grid = igtbl_getGridById(gridName);
    var Cell = igtbl_getCellById(cellId);
    var Row = Cell.getRow()
    var RowId = GetRowIndexFromCellId(cellId);
    var CellCode = Row.getCell(6).getValue();
    Control = window.document.getElementById("txtHidden1");
    strCode = Control.value;
    if (Cell.Column.Key == "Prepare") {
        if (Cell.getValue() == "true") {
            if (strCode.length > 0) {
                if (strCode.indexOf("$" + CellCode + "$") == -1) {
                    strCode += "$" + CellCode + "$-";
                }
            }
            else {
                strCode += "$" + CellCode + "$-";

            }
        }
        else {
            if (strCode.indexOf("$" + CellCode + "$") >= 0) {
                strCode = strCode.replace("$" + CellCode + "$-", "");
            }
        }
        Control.text = strCode;
        Control.value = strCode;
    }
}

//   '========================================================================
//    'ProcedureName  :  GetRowIndexFromCellId 
//    'Screen         :  frmEmployeeMonthlyTransactions
//    'Project        :  Venus V.
//    'Description    :  Get row index from cell id
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  03-09-2007
//    'fn. Arguments  :
//    '---------------------------------------------------------
//    'Parmeter Name      : Data Type : Description
//    '---------------------------------------------------------
//     cellId             :cell id
//    '========================================================================
function GetRowIndexFromCellId(cellId) {
    var arr = cellId.split('_');
    var tt = arr[2] - 1;
    tt = tt + 1;
    return tt;
}

//  [0256] Change the Script to suit new Design
function DDL_LostFocusToGrid(gridName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var Control = igtab_getElementById(gridName, webTab.element);
    var grid = igtbl_getGridById(Control.id);
    var txtCode = webTab.findControl("txtCode");
    //var txtCode           = window.document.getElementById(gridName);
    var nextEditCell;

    if (grid.Rows.length == 0)
        txtCode.foucus();
    else {
        nextEditCell = igtbl_getCellById(grid.Id + "_rc_0_0");
        nextEditCell.activate();
        nextEditCell.beginEdit();
    }

}



function Check_Pass_Top_Score() {
    //----------------------------------------------------
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var txtTopScore = igedit_getById("UltraWebTab1__ctl0_txtTopScore");
    var txtPassingScore = igedit_getById("UltraWebTab1__ctl0_txtPassingScore");
    //----------------------------------------------------

    if (txtPassingScore.getValue() > txtTopScore.getValue()) {
        txtPassingScore.setValue(txtTopScore.getValue());
    }
}

var fnCheckVacationCount = "";
function SaveVacationTypes() {
    fnCheckVacationCount = "SaveVacationTypes";
    PageMethods.CheckVacationCount(OnSucceeded, OnFailed);
}

function ClearAnnualCheckBox() {
    fnCheckVacationCount = "ClearAnnualCheckBox";
    PageMethods.CheckVacationCount(OnSucceeded, OnFailed);
}

function ClearSickCheckBox() {
    fnCheckVacationCount = "ClearSickCheckBox";
    PageMethods.CheckVacationCount(OnSucceeded, OnFailed);
}

//====End=====frmVacationsTypes.aspx





//===========Start**********frmDocumentTypes==

//    Screen         :  frmDocument.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Modified  :  24-04-2008
//    function name  :  frmDocument_txtArabicLostFocusToGrid
function frmDocument_txtArabicLostFocusToGrid(gridName) {
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgForNationality");

    var nextEditCell;

    if (grid.Rows.length == 0) {
        nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgForNationality_anc_1");
        frmDocument_GridLostFocus(gridName, "#_#_#_#")
    }
    else {
        nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgForNationality_rc_0_0");
        nextEditCell.activate();
        nextEditCell.beginEdit();
    }



}

//    Screen         :  frmDocument.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Modified  :  24-04-2008
//    function name  :  frmDocument_GridLostFocus
function frmDocument_GridLostFocus(gridName, cellId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var CtrTxt = webTab.findControl("txtCode");

    var lastGrid = igtbl_getGridById("UltraWebTab1xxctl0xuwgForNationality");
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgOnlyForProfession");
    var nextEditCell;
    var cell = igtbl_getCellById(cellId);
    var RowsCount = lastGrid.Rows.length - 1;

    var arrCount = cellId.split("_");

    if (arrCount[2] == RowsCount && arrCount[3] == "2") {
        if (grid.Rows.length == 0) {
            // nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgOnlyForProfession_anc_1");
            CtrTxt.focus();
        }

        else {

            nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgOnlyForProfession_rc_0_0");
            nextEditCell.activate();
            nextEditCell.beginEdit();
        }

    }
    else if (arrCount[2] == "#" && arrCount[3] == "#") {
        frmDocument_NextGridLostFocus(gridName, "UltraWebTab1xxctl0xuwgOnlyForProfession_rc_0_0")
    }



}





function frmDocument_NextGridLostFocus(gridName, cellId) {
    //var ultraTab       = igtab_getTabById("UltraWebTab1");

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var CtrTxt = webTab.findControl("txtCode");

    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgOnlyForProfession");
    var cell = igtbl_getCellById(cellId);

    var RowsCount = grid.Rows.length - 1;

    var arrCount = cellId.split("_");

    if (arrCount[2] == RowsCount && arrCount[3] == "2") {

        CtrTxt.focus();
    }
    else if (arrCount[2] == "#" && arrCount[3] == "#") {

        CtrTxt.focus();
    }
    else {
        cell.activate();
        cell.beginEdit()
    }


}


//===========End************frmDocumentTypes======














function Open_Search_KeyDown(SearchID, ControlName) {
    var e = window.event;
    if (e.keyCode == 120) {
        var winopen = window.open("./Pages/HR/frmSearchScreen.aspx?TargetControl=" + ControlName + "&SearchID=" + SearchID, "_Parent" + 1, "height=525,width=724,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        winopen.document.focus();
    }
}


//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  get the description of the formula.
//    Developer      :  [0260]
//    Date Created   :  20-04-2008
//    function name  :  txtFormula_TextChanged()
//      
//=========================== [Start]
function txtFormula_TextChanged() {

    //================================
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var ctrl = webTab.findControl("txtFormula");
    var ctrlFormulaDesc = webTab.findControl("lblFormulaDesc");

    //================================


    if (ctrl.value != "")
        getFormulaValue(ctrl.value);
    else {
        ctrlFormulaDesc.style.border = "0px solid";
        ctrlFormulaDesc.value = "";
        ctrlFormulaDesc.innerText = "";
    }
}


//========================================

//===========Screen     : OtherFields
//===========Developer  :[0261]
//===========Date       :[20-04-2008]
//1- Add This Function
function Change_Data_Lenght() {
    var ddlDataTypes = window.document.getElementById("ddlDataTypes").value;
    var txtDataLength = igedit_getById("txtDataLength");

    if (ddlDataTypes == "Charachters") {
        txtDataLength.setValue(8000);
    }
    else if (ddlDataTypes == "Numeric") {
        txtDataLength.setValue(8);
    }
    else if (ddlDataTypes == "Boolean") {
        txtDataLength.setValue(1);
    }
    else if (ddlDataTypes == "DateTime") {
        txtDataLength.setValue(8);
    }


}


//2- Add This Function
//===========Screen     : OtherFields
//===========Developer  :[0261]
//===========Date       :[20-04-2008]
function Set_MAx_Value() {
    var ddlDataTypes = window.document.getElementById("ddlDataTypes").value;
    var txtDataLength = igedit_getById("txtDataLength");
    //Private arrDataLength() As Integer = {8000, 8, 1, 8}
    //Private arrDataTypes() As String = {"Charachters", "Numeric", "Boolean", "DateTime"}
    if (ddlDataTypes == "Charachters") {
        txtDataLength.setMaxValue(8000);
    }
    else if (ddlDataTypes == "Numeric") {
        txtDataLength.setMaxValue(8);
    }
    else if (ddlDataTypes == "Boolean") {
        txtDataLength.setMaxValue(1);
    }
    else if (ddlDataTypes == "DateTime") {
        txtDataLength.setMaxValue(8);
    }
}







//   ========================================================================
//   ProcedureName  :  uwgBenetitTemplet_CellChangeHandler
//   Screen         :  frmEmployeeClasses
//   Project        :  Venus V.
//   Description    :  focus to the required working months ,duration days cells by pressing tab or enter.  
//                     
//   Developer      :  [AIM]   
//   Date Created   :  24-04-2008
//   ---------------------------------------------------------
//   Parmeter Name      : Data Type : Description
//   ---------------------------------------------------------
//    gridName           : grid name
//    cellId             : cell id
//   ========================================================================
//=============================[0260] [Start]
function uwgBenetitTemplet_CellChangeHandler(gridName, cellId) {
    if (cellId == null) return;
    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var NextCell = cell;
    if (cell.Column.Index <= 1)
        NextCell = cell.Row.getCellFromKey("RequiredWorkingMonths");
    else return;
    if (NextCell != undefined) {
        NextCell.activate();
        if (NextCell.Column.Index != 2)
            NextCell.beginEdit();
        return true;
    }
}
//=============================[0260] [End]



//    ========================================================================
//    Screen         :  frmGradesSteps
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  focus to grid.
//    Developer      :  [0260]
//    Date Created   :  24-04-2008
//      
//=========================== [Start]

function txtArbNameLostFocusToGrid() {
    var grid = igtbl_getGridById("uwgGradesStepsTransactions");
    var nextEditCell;
    if (grid.Rows.rows.length == 0)
        nextEditCell = igtbl_getCellById("uwgGradesStepsTransactions_anc_1");
    else
        nextEditCell = igtbl_getCellById("uwgGradesStepsTransactions_rc_0_1");
    if (nextEditCell != null) {
        nextEditCell.activate();
        nextEditCell.beginEdit();
    }
}
//======[End]


// OtherFields Enhancements Scripts 
//================================= [Begin]
//Developer :  [0256]
function MainToolBarSave_Other_Fields() {
    var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
    TlbMainToolbarNotNavigation_Click();
    if (tlbControl.Items.fromKey("Save").Selected == true)
        SaveOtherFieldsData()
}


//function MainToolBarSave_Other_Fields()
//{
//} 
//================================= [End]

//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Open Formula Screen
//    Developer      :  [0261]
//    Date Created   :  05-05-2008
//    function name  :  Open_Formula_Screen()
//
function Open_Formula_Screen() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    control = webTab.findControl("txtFormula");
    var queryString = "?ControlName=" + control.name + "&ControlValue=" + control.value + "&ControlType=T";
    window.open("frmFormulaDesigner.aspx" + queryString, "_blank", "height=320px,width=787px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");

}



////////////////////////////////////////////////////////////
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function to handle check of basic slary check box(uncheck End of sevice check).
//    Developer      :  [0260]
//    Date Created   :  30-04-2008
//    function name  :  clearendofservicechb()
//      
//////////////////////////////[Start]
function clearendofservicechb() {

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var basic = webTab.findControl("ddlIsBasicSalary");
    var end = webTab.findControl("ddlIsEndOfService");

    if (basic.value == "True") {
        if (end.value == "True") {
            end.value = "False";
        }
    }
}


////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function to handle check of end of service check box(uncheck Basic salary check).
//    Developer      :  [0260]
//    Date Created   :  30-04-2008
//    function name  :  clearbasicsalarychb()
//      
//////////////////////////////[Start]
function clearbasicsalarychb() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var basic = webTab.findControl("ddlIsBasicSalary");
    var end = webTab.findControl("ddlIsEndOfService");

    if (end.value == "True") {
        if (basic.value == "True") {
            basic.value = "False";
        }
    }
}
////////////////////////////[End]   

//////////////////////// 15-05-2008 Fiscal Periods
////////////////////////////////////////////////////////////
//    Screen         :  frmFiscalPeriods.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Bind Data To Controls When Selected Row Cahanged
//    Developer      :  [0261]
//    Date Created   :  11-05-2008
//    function name  :  uwgFiscalYearsPeriods_AfterSelectChangeHandler(gridName, id)
//
//////////////////////////////[Start]
var FromDate;
var ToDate;
var txtCode;
function uwgFiscalYearsPeriods_AfterSelectChangeHandler(gridName, id) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ddlFiscalYear = webTab.findControl("ddlFiscalYear");
    var txtEngName = webTab.findControl("txtEngName");
    var txtArbName = webTab.findControl("txtArbName");
    var WebDateChooser1 = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser1');
    var WebDateChooser2 = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser2');
    txtCode = igtab_getElementById("txtCode", webTab.element);
    //var oldFPID = txtCode.value ;

    //var Cell        = igtbl_getCellById(cellId);
    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(id);
    //Get Row Data
    var FPID = Row.getCell(0).getValue();
    var FYID = Row.getCell(1).getValue();
    var EngName = Row.getCell(2).getValue();
    var ArbName = Row.getCell(3).getValue();
    FromDate = Row.getCell(4).getValue();
    ToDate = Row.getCell(5).getValue();
    var FiscalYearsName = Row.getCell(6).getValue();

    var RegUserID = Row.getCell(7).getValue();
    var RegComputerID = Row.getCell(8).getValue();
    var RegDate = Row.getCell(9).getValue();
    var CancelDate = Row.getCell(10).getValue();


    //Set Row Data To Controls
    if (FPID != null && FPID != null) {
        PageMethods.GetControlsStatus(FYID, FPID, OnSucceeded, OnFailed);
    }

    //ddlFiscalYear        =   
    if (FPID != null) {
        txtCode.value = FPID;
    }
    else {
        PageMethods.GetLastDate(ddlFiscalYear.value, OnSucceeded, OnFailed);
    }
    if (EngName != null) {
        txtEngName.value = EngName;
    }
    else {
        txtEngName.value = "";
    }
    if (ArbName != null) {
        txtArbName.value = ArbName;
    }
    else {
        txtArbName.value = "";
    }

    WebDateChooser1.setValue(FromDate);
    WebDateChooser2.setValue(ToDate);
}


//***************************** frmFiscalPeriods [Satrt]
////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen         :  frmFiscalPeriods.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Set TlbMainToolbar Events When Clicked & Save Other Fields Data 
//    Developer      :  [0261]
//    Date Created   :  12-05-2008
//    function name  :  frmFiscalPeriods_MainToolBarSave_Other_Fields()
//      
//////////////////////////////[Start]
function frmFiscalPeriods_MainToolBarSave_Other_Fields() {
    var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
    // SubmitFlag=0;
    TlbMainToolbarNotNavigation_Click();
    if (tlbControl.Items[2].Selected == true)
        SaveOtherFieldsData()
}

//***************************** frmFiscalPeriods [Satrt]
////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen         :  frmFiscalPeriods.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Set TlbNavigationToolbar Data when Row Selected Changed
//    Developer      :  [0261]
//    Date Created   :  12-05-2008
//    function name  :  frmFiscalPeriods_MainToolBarSave_Other_Fields()
//      
//////////////////////////////[Start] 

var txtEngName;
var txtArbName;
var wdcStartDate;
var wdcEndDate;
var txtRowIndex;

function uwgFiscalYearsPeriods_AfterRowActivateHandler(gridName, rowId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    IsDataChanged = "F"

    var Grid = igtbl_getGridById(gridName);
    var row = igtbl_getRowById(rowId)

    txtRowIndex = rowId//igtab_getElementById("txtRowIndex",webTab.element).value
    txtEngName = row.getCellFromKey("EngName").Element.outerText;
    txtArbName = row.getCellFromKey("ArbName").Element.outerText;
    wdcStartDate = row.getCellFromKey("FromDate").Element.outerText;
    wdcEndDate = row.getCellFromKey("ToDate").Element.outerText;

    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
        PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermission, OnSucceeded, OnFailed)
        PageMethods.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfo, OnSucceeded, OnFailed)
    }
    else {
        PageMethods.GetRecordPermissionAjax(0, callback_GetRecordPermission, OnSucceeded, OnFailed)
        PageMethods.GetRecordInfoAjax(0, call_backRecordInfo, OnSucceeded, OnFailed)
    }

}

function uwgFiscalYearsPeriods_BeforeRowDeactivateHandler(gridName, rowId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var txtEngNameT = webTab.findControl("txtEngName").value;
    var txtArbNameT = webTab.findControl("txtArbName").value;
    var wdcStartDateT = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser1').getText();
    var wdcEndDateT = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser2').getText();
    var bChangeFound = false;
    if (txtRowIndex != null && txtRowIndex == rowId) {
        if (txtEngNameT != txtEngName)
            bChangeFound = true;
        if (txtArbNameT != txtArbName)
            bChangeFound = true;
        if (wdcStartDateT != wdcStartDate)
            bChangeFound = true;
        if (wdcEndDateT != wdcEndDate)
            bChangeFound = true;
    }
    if (bChangeFound) {
        var msg = returnDiscardMsg();
        if (window.confirm(msg))
            IsDataChanged = "F";
        else
            return 1;

    }
}

//***************************** frmFiscalPeriods [Satrt]
////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen            :  frmFiscalPeriods.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate To Date
//    Developer         :  [0261]
//    Date Modification :  12-05-2008
//    function name     :  frmFYPWebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent)
//      
//////////////////////////////[Start] 
function frmFYPWebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent) {

    var oComboFrom = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser1');
    var oComboTo = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser2');

    var valueFrom = oComboFrom.getValue()
    var nD = new Date
    nD.setFullYear(valueFrom.getFullYear(), valueFrom.getMonth(), valueFrom.getDate() + 1)
    if (newValue <= valueFrom) {
        oComboTo.setValue(nD)
    }
}

//***************************** frmFiscalPeriods [Satrt]
////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen            :  frmFiscalPeriods.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate From Date
//    Developer         :  [0261]
//    Date Modification :  12-05-2008
//    function name     :  frmFYPWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent)
//      
//////////////////////////////[Start] 

function frmFYPWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent) {
    var oComboTo = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser2');
    var oComboFrom = igdrp_getComboById('UltraWebTab1__ctl0_WebDateChooser1');

    var valueTo = oComboTo.getValue()
    var nD = new Date
    nD.setFullYear(valueTo.getFullYear(), valueTo.getMonth(), valueTo.getDate() - 1)
    //nD.setFullYear(newValue.getFullYear(),newValue.getMonth()+1,newValue.getDate()-1)

    if (newValue >= valueTo) {
        oComboFrom.setValue(nD)
        //oComboTo.setValue(nD)
    }
}
//End FrmFiscalYearPeriods
//***************************** frmFiscalPeriods [End]


function SetDataChangedToFalse() {
    IsDataChanged = "F";
}

function ChangeIsDataChanged() {
    if (isFormChanged()) {
        IsDataChanged = "T";
    }
    else {
        IsDataChanged = "F";
    }
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

    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")
    var delItem = tlbMain.Items.fromKey("Delete")
    if (arr[2] != "")
        delItem.setEnabled(false)
}

////////// Employees Items Screen
////////////////////////////////////////////////////////////
//    Screen         :  frmEmployeesItems.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Get Item Name & Code
//    Developer      :  [0261]
//    Date Created   :  11-05-2008
//    function name  :  Get_Item_Description ()
//      
//////////////////////////////[Start] 
//function Get_Item_Description() {
//    var webTab = igtab_getTabById("UltraWebTab1");
//    if (webTab == null)
//        return;
//    var txtEmp = webTab.findControl("txtEmployee");
//    var Item = webTab.findControl("txtItem");
//    var ItemName = webTab.findControl("txtItemName");
//    var ItemID = igtab_getElementById("txtItemID", webTab.element);
//    var btnSearchItem = igtab_getElementById("btnSearchItem", webTab.element);
//    var txtID = igtab_getElementById("txtID", webTab.element);
//    var txtReceivedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReceivedDate')
//    var txtReceivingItemstatus = webTab.findControl("txtReceivingItemstatus");
//    var txtReturnedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')
//    var txtReturnedItemstatus = webTab.findControl("txtReturnedItemstatus");
//    var strValues = '';


//    strValues = Interfaces_frmEmployeesItems.GetItem_ID_And_Name(Item.value);

//    if (strValues.value != "/" && strValues.value != null) {
//        var strTemp = strValues.value.split("/");

//        ItemID.value = strTemp[0];
//        ItemID.innerText = strTemp[0];
//        if (strTemp[1] != "") {
//            ItemName.value = strTemp[1];
//            ItemName.innerText = strTemp[1];
//            ItemName.style.borderWidth = "1px";
//            //txtReceivedDate.setValue("");
//            //txtReceivingItemstatus.value    =   "";
//            //txtReturnedDate.setValue("");
//            //txtReturnedItemstatus.value     =   "";
//        }
//        else {

//            ItemName.value = "";
//            ItemName.innerText = "";
//            ItemName.style.borderWidth = "0px";
//            Item.value = "";
//        }

//    }
//    else {
//        Item.value = "";
//        ItemID.value = "";
//        ItemID.innerText = "";
//        ItemName.value = "";
//        ItemName.innerText = "";
//        ItemName.style.borderWidth = "0px";
//    }



//}

////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen         :  frmEmployeesItems.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Bind Data To Controls When Selected Row Cahanged
//    Developer      :  [0261]
//    Date Created   :  11-05-2008
//    function name  :  uwgEmployeeItems_AfterSelectChangeHandler(gridName, id)
//      
//////////////////////////////[Start] 


function uwgEmployeeItems_AfterSelectChangeHandler(gridName, id) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var txtItem = webTab.findControl("txtItem");
    var txtID = igtab_getElementById("txtID", webTab.element);
    var txtEmpID = igtab_getElementById("txtEmpID", webTab.element);
    var txtReceivedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReceivedDate')
    var txtReceivingItemstatus = webTab.findControl("txtReceivingItemstatus");
    var txtReturnedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')
    var txtReturnedItemstatus = webTab.findControl("txtReturnedItemstatus");

    var btnSearchItem = igtab_getElementById("btnSearchItem", webTab.element);
    //var Cell        = igtbl_getCellById(cellId);
    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(id)
    //Get Row Data
    var intID = Row.getCell(0).getValue();
    var intEmpID = Row.getCell(1).getValue();
    var ItemCode = Row.getCell(2).Element.innerText;
    var intItemID = Row.getCell(3).getValue();


    var CellReceivedDate = Row.getCell(4).getValue();
    var CellReceivingItemstatus = Row.getCell(5).getValue();
    var CellReturnedDate = Row.getCell(6).getValue();
    var CellReturningItemstatus = Row.getCell(7).getValue();

    var RegUserID = Row.getCell(8).getValue();
    var ComputerID = Row.getCell(9).getValue();
    var RegDate = Row.getCell(10).getValue();
    var CancelDate = Row.getCell(11).getValue();



    //Set Row Data To Controls  
    txtID.value = intID;
    txtItem.value = ItemCode;
    Get_Item_Description();

    txtItem.readOnly = true;
    btnSearchItem.disabled = true;

    txtReceivedDate.setValue(CellReceivedDate)
    if (CellReceivingItemstatus != null) {
        txtReceivingItemstatus.value = CellReceivingItemstatus
    }
    else {
        txtReceivingItemstatus.value = "";
    }
    txtReturnedDate.setValue(CellReturnedDate)
    if (CellReturningItemstatus != null) {
        txtReturnedItemstatus.value = CellReturningItemstatus
    }
    else {
        txtReturnedItemstatus.value = "";
    }

    if (CellReturnedDate != null) {
        txtReceivedDate.readOnly = true;
        txtReturnedDate.readOnly = true;
        txtReturnedItemstatus.readOnly = false;
        txtReceivingItemstatus.focus();
    }
    else {
        txtReturnedItemstatus.readOnly = true;
        txtReceivedDate.readOnly = false;
        txtReturnedDate.readOnly = false;
        txtReceivedDate.focus();
    }


}

////////////////////////////////////////////////////////////
//    Screen            :  frmEmployeesItems.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate Date
//    Developer         :  [0261]
//    Date Modification :  18-05-2008
//    function name     :  txtReceivedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent)
//      
//////////////////////////////[Start] 
//Tamer Items Screen    
function txtReceivedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent) {
    var ReturnDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')
    var RecievDate = oDateChooser.getValue()
    if (RecievDate > ReturnDate.getValue()) {
        //alert("Invalid Return Date!")
        ReturnDate.setValue('')
        oDateChooser.focus
    }
}


////////////////////////////////////////////////////////////
//    Screen            :  frmEmployeesItems.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate Date
//    Developer         :  [0261]
//    Date Modification :  18-05-2008
//    function name     :  txtReceivedDate_ValueChanged(oDateChooser, newValue, oEvent)
//      
//////////////////////////////[Start] 

function txtReceivedDate_ValueChanged(oDateChooser, newValue, oEvent) {
    var ReturnDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')
    var RecievDate = oDateChooser.getValue()
    if (RecievDate > ReturnDate.getValue()) {
        //alert("Invalid Return Date!")
        ReturnDate.setValue('')
        oDateChooser.focus
    }
}



////////////////////////////////////////////////////////////
//    Screen            : frmEmployeesItems.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate Date
//    Developer         :  [0261]
//    Date Modification :  18-05-2008
//    function name     :  txtReturnedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent)
//      
//////////////////////////////[Start] 

function txtReturnedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent) {
    var RecievDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReceivedDate')
    var ReturnDate = oDateChooser.getValue()
    if (RecievDate.getValue() > ReturnDate) {
        alert("Invalid Return Date!")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}


////////////////////////////////////////////////////////////
//    Screen            :  frmEmployeesItems.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Validate Date
//    Developer         :  [0261]
//    Date Modification :  18-05-2008
//    function name     :  txtReturnedDate_ValueChanged(oDateChooser, newValue, oEvent)
//      
//////////////////////////////[Start] 

function txtReturnedDate_ValueChanged(oDateChooser, newValue, oEvent) {
    var RecievDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReceivedDate')
    var ReturnDate = oDateChooser.getValue()
    if (RecievDate.getValue() > ReturnDate) {
        alert("Invalid Return Date!")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}




////////////////////////////////[End]
////////////////////////////////////////////////////////////
//    Screen         :  frmEmployeesItems.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Bind Data To Controls When Selected Row Cahanged
//    Developer      :  [0261]
//    Date Created   :  15-05-2008
//    function name  :  uwgEmployeeItems_AfterRowActivateHandler(gridName, rowId)
//      
//////////////////////////////[Start] 

//function uwgEmployeeItems_AfterRowActivateHandler(gridName, rowId) {
//    var Grid = igtbl_getGridById(gridName);
//    var row = igtbl_getRowById(rowId)
//    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
//        Interfaces_frmEmployeesItems.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermission)
//        Interfaces_frmEmployeesItems.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfo)
//    }
//}

function uwgEmployeeItems_BeforeRowActivateHandler(gridName, rowId) {
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




////////////////////////////////////////////////////////////
//    Screen         :  frmEmployeesItems.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  function Bind Data To Controls When Selected Row Cahanged
//    Developer      :  [0261]
//    Date Created   :  15-05-2008
//    function name  :  frmEmpItems_MainToolBarSave_Other_Fields()
//      
//////////////////////////////[Start]

//===========================================================
function frmEmpItems_MainToolBarSave_Other_Fields() {
    var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
    var txtReceivedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReceivedDate')
    var txtReturnedDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var txtEmp = webTab.findControl("txtEmployee");

    SubmitFlag = 0;
    TlbMainToolbarNotNavigation_Click();

    //Save 
    if (tlbControl.Items[2].Selected == true) {

        if (txtEmp.value == "") {
            alert("Please Choose Employee First.")
            SubmitFlag = 1;
        }
    }

    //received Date = null 
    if (tlbControl.Items[2].Selected == true) {
        if (txtReceivedDate.getValue() == undefined) {
            alert("Please Enter Received Date First.")
            SubmitFlag = 1;
            //received Date = null  And Return Date != null
            if (txtReturnedDate.getValue() != undefined) {
                txtReturnedDate.setValue(undefined);
            }
        }
        //received Date != null 
        else {
            //received Date != null And Return Date != null
            if (txtReturnedDate.getValue() != undefined) {
                //Return Date Less Than received Date
                if (txtReturnedDate.getValue() < txtReceivedDate.getValue()) {
                    txtReturnedDate.setValue(undefined);
                }
            }
        }
    }
}

//-------------------------------------
////////////////////////////////////////////////////////////
//    Screen            :  frmEmployeesItems.aspx
//    Project           :  Venus V.
//    Module            :  PayRoll
//    Description       :  Open Search Screen
//    Developer         :  [0261]
//    Date Creation     :  18-05-2008
//    function name     :   frmEmpItems_Open_Search_KeyDown(SearchID,ControlName)
//      
//////////////////////////////[Start] 
function frmEmpItems_Open_Search_KeyDown(SearchID, ControlName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var control = webTab.findControl(ControlName);

    var e = window.event;
    if (e.keyCode == 120) {
        if (control.readOnly == false) {
            var winopen = window.open("./Pages/HR/frmSearchScreen.aspx?TargetControl=" + ControlName + "&SearchID=" + SearchID, "_Parent" + 1, "height=615,width=724,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
            winopen.document.focus();
        }
    }
}


//*******************************************************
function callback_GetRecordPermission(res) {
    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")

    var saveItem = tlbMain.Items.fromKey("Save");
    var delItem = tlbMain.Items.fromKey("Delete");
    var printItem = tlbMain.Items.fromKey("Print");
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var strFormPermission = igtab_getElementById("txtFormPermission", webTab.element).value;

    saveItem.setEnabled(true);
    delItem.setEnabled(true);
    printItem.setEnabled(true);

    if (strFormPermission != "") {
        var arrform = strFormPermission.split(",");
        if (arrform[0] != "0")
            saveItem.setEnabled(false);
        if (arrform[1] != "0")
            delItem.setEnabled(false);
        if (arrform[2] != "0")
            printItem.setEnabled(false);
    }

    var arr = res.split(",");
    if (arr[0] != "1")
        saveItem.setEnabled(false);
    if (arr[1] != "1")
        delItem.setEnabled(false);
    if (arr[2] != "1")
        printItem.setEnabled(false);
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



function frmEmpItem_CheckReturnDate(oDateChooser, dummy, oEvent) {
    var ReturnDate = igdrp_getComboById('UltraWebTab1__ctl0_txtReturnedDate')
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null) return;
    var txtReturnedItemstatus = webTab.findControl("txtReturnedItemstatus");
    if (ReturnDate.getValue() == undefined) {
        txtReturnedItemstatus.readOnly = true;
        txtReturnedItemstatus.value = "";
        txtReturnedItemstatus.innerText = "";
    }
    else {
        txtReturnedItemstatus.readOnly = false;
        txtReturnedItemstatus.focus();
    }
}




//----------------------------------------------------------------------------------------------------------



function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'GetControlsStatus') {
        var strStatus = result;
        if (strStatus != "" && strStatus != null) {
            var arrStatus = strStatus.split("/");

            if (arrStatus[0] == "True") {
                arrStatus[0] = true;
            }
            else {
                arrStatus[0] = false;
            }

            if (arrStatus[1] == "True") {
                arrStatus[1] = true;
            }
            else {
                arrStatus[1] = false;
            }
        }
    }
    else if (methodName == 'GetLastDate') {
        var strDates = result;
        var arrDates = strDates.split("$");
        FromDate = arrDates[0];
        ToDate = arrDates[1];

        txtCode.value = "";
    }
    else if (methodName == 'CheckVacationCount') {

        TlbMainToolbarNotNavigation_Click();
        var ctrlAnnual = window.document.getElementById("chkAnnual");
        var ctrlSick = window.document.getElementById("chkSick");

        var ctrlErrorAnnual = window.document.getElementById("lblErrorAnnual");
        var ctrlErrorSick = window.document.getElementById("lblErrorSick");

        var strCount;
        strCount = result;
        var strCountValue = strCount;
        var arrCount = strCountValue.split("/");

        if (fnCheckVacationCount == 'SaveVacationTypes') {

            //========Annual=========
            if (ctrlAnnual.checked == true) {
                if (arrCount[0] > 0) {
                    PageMethods.UpdateAnnual(OnSucceeded, OnFailed)
                }
            }
            else {
                if (arrCount[0] == 0) {
                    if (ctrlSick.checked == false) {
                        var str = new String();
                        str = GetCookie("Lang");
                        if (str.indexOf("ar") > -1) {
                            if (confirm("لا يوجد أجازة مرضية حتى الان . هل تريد جعل هذا الحقل اجازة مرضية") == true) {
                                ctrlSick.checked = true;
                            }
                        }
                        else {
                            if (confirm("there is no vacation set as sick vacation, would you like to set this record as sick vacation ?") == true) {
                                ctrlSick.checked = true;
                            }
                        }
                    }
                }
            }
        }

        //========Sick=========

        if (ctrlSick.checked == true) {
            if (arrCount[1] > 0) {
                PageMethods.UpdateSick(OnSucceeded, OnFailed)
            }
        }
        else {
            if (arrCount[1] == 0) {
                if (ctrlAnnual.checked == false) {
                    var str = new String();
                    str = GetCookie("Lang");
                    if (str.indexOf("ar") > -1) {
                        if (confirm("لا يوجد أجازة سنوية حتى الان . هل تريد جعل هذا الحقل اجازة سنوية") == true) {
                            ctrlAnnual.checked = true;
                        }
                    }
                    else {
                        if (confirm("There is no vacation set as Annual Vacation, Would you like to set this record as Annual Vacation ?") == true) {
                            ctrlAnnual.checked = true;
                        }
                    }
                }
            }
        }
        if (fnCheckVacationCount == 'ClearAnnualCheckBox') {
            DataChanged();

            if (ctrlSick.checked == true) {
                if (ctrlAnnual.checked == true) {
                    ctrlAnnual.checked = false;
                }
                ctrlErrorSick.value = "";
                ctrlErrorSick.innerText = "";

                ctrlErrorAnnual.value = "";
                ctrlErrorAnnual.innerText = "";
            }
            else if (arrCount[1] >= 0) {
                if (ctrlSick.checked == false) {
                    var str = new String();
                    str = GetCookie("Lang");
                    if (str.indexOf("ar") > -1) {
                        ctrlErrorSick.value = "لا يوجد اجازة مرضية حتى الان";
                        ctrlErrorSick.innerText = "لا يوجد اجازة مرضية حتى الان";
                    }
                    else {
                        ctrlErrorSick.value = "There will be no Vacation set as Sick Vacation.";
                        ctrlErrorSick.innerText = "There will be no Vacation set as Sick Vacation.";
                    }
                }
            }
        }
    }

    if (fnCheckVacationCount == 'ClearSickCheckBox') {
        DataChanged();

        if (ctrlAnnual.checked == true) {
            if (ctrlSick.checked == true) {
                ctrlSick.checked = false;
            }
            ctrlErrorAnnual.value = "";
            ctrlErrorAnnual.innerText = "";

            ctrlErrorSick.value = "";
            ctrlErrorSick.innerText = "";
        }

        else if (arrCount[0] >= 0) {
            if (ctrlAnnual.checked == false) {

                var str = new String();
                str = GetCookie("Lang");
                if (str.indexOf("ar") > -1) {
                    ctrlErrorAnnual.value = "لا يوجد أجازة سنوية حتى الان";
                    ctrlErrorAnnual.innerText = "لا يوجد أجازة سنوية حتى الان";
                }
                else {
                    ctrlErrorAnnual.value = "There will be no Vacation set as Annual Vacation.";
                    ctrlErrorAnnual.innerText = "There will be no Vacation set as Annual Vacation.";
                }
            }
        }
    }
}

function OnFailed(error) {
    //alert(error.get_message());
}
