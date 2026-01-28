
//////////////////////////////////////////////////////////
// File Name    : App_Employees.Js
// Project      : Venus Payroll
// Developer    :  
// Date Created : 18-05-2008
//////////////////////////////////////////////////////////

var uwgVacationDetailsID = "OfficeEditClsxuwgVacationDetails";

//////////////////////////////////////////////////////////
// [0256]
// 18-05-2008
// CHECKS IF THERE'S AN EMPLOYEE DATA INTO SCREEN BEFORE DEALING WITH EMPLOYEE'S DETAILS
function UltraWebTab1_BeforeSelectedTabChange(oWebTab, oTab, oEvent) {
window.document.all
    if (oTab.Key != "Employees") {
        var ctrId = window.document.getElementById("txtEmpId");
        if (ctrId.value == "0" || ctrId.value == "") {
            oEvent.cancel = true;
        }
    }
}


//////////////////////////////////////////////////////////
// [0256]
// 18-05-2008
// Opens After checking the contract data employees periodical transactions screen
function Emp_ContractsTransactions_Click(oButton, oEvent) {

//    var webTab = igtab_getTabById("UltraWebTab1");
//    if (webTab == null)
//        return;

    var ctrPermission = window.document.getElementById("txtContractsTransactionsPermissions");
    if (ctrPermission.value == "1") {
        alert(" Sorry! Access is denied, You have no permission to open this Screen. ");
        return 0;
    }
    
   
//    var ctrContractId = window.document.getElementById("txtCId");
//    var ctrId = window.document.getElementById("txtEmpId");
//    var IntGradeStepId = igtab_getElementById("DdlGradeStep", webTab.element).value//selectedIndex
//    //========= 0257 [start]
//    if (IntGradeStepId == "") {
//        IntGradeStepId = 0
//    }
    //======= 0257 [end]
//    var StrCode = igtab_getElementById("txtCode2", webTab.element).value;
//    IntEmployeeId = ctrId.value;
//    var IntContractId = ctrContractId.value;
//    var StrEmpCode = igtab_getElementById("txtCode", webTab.element).value;
//    var StrEmpName = igtab_getElementById("txtEngName", webTab.element).value;
//    var ctrStartDate = igtab_getElementById("txtStartDate", webTab.element);
//    ctrStartDate = igdrp_getComboById(ctrStartDate.id);


//    if (StrEmpCode == "0" || StrEmpCode == "" || StrEmpCode == null) {
//        alert(" No employee is data is present yet! ");
//        return 0;
//    }
//    //    if (StrCode=="0" || StrCode=="") {
//    //        alert(" Current Employee dosn't has any contracts yet! " );
//    //        return ; }
//    if ((StrCode != "0" && StrCode != "") && (ctrStartDate.getValue() != null && ctrStartDate.getValue() != "" && ctrStartDate.getValue() != undefined)) {
////        var winopen = window.open("frmEmployeePeriodicalTransactions.aspx?EId=" + IntEmployeeId + "&CN=" + StrCode + "&GsId=" + IntGradeStepId + "&EmpC=" + StrEmpCode + "&EmpN=" + StrEmpName, "_Parent" + 1, "height=573,width=787,resizable=0,menubar=0,toolbar=0,addressbar=0 ,location=0,directories=0,scrollbars=0,status=0,dependent=No");
//        //        winopen.document.focus();
//        
//    }
//    else
//        alert("No selected contract or this employee not have contract yet ");

}



////////////////////////////////////////////////////////////
//   Screen         :  FrmEmployees.aspx
//   Project        :  Venus V.
//   Module         :  PayRoll
//   Function name  :  uwgDepandants_AfterRowActivateHandler
//   Developer      :  [0256]
//   Date Created   :  20-05-2008
//   Description    :  Gets records permissions
//////////////////////////////[Start] 
function uwgDepandants_AfterRowActivateHandler(gridName, rowId) {
    var Grid = igtbl_getGridById(gridName);
    var row = igtbl_getRowById(rowId);
    row.setSelected(true);
    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
        //PageMethods.GetRecordDepedantsPermissionAjax(row.getCellFromKey("ID").getValue(), callback_DepandentGetRecordPermission, OnSucceeded, OnFailed)
        //PageMethods.GetRecordDepedantsInfoAjax(row.getCellFromKey("ID").getValue(), call_backDepandentRecordInfo, OnSucceeded, OnFailed)

    }
    else
        return;
}

////////////////////////////////////////////////////////////
//   Screen         :  frmEmployees.aspx
//   Project        :  Venus V.
//   Module         :  PayRoll
//   Function name  :  uwgDepandants_AfterSelectChangeHandler
//   Developer      :  [0256]
//   Date Created   :  20-05-2008
//   Description    :  Gets records permissions
//   Modifications  :  [0260] 02-06-2008 handles if new mode return from function
//////////////////////////////[Start] 
function uwgDepandants_AfterSelectChangeHandler(gridName, id) {
    if (NewMode == true) return;
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = window.document.all.item("UltraWebTab1$_ctl0$txtEngName")
    var ctrArbName = window.document.all.item("UltraWebTab1$_ctl0$txtArbName")
    var ctrEmpDependantTypeId = window.document.all.item("UltraWebTab1$_ctl0$DdlDependantTypeID")
    var ctrNationalityId = webTab.findControl("DdlNationalityId");
    var ctrBirthDate = igdrp_getComboById("UltraWebTab1__ctl0_txtBirthDate");
    var ctrBirthCity = window.document.all.item("UltraWebTab1$_ctl0$DdlBirthCity2")
    var ctrSex = window.document.all.item("UltraWebTab1$_ctl0$DdlSex")
    var ctrInsuranceCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlInsuranceCovered")
    var ctrImage = window.document.all.item("UltraWebTab1__ctl0_ImgDependantImage")
    var dependentgrid = window.document.all.item("UltraWebTab1xxctl0xuwgDependents")
    var ctrPercentageInsurance = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageInsurance")
    var ctrTicketCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlTicketCovered")
    var ctrPercentageTicket = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageTicket")
    //var btnDependantsDocuments = ig_getWebControlById(igtab_getElementById("btnDependantsDocuments", webTab.element).id);
    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(id)

    //Get Row Data
    var IntID = Row.getCell(0).getValue();
    var StrEngName = Row.getCell(1).getValue();
    var StrArbName = Row.getCell(2).getValue();
    var IntEmpDependantTypeId = Row.getCell(3).getValue();
    var IntNationalityId = Row.getCell(5).getValue();

    var dteBirthDate = Row.getCell(4).getValue();
    var IntBirthCity = Row.getCell(6).getValue();
    var IntSex = Row.getCell(7).getValue();
    var IntInsuranceCovered = Row.getCell(8).getValue();


    var RegUserID = Row.getCell(9).getValue();
    var RegDate = Row.getCell(10).getValue();
    var CancelDate = Row.getCell(11).getValue();

    var FileName = Row.getCell(13).getValue();
    var ObjectId = Row.getCell(14).getValue();



    var IntPercentageInsurance = Row.getCell(15).getValue();
    var IntTicketCovered = Row.getCell(16).getValue();
    var IntPercentageTicket = Row.getCell(17).getValue();


    if (StrEngName == null)
        StrEngName = "";

    if (StrArbName == null)
        StrArbName = "";

    if (IntInsuranceCovered == null)
        IntInsuranceCovered = 0;

    //Set Row Data To Controls  
    ctrId.value = IntID;
    if (IntID == null) {
        //btnDependantsDocuments.setVisible(false);

    }
    else {
        //btnDependantsDocuments.setVisible(true);
    }
    ctrEngName.value = StrEngName;
    ctrArbName.value = StrArbName;
    ctrNationalityId.value = IntNationalityId;
    ctrEmpDependantTypeId.value = IntEmpDependantTypeId;
    ctrBirthDate.setValue(dteBirthDate);
    ctrBirthDate.value = dteBirthDate;
    ctrBirthCity.value = IntBirthCity;
    ctrSex.value = IntSex;
    ctrInsuranceCovered.value = IntInsuranceCovered;

    ctrPercentageInsurance.value = IntPercentageInsurance;
    ctrTicketCovered.value = IntTicketCovered;
    ctrPercentageTicket.value = IntPercentageTicket;


    // Load Image 
    if (FileName != null) {
        ctrImage.ImageUrl = "../../Photos/" + ObjectId.toString() + "_" + IntID.toString() + "/" + FileName;
        ctrImage.src = "../../Photos/" + ObjectId.toString() + "_" + IntID.toString() + "/" + FileName;
    }
    else {
        ctrImage.ImageUrl = "";
        ctrImage.src = ""
    }

    var tlbControl = window.document.all.item("UltraWebTab1$_ctl0$btnDelete");
    if (CancelDate != null)
        tlbControl.enabled=false;
    else
        tlbControl.enabled=true;
        

}

//*******************************************************
function callback_DepandentGetRecordPermission(res) {
    if (res == null)
        return;
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var TempTbl = webTab.findControl("UltraWebTlbDependants");
    var tlbMainToolBar = igtbar_getToolbarById("TlbMainToolbar")
    var tlbMain = igtbar_getToolbarById(TempTbl.id)


    var saveItem = tlbMain.Items.fromKey("Save")
    var delItem = tlbMain.Items.fromKey("Delete")
    var printItem = tlbMainToolBar.Items.fromKey("Print")

    var arr = res.split(",")
    if (arr[0] != "1")
        saveItem.setEnabled(false)

    if (arr[1] != "1")
        delItem.setEnabled(false)

    if (arr[2] != "1")
        printItem.setEnabled(false)

}

function call_backDepandentRecordInfo(res) {

    //    var webTab = igtab_getTabById("UltraWebTab1");
    //    if (webTab == null)
    //        return ;
    //    var reguserItem                   = igtab_getElementById("lblDepRegUserId",webTab.element);
    //    var regdateItem                   = igtab_getElementById("lblDepRegDate",webTab.element);
    //    var canceldateItem                = igtab_getElementById("lblDepCancelDate",webTab.element);
    //    var arr = res.value.split(",")
    //    
    //    reguserItem.innerText             = arr[0]
    //    regdateItem.innerText             = arr[1]
    //    canceldateItem.innerText          = arr[2]
    //    
    //    reguserItem.value                 = arr[0]
    //    regdateItem.value                 = arr[1]
    //    canceldateItem.value              = arr[2]

}


//function Emp_Dependants_UltraWebTlbDependants_Click(oToolbar, oButton, oEvent) {

//    var webTab = igtab_getTabById("UltraWebTab1");
//    if (webTab == null)
//        return;
//    var grid = igtab_getElementById("uwgDependents", webTab.element);
//    grid = igtbl_getGridById(grid.id);
//    var tlbControl = igtab_getElementById("UltraWebTlbDependants", webTab.element);
//    tlbControl = igtbar_getToolbarById(tlbControl.id);
//    grid.clearSelectionAll();
//    if (oButton.Key == "New") {
//        blDoSubmit = 1;
//        var ctrId = window.document.getElementById("txtEmpDependantId");
//        var ctrEngName = igtab_getElementById("txtEngName", webTab.element);
//        var ctrArbName = igtab_getElementById("txtArbName", webTab.element);
//        var ctrEmpDependantTypeId = webTab.findControl("DdlDependantTypeID");
//        var ctrNationalityId = webTab.findControl("DdlNationalityId");
//        var ctrBirthDate = igtab_getElementById("txtBirthDate ", webTab.element);
//        ctrBirthDate = igdrp_getComboById(ctrBirthDate.id); //'UltraWebTab1__ctl0_WebDateChooser1'
//        var ctrBirthCity = igtab_getElementById("DdlBirthCity2", webTab.element);
//        var ctrSex = igtab_getElementById("DdlSex", webTab.element);
//        var ctrInsuranceCovered = igtab_getElementById("DdlInsuranceCovered", webTab.element);
//        var ctrImage = igtab_getElementById("ImgDependantImage", webTab.element);
//        //Update by abdulrahman-----------------------------------------------------------------------
//        var ctrPercentageInsurance = igtab_getElementById("txtPercentageInsurance", webTab.element);
//        var ctrTicketCovered = igtab_getElementById("DdlTicketCovered", webTab.element);
//        var ctrPercentageTicket = igtab_getElementById("txtPercentageTicket", webTab.element)
//        ctrPercentageInsurance.value = 0;
//        ctrTicketCovered.value = 0;
//        ctrPercentageTicket.value = 0;
//        //--------------------------------------------------------------------------------------------
//        ctrId.value = 0;
//        ctrEngName.value = "";
//        ctrArbName.value = "";
//        ctrNationalityId.value = 0;
//        //ctrEmpDependantTypeId.value           = 0
//        ctrBirthDate.setValue("");
//        ctrBirthCity.value = 0;
//        ctrSex.value = "M";
//        ctrInsuranceCovered.value = 0;
//        ctrImage.ImageUrl = "./DefaultDepndant.jpg"
//        tlbControl.Items.fromKey("Delete").setEnabled(false);
//    }
//    else
//        blDoSubmit = 0;
//    tlbControl.Items.fromKey("Delete").setEnabled(true);
//}


//function Emp_Dependants_btnDelete() {
//    var webTab = igtab_getTabById("UltraWebTab1");
//    if (webTab == null)
//        return;

//    var btnDelete = igtab_getElementById("btnDelete", webTab.element)

//    blDoSubmit = 0;
//    btnDelete.Enabled = false;
//}


//function Emp_Dependants_btnSave() {

//    var webTab = igtab_getTabById("UltraWebTab1");
//    if (webTab == null)
//        return;
//    var grid = igtab_getElementById("uwgDependents", webTab.element);
//    grid = igtbl_getGridById(grid.id);
//    grid.clearSelectionAll();
//    
//        blDoSubmit = 1;
//        var ctrId = window.document.getElementById("txtEmpDependantId");
//        var ctrEngName = igtab_getElementById("txtEngName", webTab.element);
//        var ctrArbName = igtab_getElementById("txtArbName", webTab.element);
//        var ctrEmpDependantTypeId = webTab.findControl("DdlDependantTypeID");
//        var ctrNationalityId = webTab.findControl("DdlNationalityId");
//        var ctrBirthDate = igtab_getElementById("txtBirthDate ", webTab.element);
//        ctrBirthDate = igdrp_getComboById(ctrBirthDate.id); 
//        var ctrBirthCity = igtab_getElementById("DdlBirthCity2", webTab.element);
//        var ctrSex = igtab_getElementById("DdlSex", webTab.element);
//        var ctrInsuranceCovered = igtab_getElementById("DdlInsuranceCovered", webTab.element);
//        var ctrImage = igtab_getElementById("ImgDependantImage", webTab.element);
//        var ctrPercentageInsurance = igtab_getElementById("txtPercentageInsurance", webTab.element);
//        var ctrTicketCovered = igtab_getElementById("DdlTicketCovered", webTab.element);
//        var ctrPercentageTicket = igtab_getElementById("txtPercentageTicket", webTab.element)
//        var btnDelete = igtab_getElementById("btnDelete", webTab.element)
//        
//        ctrPercentageInsurance.value = 0;
//        ctrTicketCovered.value = 0;
//        ctrPercentageTicket.value = 0;
//        ctrId.value = 0;
//        ctrEngName.value = "";
//        ctrArbName.value = "";
//        ctrNationalityId.value = 0;
//        //ctrEmpDependantTypeId.value           = 0
//        ctrBirthDate.setValue("");
//        ctrBirthCity.value = 0;
//        ctrSex.value = "M";
//        ctrInsuranceCovered.value = 0;
//        ctrImage.ImageUrl = "./DefaultDepndant.jpg"
//        btnDelete.Enabled=false;

//}



function uwgDependents_BeforeRowDeActivateHandler(gridName, rowId) {

    var row = igtbl_getRowById(rowId)
    if (NewMode == true) return 1;
    if (CheckDependantsFieldsUpdates(gridName, rowId) == true) {
        var msg = returnDiscardMsg();
        if (window.confirm(msg)) {
            IsDataChanged = "F";
        }
        else
            return 1;
    }

}



var blnnodependentChange = true;

function CheckDependantsFieldsUpdates(gridName, rowId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = window.document.all.item("UltraWebTab1$_ctl0$txtEngName")
    var ctrArbName = window.document.all.item("UltraWebTab1$_ctl0$txtArbName")
    var ctrEmpDependantTypeId = window.document.all.item("UltraWebTab1$_ctl0$DdlDependantTypeID")
    var ctrNationalityId = webTab.findControl("DdlNationalityId");
    var ctrBirthDate = igdrp_getComboById("UltraWebTab1__ctl0_txtBirthDate");
    var ctrBirthCity = window.document.all.item("UltraWebTab1$_ctl0$DdlBirthCity2")
    var ctrSex = window.document.all.item("UltraWebTab1$_ctl0$DdlSex")
    var ctrInsuranceCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlInsuranceCovered")
    var ctrImage = window.document.all.item("UltraWebTab1__ctl0_ImgDependantImage")
    var dependentgrid = window.document.all.item("UltraWebTab1xxctl0xuwgDependents")
    var ctrPercentageInsurance = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageInsurance")
    var ctrTicketCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlTicketCovered")
    var ctrPercentageTicket = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageTicket")

    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(rowId)
    if (Row == null)
        return false;
    //Get Controls Data 
    var IntID = Row.getCell(0).getValue();
    var StrEngName = Row.getCell(1).getValue();
    var StrArbName = Row.getCell(2).getValue();
    var IntEmpDependantTypeId = Row.getCell(3).getValue();
    var IntNationalityId = Row.getCell(5).getValue();

    var dteBirthDate = Row.getCell(4).getValue();
    var IntBirthCity = Row.getCell(6).getValue();
    var IntSex = Row.getCell(7).getValue();
    var IntInsuranceCovered = Row.getCell(8).getValue();

    var RegUserID = Row.getCell(9).getValue();
    var RegDate = Row.getCell(10).getValue();
    var CancelDate = Row.getCell(11).getValue();

 
    var IntPercentageInsurance = Row.getCell(15).getValue();
    var IntTicketCovered = Row.getCell(16).getValue();
    var IntPercentageTicket = Row.getCell(17).getValue();

    if (StrEngName == null)
        StrEngName = "";

    if (StrArbName == null)
        StrArbName = "";

    if (IntInsuranceCovered == null)
        IntInsuranceCovered = 0;


    //        if(ctrId.value==0 && ctrEngName.value=="" && ctrArbName.value=="" && ctrNationalityId.value==0 && ctrEmpDependantTypeId.value==0)
    //        {if(ctrEmpDependantTypeId.value==0 && ctrBirthDate.getValue()== undefined && ctrBirthCity.value==0 && ctrSex.value=="M" && ctrInsuranceCovered.value==0)
    //         return false
    //        }
    if (blnnodependentChange == false) {
        blnnodependentChange = true;
        return false;
    }
    else
        if (CompareVal(ctrId.value, IntID) == true || CompareVal(ctrEngName.value, StrEngName) == true || CompareVal(ctrArbName.value, StrArbName) == true || CompareVal(ctrNationalityId.value, IntNationalityId) == true || CompareVal(ctrEmpDependantTypeId.value, IntEmpDependantTypeId) == true || CompareVal(ctrBirthDate.getValue(), dteBirthDate) == true || CompareVal(ctrBirthCity.value, IntBirthCity) == true || CompareVal(ctrSex.value, IntSex) == true || CompareVal(ctrInsuranceCovered.value, IntInsuranceCovered) == true)
        return true;
    else
        return false;
}



var blDoSubmit = 0;






function Do_Submit_Employee() {
    if (blDoSubmit == 0) {
        return true;
    }
    if (blDoSubmit == 1) {
        return false;
    }
}


function DdlInsuranceCovered_Old_OnBlur() {

    //          var webTab = igtab_getTabById("UltraWebTab1");
    //          if(webTab == null)
    //             return;
    //          var grid               = igtab_getElementById("uwgDependents", webTab.element);
    //              grid               = igtbl_getGridById(grid.id);
    //          var txtEngName         = webTab.findControl("txtEngName");
    //          var nextEditCell;
    //    
    //          if (grid.Rows.length == 0)
    //             txtEngName.foucus();
    //          else {
    //             nextEditCell = igtbl_getCellById(grid.Id+"_rc_0_0");
    //             nextEditCell.activate();
    //             nextEditCell.beginEdit();
    //          }

}

function UwgDependants_AfterRowActivateHandler(gridName, rowId) {

}

function uwgDependents_KeyDownHandler(gridName, cellId, key) {
    //  if(key==9 || key ==13)
    //    {
    //    var cell           = igtbl_getCellById(cellId);
    //	var nextindex      = cell.Row.getIndex()+1;
    //    var NextRow        =  igtbl_getRowById(gridName+"_r_"+nextindex);
    //    if (NextRow == null)
    //    {
    //        nextindex = 0
    //        NextRow=  igtbl_getRowById(gridName+"_r_"+nextindex);
    //    }
    //    NextRow.setSelected(true);
    //	return true;
    //    }
}

function DisplayImageScreen(IntObjectId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtEmpDependantId");
    if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
        window.open("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
}

function DisplayImageScreenEmp(IntObjectId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var ctrId = window.document.getElementById("txtEmpId");

    if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
        window.open("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
    //window.showModalDialog("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value ,"_blank" ,"height=800,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
}


function Employees_Dep_Button1_Click(StrTableName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var ctrPermission = window.document.getElementById("txtDocumentsPermission");
    if (ctrPermission.value == "1") {
        alert(" Sorry! Access is denied, You have no permission to open this Screen. ");
        return 0;
    }
    var ctrEmployeeId = window.document.getElementById("txtEmpId");
    var ctrEmployeeCode = igtab_getElementById("txtCode1", webTab.element);
    var ctrEmployeeName = igtab_getElementById("txtEngFirstName", webTab.element);


    if (ctrEmployeeCode.value != null && ctrEmployeeCode.value != "" && ctrEmployeeCode.value != "0") {
        window.open("frmEmployeesDocuments.aspx?TB=" + StrTableName + "&SV=" + ctrEmployeeId.value + "&PC=" + ctrEmployeeCode.value + "&PN=" + ctrEmployeeName.value, "_Doc", "height=550,width=787,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");

    }
}

function Employees_Dep_Button2_Click(StrTableName) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrPermission = window.document.getElementById("txtDocumentsPermission");
    if (ctrPermission.value == "1") {
        alert(" Sorry! Access is denied, You have no permission to open this Screen. ");
        return 0;
    }
    var ctrEmployeeId = window.document.getElementById("txtEmpId");
    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = igtab_getElementById("txtEngName", webTab.element);
    var ctrArbName = igtab_getElementById("txtArbName", webTab.element);
    var ctrEmployeeCode = igtab_getElementById("txtCode1", webTab.element);

    if (ctrEmployeeCode.value != null && ctrEmployeeCode.value != "" && ctrEmployeeCode.value != "0") {
        if (ctrEngName.value != "" || ctrArbName.value != "") {
            if (ctrEngName.value == "")
                var Name = ctrArbName.value;
            else
                var Name = ctrEngName.value;
            window.open("frmEmployeesDocuments.aspx?TB=" + StrTableName + "&SV=" + ctrId.value + "&PC=Dependant&PN=" + Name, "_blank", "height=550,width=787,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        }
        else
            alert(" No dependant data is present yet! Please select a dependant or type in dependant data first! ")
    }
}


function txtStartDate_ValueChanged(oDateChooser, dropDownPanel, oEvent) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var EndDate = igtab_getElementById("txtEndDate", webTab.element);
    EndDate = igdrp_getComboById(EndDate.id);
    var contractId = igtab_getElementById("txtContractId", webTab.element).value;
    var StartDate = oDateChooser.getValue();
    //-------------------------------0257 MODIFIED-----------------------------------------
    //if (StartDate.getValue()>=EndDate)
    //-------------------------------=============-----------------------------------------
    if (StartDate >= EndDate.getValue()) {
        //        alert("Invalid Start Date");
        oDateChooser.setValue('');
        oDateChooser.focus
        return;
    }

    var empId = window.document.getElementById("txtEmpId").value;
    var strempcontractPeriod = oDateChooser.getText() + "_" + EndDate.getText() + "_" + contractId + "_" + empId;
    if (strempcontractPeriod == undefined || oDateChooser.getText() == "") {
        oDateChooser.setValue('');
        return;
    }
    else
        PageMethods.CheckValidContractExist(strempcontractPeriod + "_S", callback_GetValidContractPeriod, OnSucceeded, OnFailed)

}



function callback_GetValidContractPeriod(res) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var EndDate = igtab_getElementById("txtEndDate", webTab.element);
    EndDate = igdrp_getComboById(EndDate.id);
    var StartDate = igtab_getElementById("txtStartDate", webTab.element);
    StartDate = igdrp_getComboById(StartDate.id);
    if (res == "1") { //if(res.value=="1") 
        alert('A valid contract in this period is already exist');
        StartDate.setValue('');
        EndDate.setValue('');
        StartDate.focus();
    }
    else if (res == "2") {
        EndDate.setValue(StartDate.getValue())
    }
    else if (res == "3") {
        alert('A valid contract in this period is already exist');
        EndDate.setValue('');
        EndDate.focus();
    }

}


function txtEndDate_ValueChanged(oDateChooser, newValue, oEvent) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var StartDate = igtab_getElementById("txtStartDate", webTab.element);
    StartDate = igdrp_getComboById(StartDate.id);

    var EndDate = oDateChooser.getValue()
    if (StartDate.getValue() > EndDate) {
        //        alert("Invalid End Date")
        oDateChooser.setValue('')
        oDateChooser.focus
        return;
        //EndDate.setValue('')
    }
    var contractId = igtab_getElementById("txtContractId", webTab.element).value;
    var empId = window.document.getElementById("txtEmpId").value;
    var strempcontractPeriod = StartDate.getText() + "_" + oDateChooser.getText() + "_" + contractId + "_" + empId;
    if (strempcontractPeriod == undefined || StartDate.getText() == "") {
        StartDate.setValue('');
        return;
    }
    else
        PageMethods.CheckValidContractExist(strempcontractPeriod + "_E", callback_GetValidContractPeriod, OnSucceeded, OnFailed)
}










////////////////////////////////////////
// Developer    : [260]
// Date Created : 02-06-2008
////////////////////////////////////////
function CompareVal(firstVal, secondVal) {
    if (firstVal == null || firstVal == undefined)
        firstVal = ""
    if (secondVal == null || secondVal == undefined)
        secondVal = ""
    if (firstVal != secondVal)
        return true;
    else
        return false;

}

var NewMode = false;

function Emp_Dependants_btnNew() {
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgDependents");
    if (grid.Rows.length >= 0)
        var ActiveRow = grid.getActiveRow();

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = window.document.all.item("UltraWebTab1$_ctl0$txtEngName")
    var ctrArbName = window.document.all.item("UltraWebTab1$_ctl0$txtArbName")
    var ctrEmpDependantTypeId = window.document.all.item("UltraWebTab1$_ctl0$DdlDependantTypeID")
    var ctrNationalityId = webTab.findControl("DdlNationalityId");
    var ctrBirthDate = igdrp_getComboById("UltraWebTab1__ctl0_txtBirthDate");
    var ctrBirthCity = window.document.all.item("UltraWebTab1$_ctl0$DdlBirthCity2")
    var ctrSex = window.document.all.item("UltraWebTab1$_ctl0$DdlSex")
    var ctrInsuranceCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlInsuranceCovered")
    var ctrImage = window.document.all.item("UltraWebTab1__ctl0_ImgDependantImage")
    var dependentgrid = window.document.all.item("UltraWebTab1xxctl0xuwgDependents")
    var ctrPercentageInsurance = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageInsurance")
    var ctrTicketCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlTicketCovered")
    var ctrPercentageTicket = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageTicket")
    var ctrGrid = igtbl_getGridById(dependentgrid.id); 


    NewMode = true;

    ctrId.value = 0;
    ctrEngName.value = "";
    ctrArbName.value = "";
    ctrNationalityId.value = 0;
    ctrBirthDate.setValue("");
    ctrBirthCity.value = 0;
    ctrSex.value = "M";
    ctrInsuranceCovered.value = 0;
    ctrImage.ImageUrl = "./DefaultDepndant.jpg"
    ctrPercentageInsurance.value = 0;
    ctrTicketCovered.value = 0;
    ctrPercentageTicket.value = 0;
    NewMode = false;
    blnnodependentChange = false;
    //SetEmpDocDepVisibilty()
}


function Emp_Dependants_btnDelete() {
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgDependents");
    if (grid.Rows.length >= 0)
        var ActiveRow = grid.getActiveRow();

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;


    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = window.document.all.item("UltraWebTab1$_ctl0$txtEngName")
    var ctrArbName = window.document.all.item("UltraWebTab1$_ctl0$txtArbName")
    var ctrEmpDependantTypeId = window.document.all.item("UltraWebTab1$_ctl0$DdlDependantTypeID")
    var ctrNationalityId = webTab.findControl("DdlNationalityId");
    var ctrBirthDate = igdrp_getComboById("UltraWebTab1__ctl0_txtBirthDate");
    var ctrBirthCity = window.document.all.item("UltraWebTab1$_ctl0$DdlBirthCity2")
    var ctrSex = window.document.all.item("UltraWebTab1$_ctl0$DdlSex")
    var ctrInsuranceCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlInsuranceCovered")
    var ctrImage = window.document.all.item("UltraWebTab1__ctl0_ImgDependantImage")
    var dependentgrid = window.document.all.item("UltraWebTab1xxctl0xuwgDependents")
    var ctrPercentageInsurance = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageInsurance")
    var ctrTicketCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlTicketCovered")
    var ctrPercentageTicket = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageTicket")
    var ctrGrid = igtbl_getGridById(dependentgrid.id); 


    NewMode = true;

    if (ActiveRow != null) {
        if (ActiveRow.getSelected() == true) {
            var txtDependentDeletedID = window.document.getElementById("txtDependentDeletedID");
            var StrID = new String()
            StrID = txtDependentDeletedID.value;
            if (ActiveRow.getCellFromKey("ID").getValue() > 0) {
                StrID = StrID + "_" + ActiveRow.getCellFromKey("ID").getValue();
            }
            txtDependentDeletedID.value = StrID;
            ActiveRow.deleteRow();
            ActiveRow.remove();
        }

        ctrId.value = 0;
        ctrEngName.value = "";
        ctrArbName.value = "";
        ctrNationalityId.value = 0;
        ctrBirthDate.setValue("");
        ctrBirthCity.value = 0;
        ctrSex.value = "M";
        ctrInsuranceCovered.value = 0;
        ctrImage.ImageUrl = "./DefaultDepndant.jpg"
        ctrPercentageInsurance.value = 0;
        ctrTicketCovered.value = 0;
        ctrPercentageTicket.value = 0;
        NewMode = false;
        blnnodependentChange = false;
        //SetEmpDocDepVisibilty()
    }
    else {
        alert("There's not any Dependant to delete !")
    }
}

function Emp_Dependants_btnSave() {
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgDependents");
    if (grid.Rows.length >= 0)
        var ActiveRow = grid.getActiveRow();

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var ctrId = window.document.getElementById("txtEmpDependantId");
    var ctrEngName = window.document.all.item("UltraWebTab1$_ctl0$txtEngName")
    var ctrArbName = window.document.all.item("UltraWebTab1$_ctl0$txtArbName")
    var ctrEmpDependantTypeId = window.document.all.item("UltraWebTab1$_ctl0$DdlDependantTypeID")
    var ctrNationalityId = webTab.findControl("DdlNationalityId");
    var ctrBirthDate = igdrp_getComboById("UltraWebTab1__ctl0_txtBirthDate");
    var ctrBirthCity = window.document.all.item("UltraWebTab1$_ctl0$DdlBirthCity2")
    var ctrSex = window.document.all.item("UltraWebTab1$_ctl0$DdlSex")
    var ctrInsuranceCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlInsuranceCovered")
    var ctrImage = window.document.all.item("UltraWebTab1__ctl0_ImgDependantImage")
    var dependentgrid = window.document.all.item("UltraWebTab1xxctl0xuwgDependents")
    var ctrPercentageInsurance = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageInsurance")
    var ctrTicketCovered = window.document.all.item("UltraWebTab1$_ctl0$DdlTicketCovered")
    var ctrPercentageTicket = window.document.all.item("igtxtUltraWebTab1__ctl0_txtPercentageTicket")
    var ctrGrid = igtbl_getGridById(dependentgrid.id); 

    NewMode = true;


    if (ctrEngName.value != "" || ctrArbName.value != "") {
        var currentrow
        var intRowId = CheckDuplicateDependantName()
        if (intRowId == -1) {
            currentrow = igtbl_addNew(ctrGrid.Id, 0, true, true)
        }
        else {
            currentrow = igtbl_getRowById(intRowId);
        }


        var engnamecell = currentrow.cells[1];
        var arbnamecell = currentrow.cells[2];
        var dependenttypeCell = currentrow.cells[3];
        var birthdateCell = currentrow.cells[4];
        var nationalityCell = currentrow.cells[5];
        var birthcityCell = currentrow.cells[6];
        var sexCell = currentrow.cells[7];
        var insuranceCell = currentrow.cells[8];
        var IntPercentageInsuranceCell = currentrow.cells[15];
        var IntTicketCoveredCell = currentrow.cells[16];
        var IntPercentageTicketCell = currentrow.cells[17];

        IntPercentageInsuranceCell.setValue(ctrPercentageInsurance.value);
        IntTicketCoveredCell.setValue(ctrTicketCovered.value);
        IntPercentageTicketCell.setValue(ctrPercentageTicket.value);

        engnamecell.setValue(ctrEngName.value);
        arbnamecell.setValue(ctrArbName.value);
        dependenttypeCell.setValue(ctrEmpDependantTypeId.value);
        birthdateCell.setValue(ctrBirthDate.getValue());
        nationalityCell.setValue(ctrNationalityId.value);
        birthcityCell.setValue(ctrBirthCity.value);
        sexCell.setValue(ctrSex.value);
        insuranceCell.setValue(ctrInsuranceCovered.value);
    }


    ctrId.value = 0;
    ctrEngName.value = "";
    ctrArbName.value = "";
    ctrNationalityId.value = 0;
    ctrBirthDate.setValue("");
    ctrBirthCity.value = 0;
    ctrSex.value = "M";
    ctrInsuranceCovered.value = 0;
    ctrImage.ImageUrl = "./DefaultDepndant.jpg"
    ctrPercentageInsurance.value = 0;
    ctrTicketCovered.value = 0;
    ctrPercentageTicket.value = 0;
    NewMode = false;
    blnnodependentChange = false;
    //SetEmpDocDepVisibilty()
}


function frmEmployeesLoad() {
    SetEmpDocDepVisibilty()
    //ddlDepartment_Change()
}

function SetEmpDocDepVisibilty() {
    try {
        var webTab = igtab_getTabById("UltraWebTab1");
        if (webTab == null)
            return;
        var btnDependantsDocuments = ig_getWebControlById(igtab_getElementById("btnDependantsDocuments", webTab.element).id);
        btnDependantsDocuments.setVisible(false);
    }
    catch (ex) {
    }
}




function CheckDepndantCode() {
    var rowId = CheckDuplicateDependantName()
    if (rowId != -1) {
        var row = igtbl_getRowById(rowId);
        row.activate();
        row.select();
        if (row.getIndex() == 0)
            uwgDepandants_AfterSelectChangeHandler("UltraWebTab1xxctl0xuwgDependents", row.Id)
        return;
    }
}


function CheckDuplicateDependantName() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    
    var txtEngName = window.document.all.item("UltraWebTab1__ctl0_txtEngName");
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgDependents");

    for (i = 0; i < grid.Rows.length; i++) {
        var currRow = grid.Rows.rows[i];
        try {
            currCodeCell = currRow.cells[1];
            if (currCodeCell == undefined || currCodeCell == null) {
                currCodeCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgDependents_rc_" + i + "_1");
            }
        }
        catch (e) {
            currCodeCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgDependents_rc_" + i + "_1");
            if (currCodeCell == null) {
                continue;
            }
        }

        var strEngName = new String()
        var strCellName = new String()

        strEngName = txtEngName.value
        strCellName = currCodeCell.getValue()

        strEngName = strEngName.toLowerCase().replace(" ", "")
        strCellName = strCellName.toLowerCase().replace(" ", "")

        if (strEngName == strCellName) {
            var row = igtbl_getRowById(currCodeCell.Id);

            return row.Id;
        }

    }
    return -1
}






//--------------Add By [0261]
//---------------------------------------------------------------------------
//function DdlCurrency_OnBlur() {
//    var grid = igtbl_getGridById(uwgVacationDetailsID);
//    var nextEditCell;
//    if (grid.Rows.rows.length == 0)
//        nextEditCell = igtbl_getCellById(uwgVacationDetailsID + "_anc_2");
//    else
//        nextEditCell = igtbl_getCellById(uwgVacationDetailsID + "_rc_0_2");
//    if (nextEditCell != null) {
//        nextEditCell.activate();
//        nextEditCell.beginEdit();
//    }
//}
//---------------------------------------------------------------------------
function GetSecondTab(inttabIndex) {
    var ultraTab = igtab_getTabById("UltraWebTab1");
    ultraTab.setSelectedIndex(inttabIndex);
}
//---------------------------------------------------------------------------
function GOSIExcludeDate_OnBlur(oDateChooser, dummy, oEvent) {
    var ultraTab = igtab_getTabById("UltraWebTab1");
    var DdlProfessions = igtab_getElementById("DdlProfessions", ultraTab.element);
    var txtCode1 = igtab_getElementById("txtCode1", ultraTab.element);

    if (txtCode1.value != "" && txtCode1.value != null) {
        GetSecondTab(1);
        DdlProfessions.focus()
    }
    else {
        txtCode1.focus();
    }
}
//---------------------------------------------------------------------------
function DdlInsuranceCovered_OnBlur() {
    //    var ultraTab             = igtab_getTabById("UltraWebTab1");
    //    var txtCode1 = igtab_getElementById("txtCode1",ultraTab.element);  
    //    GetSecondTab(0)  ;
    //    txtCode1.focus();
    var ultraTab = igtab_getTabById("UltraWebTab1");
    var txtEngName = igtab_getElementById("txtEngName", ultraTab.element);
    txtEngName.focus();
}
//---------------------------------------------------------------------------
function DdlNationalityId_OnBlur() {
    var ultraTab = igtab_getTabById("UltraWebTab1");
    var ctrBirthDate = igtab_getElementById("txtBirthDate ", ultraTab.element);
    ctrBirthDate = igdrp_getComboById(ctrBirthDate.id);
    ctrBirthDate.focus();
}
//---------------------------------------------------------------------------
function txtBirthDate_OnBlur() {
    var ultraTab = igtab_getTabById("UltraWebTab1");
    var DdlBirthCity2 = igtab_getElementById("DdlBirthCity2", ultraTab.element);
    DdlBirthCity2.focus();
}
//---------------------------------------------------------------------------
//*******************************************************************************
//***************************************************************************
var bEnter = true;
var bSEnter = true
//***************************************************************************

function uwgContractVacations_BeforeCellChangeHandler(gridName, cellId) {
    if (!bEnter)
        return
    var cell = igtbl_getCellById(cellId);
    var Row = igtbl_getRowById(cellId);
    var grid = igtbl_getGridById(gridName);
    var ReqExpCell;
    var ReqExpCellValue;
    var DurationCell;
    var DurationCellValue;

    if (Row.getPrevRow() != null) {
        ReqExpCell = Row.getPrevRow().getCellFromKey("RequiredWorkingMonths");
        ReqExpCellValue = Row.getPrevRow().getCellFromKey("RequiredWorkingMonths").getValue();
        DurationCell = Row.getPrevRow().getCellFromKey("DurationDays")
        DurationCellValue = Row.getPrevRow().getCellFromKey("DurationDays").getValue();
    }

    if (ReqExpCellValue == null)
        ReqExpCellValue = 0;

    if (DurationCellValue == null)
        DurationCellValue = 0;

    //-----------Index 1 
    if (cell.Column.Index == 1) {
        if (ReqExpCellValue > 0 && DurationCellValue == 0) {
            bEnter = false
            Row.getPrevRow().activate;
            Row.getPrevRow().select;
            DurationCell.activate();
            DurationCell.select;
            DurationCell.beginEdit();
            bEnter = true
            return 1;
        }
        else {
            bEnter = false
            Row.activate;
            Row.select;
            Row.getCellFromKey("RequiredWorkingMonths").activate();
            Row.getCellFromKey("RequiredWorkingMonths").select;
            Row.getCellFromKey("RequiredWorkingMonths").beginEdit();
            bEnter = true
            return 1;
        }
    }
}


function uwgContractVacations_BeforeExitEditModeHandler(gridName, cellId, newValue) {
    //         if (!bSEnter)
    //            return
    //        var cell              = igtbl_getCellById(cellId);
    //        var Row               = igtbl_getRowById(cellId);
    //        var grid              = igtbl_getGridById(gridName);
    //        var strLastRowID  = grid.Rows.getLastRowId()
    //        var ReqExpCell        = Row.getCellFromKey("RequiredWorkingMonths");
    //        var ReqExpCellValue   = Row.getCellFromKey("RequiredWorkingMonths").getValue() ;
    //        var DurationCell      = Row.getCellFromKey("DurationDays")
    //        var DurationCellValue = Row.getCellFromKey("DurationDays").getValue(); 

    //        if(ReqExpCellValue == null)
    //           ReqExpCellValue =  0 ;
    //                
    //        if(DurationCellValue == null)
    //           DurationCellValue =  0 ;
    //        
    //    if(Row.Id == strLastRowID)
    //       if(cell.Column.Index == 3)
    //          if(ReqExpCellValue > 0 && DurationCellValue == 0)
    //            {
    //             bSEnter = false
    //             Row.activate;
    //             Row.select ;
    //             DurationCell.activate();
    //             DurationCell.select;
    //             DurationCell.beginEdit();
    //             bSEnter = true
    //             return  0 ;      
    //            }



}

//*******************************************************************************
var ddlBranch;
var ddlSectors;
var strBranches;
var strSectors;

function ddlDepartment_Change() {
    var ultraTab = igtab_getTabById("UltraWebTab1");
    var ddlDepartment = igtab_getElementById("ddlDepartment", ultraTab.element)
    ddlBranch = igcmbo_getComboById(igtab_getElementById("ddlBranch", ultraTab.element).id)
    ddlBranch.inputBox.value = "";
    ddlBranch.selectedIndex = 0;

    ddlSectors = igcmbo_getComboById(igtab_getElementById("ddlSectors", ultraTab.element).id)
    ddlSectors.inputBox.value = "";
    ddlSectors.selectedIndex = 0;

    PageMethods.GetRelatedDepartment(ddlDepartment.value, OnSucceeded, OnFailed);
}

//=================  Vacation Script =======================
var cEnter = 13;
var cTab = 9;
var cF9 = 120;
var cUP = 38;
var cDown = 40;
var cDelete = 46;

var intNoOfEmptyRows = 3;
var firstColumnKey = "FromMonth"

var bUpdate = true;
var currActiveCell = null
var currGridName;
var blnNotActivateThisCell = false


function CreateEmptyRows(gridName, cellId) {
    var count = igtbl_getGridById(gridName).Rows.length - 1;
    var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

    if (rowIndex == count) {

        igtbl_addNew(gridName, 0, true, false);
        igtbl_addNew(gridName, 0, true, false);
    }
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
    bUpdate = false
    currCell.activate()
    currCell.select()
    currCell.beginEdit()
    bUpdate = true
}

function getCorrectCell(gridName) {
    var grid = igtbl_getGridById(gridName)
    for (i = 0; i < grid.Rows.length ; i++) {
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
        if (currActiveCell != undefined)
            ActivateThisCell(currActiveCell);
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
        CreateEmptyRows(gridName, cellId)
        ActivateThisCell(Cell)
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
        } //End For
    }
    else if (Cell.Column.Key == "ToMonth") {
        for (i = 0; i < grid.Rows.length; i++) {
            var currRow = grid.Rows.rows[i]
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
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    var grid = gtab_getElementById("uwgVacationDetails", webTab.element).id;
    var activeRowsCount = 0;

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
    //CreateEmptyRows(GridCtrl)
    currActiveCell = getCorrectCell(GridCtrl)
    ActivateThisCell(currActiveCell);
}




//function OnSucceeded(result, userContext, methodName) {
//    if (methodName == 'GetRelatedDepartment') {
//        strBranches = result;

//        for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
//            var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j);
//            currRow.setHidden(false);
//            currRow.getCellFromKey("V").setValue("");
//        }
//        if (strBranches != null && strBranches != "") {
//            var arrBranches = strBranches.split(',');
//            for (i = 0; i < arrBranches.length; i++) {
//                for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
//                    var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j);
//                    if (ConvertToNumber(arrBranches[i]) == currRow.getCellFromKey("ID").getValue()) {
//                        currRow.setHidden(false);
//                        currRow.getCellFromKey("V").setValue("T");
//                    }
//                    else {
//                        if (currRow.getCellFromKey("V").getValue() != "T" && currRow.getCellFromKey("ID").getValue() != 0)
//                            currRow.setHidden(true);
//                    }
//                } //End for (j = 0; j < ddlBranch.length ...
//            } //End for (i = 0; i < arrBranches.length ...     
//        }
//        else {
//            for (j = 0; j < ddlBranch.grid.Rows.length; j++) {
//                var currRow = igtbl_getRowById(ddlBranch.grid.Id + "_r_" + j)
//                if (currRow.getCellFromKey("ID").getValue() != 0)
//                    currRow.setHidden(true);
//            }
//        }
//    }
//}

//function OnFailed(error) {
//    alert(error.get_message());
//}
