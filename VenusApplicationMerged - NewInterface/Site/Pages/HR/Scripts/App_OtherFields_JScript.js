// Author        : [0256]
// Date Created  : 7-2007 
// Date Modified : 20-04-2008 
// Description   : The file contains the other fields creation and data collection script
//=============================================================
function SaveOtherFieldsData() {
    debugger
    var Parameter = window.document.getElementById("name")
    var realParameter = window.document.getElementById("realname")
    var Value = Parameter.innerHTML
    var realValue = realParameter.innerHTML

    var Arr = Value.split("|");
    var realArr = realValue.split("|");

    var Final_Value = "";
    var FinalTest = "";
    var webTab = igtab_getTabById("UltraWebTab1");
    if (webTab == null)
        return;
    for (i = 0; i < Arr.length; i++) {      
        var str = new String();
        var str = Arr[i].substring(0, 3)

        switch (str) {
            case ("WV_"):
                {
                    var Control = webTab.findControl(Arr[i]);
                    //var Control =window.document.getElementById(Arr[i]);
                    Final_Value += " @" + realArr[i] + "%" + Control.value;
                    break;
                }
            case ("WN_"):
                {
                    var Control = webTab.findControl(Arr[i]);
                    //var Control =window.document.getElementById(Arr[i]);
                    Final_Value += " @" + realArr[i] + "%" + Control.value;
                    break;
                }
            case ("WD_"):
                {
                    var Control = webTab.findControl(Arr[i]);
                    var Control = igdrp_getComboById(Control.id)
                    if (Control.getValue() != undefined)
                        Final_Value += " @" + realArr[i] + "%" + Control.getText();
                    break;
                }
            case ("WB_"):
                {
                    var Control = webTab.findControl(Arr[i]);
                    //var Control  = window.document.getElementById(Arr[i]);
                    Final_Value += " @" + realArr[i] + "%" + Control.value
                    break;
                }
        }
    }

    var Target_File = window.document.getElementById("value")
    Target_File.value = Final_Value
    //window.document.form1.submit() 
    //window.close();
}


function btnCancelOtherFields_Click(oButton, oEvent) {
    window.close();
}




