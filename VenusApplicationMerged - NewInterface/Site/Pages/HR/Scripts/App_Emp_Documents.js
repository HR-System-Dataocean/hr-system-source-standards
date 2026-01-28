
function setlink() {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var link = webTab.findControl("lnkDownload")
    var ddl = webTab.findControl("DdlAttachedFiles")
    //var ddlvalue    = ddl.text 
    var IntIndex = ddl.selectedIndex;
    var selected_text = ddl.options[IntIndex].text;

    // var ddlurl      = GetValue(ddlvalue ,'Name');
    link.href = "../../Uploads" + "/" + selected_text;
}


function UwgEmployeeDocuments_AfterRowActivateHandler(gridName, rowId) {
    var Grid = igtbl_getGridById(gridName);
    var row = igtbl_getRowById(rowId)
    if (row.getCellFromKey("ID").getValue() != null && row.getCellFromKey("ID").getValue() > 0) {
        PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermissionEmployeeDocuments, OnSucceeded, OnFailed)
        PageMethods.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfoEmployeeDocuments, OnSucceeded, OnFailed)
    }
}
//Modification [0260] 21-10-2008 [Start]
function txtDocumentNumberChange(gridName, rowId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var DocObject = window.document.getElementById("txtObjectID").value;
    var DocRecord = window.document.getElementById("txtRecordID").value;
    var DocNumber = igedit_getById("UltraWebTab1__ctl0_txtDocumentNumber").text;
    var DocID = PageMethods.GetDocID(DocNumber + "_" + DocObject + "_" + DocRecord, callback_GetEmployeeDocumentID, OnSucceeded, OnFailed);
}
function callback_GetEmployeeDocumentID(res) {
    if (res != null && res > 0) {
        PageMethods.GetRecordPermissionAjax(res, callback_GetRecordPermissionEmployeeDocuments, OnSucceeded, OnFailed)
        PageMethods.GetRecordInfoAjax(res, call_backRecordInfoEmployeeDocuments, OnSucceeded, OnFailed)
    }
}
//  Modification                 [End]
function call_backRecordInfoEmployeeDocuments(res) {
    var tlbMain = igtbar_getToolbarById("TlbMainNavigation")

    var reguserItem = tlbMain.Items.fromKey("RegUserVal")
    var regdateItem = tlbMain.Items.fromKey("RegDateVal")
    var canceldateItem = tlbMain.Items.fromKey("CancelDateVal")
    if (res == null) return;
    var arr = res.split(",")

    reguserItem.Element.innerText = arr[0]
    regdateItem.Element.innerText = arr[1]
    canceldateItem.Element.innerText = arr[2]

    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")
    var delItem = tlbMain.Items.fromKey("Delete")
    if (arr[2] != "") {
        delItem.setEnabled(false)
    }
}

function callback_GetRecordPermissionEmployeeDocuments(res) {
    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")

    var saveItem = tlbMain.Items.fromKey("Save")
    var delItem = tlbMain.Items.fromKey("Delete")
    var printItem = tlbMain.Items.fromKey("Print")

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

    if (res == null) return;
    var arr = res.split(",");
    if (arr[0] != "1")
        saveItem.setEnabled(false);
    if (arr[1] != "1")
        delItem.setEnabled(false);
    if (arr[2] != "1")
        printItem.setEnabled(false);

}

// 4-

//===============================================================================================
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Vlaidate on last renewal date less than expiry date 
//    'Developer      :  [0258]
//    'Date Created   :  26-09-2007
//    'Screen         :  frmDcumentDetails
//    'Modifacations  :  [0261] cHANGE THE WAY REFERING TO CINTROLS INTO SCREEN AFETR ADDING ULTRATAB CONTROL 
function frmDocumentDetailstxtExpiryDate_ValueChanged(oDateChooser, newValue, oEvent) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    //-----------------------------
    var temptxtExpiryDate = igtab_getElementById("txtExpiryDate", webTab.element);
    var expiryDate = igdrp_getComboById(temptxtExpiryDate.id);
    //-----------------------------
    var temptxtLastRenewalDate = igtab_getElementById("txtLastRenewalDate", webTab.element);
    var lastRenewalDate = igdrp_getComboById(temptxtLastRenewalDate.id);
    //-----------------------------
    if (lastRenewalDate.getValue() != null) {
        if (lastRenewalDate.getValue() > expiryDate.getValue()) {
            lastRenewalDate.setValue(expiryDate.getValue())
        }
    }

}
//
function frmDocumentsDetailstxtLastRenewalDate_ValueChanged(oDateChooser, newValue, oEvent) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    //-----------------------------
    var temptxtExpiryDate = igtab_getElementById("txtExpiryDate", webTab.element);
    var expiryDate = igdrp_getComboById(temptxtExpiryDate.id);
    //-----------------------------
    var temptxtLastRenewalDate = igtab_getElementById("txtLastRenewalDate", webTab.element);
    var lastRenewalDate = igdrp_getComboById(temptxtLastRenewalDate.id);
    //-----------------------------
    if (expiryDate.getValue() != null) {
        if (expiryDate.getValue() < lastRenewalDate.getValue()) {
            expiryDate.setValue(lastRenewalDate.getValue())
        }
    }
}
//-------------------------------=============-----------------------------------------



//    'Modifacations  :  [0261] cHANGE THE WAY REFERING TO CINTROLS INTO SCREEN AFETR ADDING ULTRATAB CONTROL 
function UwgEmployeeDocuments_BeforeRowActivateHandler(gridName, rowId) {
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


//===============================================================================================
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  
//    'Developer      :  [0261]
//    'Date Created   :  22-05-2008
//    'Screen         :  frmDcumentDetails
//    'Modifacations  :  
//===============================================================================================
function uwgEmployeeDocuments_AfterSelectChangeHandler(gridName, id) {
    //------//Get Screen Controls--------------------------------------------------------------------------
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;

    //-----------------------------
    var txtID = igtab_getElementById("txtID", webTab.element);
    var txtDocumentID = igtab_getElementById("txtDocumentID", webTab.element);
    var txtEmpID = igtab_getElementById("txtEmpID", webTab.element);
    //-----------------------------
    var ddlDocumentType = igtab_getElementById("ddlDocumentType", webTab.element);
    var txtDocumentNumber = igtab_getElementById("txtDocumentNumber", webTab.element);
    var DdlIssuedCity = igtab_getElementById("DdlIssuedCity", webTab.element);
    //-----------txtIssueDate------------------
    var temptxtIssueDate = igtab_getElementById("txtIssueDate", webTab.element);
    var strCtrName_0 = temptxtIssueDate.id;
    var txtIssueDate = igdrp_getComboById(strCtrName_0);
    //-----------txtExpiryDate------------------
    var temptxtExpiryDate = igtab_getElementById("txtExpiryDate", webTab.element);
    var strCtrName_1 = temptxtExpiryDate.id;
    var txtExpiryDate = igdrp_getComboById(strCtrName_1);
    //-----------txtLastRenewalDate------------------
    var temptxtLastRenewalDate = igtab_getElementById("txtLastRenewalDate", webTab.element);
    var strCtrName_2 = temptxtLastRenewalDate.id;
    var txtLastRenewalDate = igdrp_getComboById(strCtrName_2);
    //-----------------------------
    var txtDocumentEngName = igtab_getElementById("txtDocumentEngName", webTab.element);
    var txtDocumentArbName = igtab_getElementById("txtDocumentArbName", webTab.element);
    var txtAttachedFile = igtab_getElementById("txtAttachedFile", webTab.element);
    var DdlAttachedFiles = igtab_getElementById("DdlAttachedFiles", webTab.element);
    //------//Get Row Data--------------------------------------------------------------------------------       

    var Grid = igtbl_getGridById(gridName);
    var Row = igtbl_getRowById(id)
    //--------------------------------------   
    var intID = Row.getCell(0).getValue();
    var intDocumentID = Row.getCell(1).getValue();
    var intObjectID = Row.getCell(2).getValue();
    var intRecordID = Row.getCell(3).getValue();
    var DocumentNumber = 0;
    if (Row.getCell(4).getValue() != null)
        DocumentNumber = Row.getCell(4).getValue()
    var IssueDate = Row.getCell(5).Element.innerText
    var intIssuedCityID = Row.getCell(6).getValue();
    var LastRenewalDate = Row.getCell(7).Element.innerText
    var ExpiryDate = Row.getCell(8).Element.innerText
    //-------//Bind Data To Controls-----------------------------------------------------------------------  

    //-----------------------------
    txtID.value = intID;
    txtID.innerText = intID;
    txtDocumentID.value = intDocumentID;
    txtDocumentID.innerText = intDocumentID;
    //-----------------------------
    ddlDocumentType.value = intDocumentID;
    ddlDocumentType.enabled = false;
    ddlDocumentType.disabled = true;

    txtDocumentNumber.value = DocumentNumber;
    DdlIssuedCity.value = intIssuedCityID;
    //-----------------------------
    if (IssueDate != null) {
        var strValue_0 = IssueDate.split(" ");
        txtIssueDate.setValue(strValue_0[0]);
    }
    else {
        txtIssueDate.setValue("");
    }

    //-----------------------------
    if (LastRenewalDate != null) {
        var strValue_1 = LastRenewalDate.split(" ");

        txtExpiryDate.setValue(strValue_1[0]);
    }
    else {
        txtExpiryDate.setValue("");
    }
    //-----------------------------
    if (ExpiryDate != null) {
        var strValue_2 = ExpiryDate.split(" ");
        txtLastRenewalDate.setValue(strValue_2[0]);
    }
    else {
        txtLastRenewalDate.setValue("");
    }
    //-----------------------------

    //--------------------------------------------------------------------------------------------------------   

}





function OnSucceeded(result, userContext, methodName) {

}
function OnFailed(error) {
    //alert(error.get_message());
}
