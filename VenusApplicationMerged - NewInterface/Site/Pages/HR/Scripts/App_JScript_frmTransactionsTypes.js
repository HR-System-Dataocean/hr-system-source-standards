
// JScript File

//================================= [End]

//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Open Formula Screen
//    Developer      :  [0261]
//    Date Created   :  05-05-2008
//    function name  :  Open_Formula_Screen()
//      
function Open_Formula_Screen(ControlId) {
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    control = webTab.findControl(ControlId);
    var queryString = "?ControlName=" + control.name + "&ControlValue=" + control.value + "&ControlType=T";
    window.open("frmFormulaDesigner.aspx" + queryString, "_blank", "height=320px,width=787px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");

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
//    ==================[End]

//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  helper function- to get the description of the formula.
//    Developer      :  [0260]
//    Date Created   :  20-04-2008
//    function name  :  getFormulaValue()
//      
//=========================== [Start] 
//Modification 
//Code       : B#001 23-04-2008 
//Developper : [0260]
//Description: call the ajax function with parameter Lang string if the language 
//             is English (Lang=Eng) or Arabic (Lang="Arb")
//
var ctrlFormulaDesc;
function getFormulaValue(formula) {
    var lang = GetCookie("Lang");
    if (lang.indexOf("ar") > -1) lang = "Arb"
    else lang = "Eng";

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    ctrlFormulaDesc = webTab.findControl("lblFormulaDesc");


    var res = '';
    if (formula != "")
        PageMethods.GetFormulaValue(formula, lang, OnSucceeded, OnFailed);

}

//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  get the description of the begin formula.
//    Developer      :  [0260]
//    Date Created   :  29-05-2008
//    function name  :  txtBeginFormula_TextChanged()
//      
//=========================== [Start] 
function txtBeginFormula_TextChanged() {

    //================================
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var ctrl = webTab.findControl("txtBeginFormula");
    var ctrlBeginFormulaDesc = webTab.findControl("lblBeginFormulaDesc");

    //================================


    if (ctrl.value != "")
        getBeginFormulaValue(ctrl.value);
    else {
        ctrlBeginFormulaDesc.style.border = "0px solid";
        ctrlBeginFormulaDesc.value = "";
        ctrlBeginFormulaDesc.innerText = "";
    }
}

//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  helper function- to get the description of the begin formula.
//    Developer      :  [0260]
//    Date Created   :  20-04-2008
//    function name  :  getBeginFormulaValue()
//      
//=========================== [Start]


function getBeginFormulaValue(formula) {
    var lang = GetCookie("Lang");
    if (lang.indexOf("ar") > -1) lang = "Arb"
    else lang = "Eng";

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    ctrlFormulaDesc = webTab.findControl("lblBeginFormulaDesc");


    var res = '';
    if (formula != "")
        res = PageMethods.GetFormulaValue(formula, lang, OnSucceeded, OnFailed);

}
//========================[End]


//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  get the description of the End formula.
//    Developer      :  [0260]
//    Date Created   :  29-05-2008
//    function name  :  txtEndFormula_TextChanged()
//      
//=========================== [Start] 
function txtEndFormula_TextChanged() {

    //================================
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    var ctrl = webTab.findControl("txtEndFormula");
    var ctrlEndFormulaDesc = webTab.findControl("lblEndFormulaDesc");

    //================================


    if (ctrl.value != "")
        getEndFormulaValue(ctrl.value);
    else {
        ctrlEndFormulaDesc.style.border = "0px solid";
        ctrlEndFormulaDesc.value = "";
        ctrlEndFormulaDesc.innerText = "";
    }
}

//    ========================================================================
//    Screen         :  frmTransactionTypes
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  helper function- to get the description of the End formula.
//    Developer      :  [0260]
//    Date Created   :  20-04-2008
//    function name  :  getEndFormulaValue()
//      
//=========================== [Start]


function getEndFormulaValue(formula) {
    var lang = GetCookie("Lang");
    if (lang.indexOf("ar") > -1) lang = "Arb"
    else lang = "Eng";

    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    ctrlFormulaDesc = webTab.findControl("lblEndFormulaDesc");


    var res = '';
    if (formula != "")
        PageMethods.GetFormulaValue(formula, lang, OnSucceeded, OnFailed);

}
//========================[End]




//    Screen         :  frmEndOfService.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Modified  :  24-04-2008
//    function name  :  uwgEndOfServiceRules_CellChangeHandler

var winopen;


function uwgEndOfServiceRules_CellChangeHandler(gridName, cellId) {
    var cell = igtbl_getCellById(cellId);

    if (window.event.keyCode == 9 && cell.Column.Index == 3) //& cellId.indexOf("anc")> -1 )
    {
        var controlvalue = cell.getValue();
        var queryString = "?ControlName=" + cellId + "&ControlValue=" + controlvalue + "&ControlType=G";
        winopen = window.open("frmFormulaDesigner.aspx" + queryString, "_Parent", "height=318px,width=780px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
    }

}


// to focus the Formula Screen
function uwgEndOfServiceRules_AfterEnterEditModeHandler(gridName, cellId) {
    if (winopen != null) {
        winopen.focus();
    }
}
//================================[End]




function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'GetFormulaValue') {
        res = result;
        //===========  B#001   [End]==============
        if (res != "") {
            ctrlFormulaDesc.style.border = "1px solid";
            ctrlFormulaDesc.style.borderColor = "White";
            ctrlFormulaDesc.value = res;
            ctrlFormulaDesc.innerText = res;
        }
        else {
            ctrlFormulaDesc.style.border = "0px solid";
            ctrlFormulaDesc.value = "";
            ctrlFormulaDesc.innerText = "";
        }
    }
    else if (methodName == '') {

    }
}
function OnFailed(error) {
    //alert(error.get_message());
}