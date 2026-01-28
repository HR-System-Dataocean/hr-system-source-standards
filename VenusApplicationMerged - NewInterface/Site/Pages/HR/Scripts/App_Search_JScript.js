// Search Functions ==================================================================================
//====================================================================================================
function MainSearch_btn_Click(oButton, oEvent) {
    MainSearch_Start()
}

function MainSearch_Start() {
    var Parameter = window.document.getElementById("name")
    var realParameter = window.document.getElementById("realname")
    var textAll = window.document.getElementById("txtSearchAll")
    var Value = Parameter.innerText
    var realValue = realParameter.innerText
    var Arr = Value.split("|");
    var realArr = realValue.split("|");
    var Final_Value = "";
    var FinalTest = "";
    for (i = 0; i < Arr.length; i++) {
        var str = Arr[i].substring(0, 3)
        switch (str) {
            case ("WV_"): 
                {
                    var Control = window.document.getElementById(Arr[i]);
                    if (textAll.value.length > 0) {
                        Final_Value += " or  IsNull(" + realArr[i] + ",'') like '%" + textAll.value + "%'";
                    }
                    else {
                        if (Control.value.length > 0) {
                            Final_Value += " And " + realArr[i] + " like '%" + Control.value + "%'";
                        }
                    }
                    break;
                }
            case ("WN_"): 
                {
                    var Control = window.document.getElementById(Arr[i]);
                    if (Control.value.length > 0)
                        if (realArr[i].indexOf("Select") > 0)
                            Final_Value += " And " + realArr[i] + "= '" + Control.value + "')";
                        else
                            Final_Value += " And " + realArr[i] + "=" + Control.value;
                    break;
                }
            case ("WD_"): 
                {
                    var Control = igdrp_getComboById(Arr[i])
                    if (Control.getValue() != undefined)
                        Final_Value += " And " + realArr[i] + "='" + Control.getText() + "'"
                    break;
                }
            case ("WB_"): 
                {
                    var Control = window.document.getElementById(Arr[i]);
                    if (Control.value.length > 0)
                        Final_Value += " And " + realArr[i] + "=" + Control.value;
                    break;
                }
        }
    }
    var Target_File = window.document.getElementById("value");
    Target_File.value = Final_Value.substring(5);
    window.document.form1.submit();
}

function MainSearch_OpenWin(Target, ModuleID) {
    var winopen = window.open("frmSearchScreen.aspx?TargetControl=" + Target + "&SearchID=" + ModuleID, "_Parent" + ModuleID, "height=525,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
    winopen.document.focus();
}
function MainSearch_DblClickHandler(gridName, cellId) {
    var row = igtbl_getRowById(cellId);
    var cell = row.getCell(0).getValue();
    var Target = document.getElementById("TargetControl");
    var TargetValue = Target.innerText;
    var cellControl = '';
    var OreginalName = TargetValue;
    if (TargetValue.substring(0, 3).toUpperCase() == "ULT") {
        OreginalName = TargetValue.substring(19)
    }


    if (OreginalName.substring(0, 3).toUpperCase() == "UWG") {
        window.opener.focus();
        cellControl = window.opener.igtbl_getCellById(TargetValue);
        cellControl.setValue(cell);
        cellControl.activate()
        cellControl.beginEdit()
        window.close();
        return;
    }
    else if (TargetValue.substring(0, 2).toUpperCase() == "UW") {
        cellControl = window.opener.igtbl_getCellById(TargetValue);
        cellControl.setValue(cell);
        window.opener.focus();
        window.close();
    }
    else if (TargetValue.substring(0, 4).toUpperCase() == "TAB1" && TargetValue.indexOf("wme") == -1) {
        var TabControl = window.opener.igtab_getTabById("UltraWebTab1");
        cellControl = window.opener.igtab_getElementById(TargetValue, TabControl.element);
        cellControl.innerText = cell;
        window.opener.focus();
        cellControl.focus();
        window.close();
    }
    else if (TargetValue.indexOf("Tab1wme") != -1) {
        cellControl = window.opener.igedit_getById("UltraWebTab1$_ctl1$" + TargetValue);
        if (cellControl == null)
            cellControl = window.opener.igedit_getById(TargetValue);
        cellControl.setValue(cell);
        window.opener.focus();
        cellControl.focus();
        window.close();
    }
    else if (TargetValue.indexOf("txtwme") != -1) {
        cellControl = window.opener.igedit_getById("UltraWebTab1__ctl0_" + TargetValue);
        cellControl.setValue(cell);
        window.opener.focus();
        window.opener.document.forms[0].submit();
        cellControl.focus();
        window.close();
    }
    else if (TargetValue.indexOf("txtNoSubmit") != -1) {
        var ctrl = window.opener.document.forms[0][TargetValue];
        if (ctrl == undefined) {

            var TabControl = window.opener.igtab_getTabById("UltraWebTab1");
            cellControl = window.opener.igtab_getElementById(TargetValue, TabControl.element);

            cellControl.innerText = cell;
            cellControl.focus();
            window.close();
            return;
        }
        window.opener.document.forms[0][TargetValue].value = cell;
        window.opener.focus();
        window.opener.document.forms[0][TargetValue].focus();
        window.close();
        return;
    }
    else

        var ctrl = window.opener.document.forms[0][TargetValue];
    if (ctrl == undefined) {
        var TabControl = window.opener.igtab_getTabById("UltraWebTab1");
        cellControl = window.opener.igtab_getElementById(TargetValue, TabControl.element);

        cellControl.innerText = cell;
        cellControl.focus();
        window.close();
        return;
    }

    window.opener.document.forms[0][TargetValue].value = cell;
    window.opener.focus();
    window.opener.document.forms[0][TargetValue].focus();
    window.close();
}


function MainSearch_KeyDownHandler(gridName, cellId, key) {
    if (key == 13) {
        var row = igtbl_getRowById(cellId);
        var cell = row.getCell(0).getValue();
        var Target = document.getElementById("TargetControl");
        var TargetValue = Target.innerText;
        var OreginalName = TargetValue;
        if (TargetValue.substring(0, 3).toUpperCase() == "ULT") {
            OreginalName = TargetValue.substring(19)
        }

        if (OreginalName.substring(0, 3).toUpperCase() == "UWG") {
            var cellControl = window.opener.igtbl_getCellById(TargetValue);
            window.opener.focus();
            cellControl.setValue(cell);
            cellControl.activate()
            cellControl.beginEdit()
            window.close();
        }
        else if (TargetValue.substring(0, 2).toUpperCase() == "UW") {
            var cellControl = window.opener.igtbl_getCellById(TargetValue);
            cellControl.setValue(cell);
            window.opener.focus();
            window.close();
        }
        else if (TargetValue.substring(0, 4).toUpperCase() == "TAB1" && TargetValue.indexOf("wme") == -1) {
            var TabControl = window.opener.igtab_getTabById("UltraWebTab1");
            var cellControl = window.opener.igtab_getElementById(TargetValue, TabControl.element);

            cellControl.innerText = cell;
            window.opener.focus();
            cellControl.focus();
            window.close();
        }
        else if (TargetValue.indexOf("Tab1wme") != -1) {
            var cellControl = window.opener.igedit_getById("UltraWebTab1$_ctl1$" + TargetValue);
            if (cellControl == null)
                cellControl = window.opener.igedit_getById(TargetValue);
            cellControl.setValue(cell);
            window.opener.focus();
            cellControl.focus();
            window.close();
        }
        else if (TargetValue.indexOf("txtwme") != -1) {
            cellControl = window.opener.igedit_getById("UltraWebTab1__ctl0_" + TargetValue);
            cellControl.setValue(cell);
            window.opener.focus();
            window.opener.document.forms[0].submit();
            cellControl.focus();
            window.close();
        }
        else if (TargetValue.indexOf("txtNoSubmit") != -1) {
            var ctrl = window.opener.document.forms[0][TargetValue];
            window.opener.document.forms[0][TargetValue].value = cell;
            window.opener.focus();
            window.opener.document.forms[0][TargetValue].focus();
        }
        else {
            var ctrl = window.opener.document.forms[0][TargetValue];
            if (ctrl == undefined) {
                var TabControl = window.opener.igtab_getTabById("UltraWebTab1");
                cellControl = window.opener.igtab_getElementById(TargetValue, TabControl.element);

                cellControl.innerText = cell;
                cellControl.focus();
                window.close();
                return;
            }

            window.opener.document.forms[0][TargetValue].value = cell;
            window.opener.focus();
            window.opener.document.forms[0][TargetValue].focus();
            window.close();
        }
    }
}


function ctrlLastSearchCriteria(gridName) {
    var grid = igtbl_getGridById(gridName);
    var Row;
    var nextEditCell;
    if (grid.Rows.length == 0) {
        Row = igtbl_getRowById(gridName + "_anr");
        nextEditCell = igtbl_getCellById(gridName + "_anc_1");
    }
    else {
        Row = igtbl_getRowById(gridName + "_r_0");
        nextEditCell = igtbl_getCellById(gridName + "_rc_0_0");
    }
    nextEditCell.activate();
    nextEditCell.beginEdit();
    Row.setSelected(true);
}

function UwgSearch_AfterRowActivateHandler(gridName, rowId) {
    var Row = igtbl_getRowById(rowId);
    Row.setSelected(true);
}


/////////////////////////////////////////////////////
// Developer    : [0256]
// Date Created : 04-05-2008
// Description  : This Function is used to get the english 
//                or arabic description of a given searched code according to language used
//////////////////////////////////////////////////////
var webTab;
var ctrTxtCode;
var ctrTxtName;
var lang;
var StrReturned;
var IntPostion;
var lang;
function GetSearchDescription(FormName, SearchId, txtCode, txtName) {
    webTab = igtab_getTabById("UltraWebTab1");
    ctrTxtCode = webTab.findControl(txtCode);
    ctrTxtName = webTab.findControl(txtName);
    lang = new String();
    StrReturned = new String();
    IntPostion = 0;
    lang = GetCookie("Lang");

    PageMethods.Get_Searched_Description(SearchId, ctrTxtCode.value, OnSucceeded, OnFailed);
}
    // GetSearchDescription =====================  [End]


function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'Get_Searched_Description') {
        StrReturned = result;
        if (StrReturned == null)
            return;

        IntPostion = StrReturned.indexOf("/");

        if (lang.indexOf("ar") > -1)
            StrReturned = StrReturned.substring(IntPostion);
        else
            StrReturned = StrReturned.substring(0, IntPostion - 1);

        ctrTxtName.innerText = StrReturned;
        ctrTxtName.value = StrReturned;
    }
}
function OnFailed(error) {

}