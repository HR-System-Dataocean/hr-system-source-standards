
//-------------------------------------------------------------------------------------
// App_JScript_M.js
//=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 14/08/2007
// Description : Validate on Grades Transactions on max value must >= minvalue
//Screen       :frmGrades
//========================================================================


function AddRow(gridName, cellId) {
    var count = igtbl_getGridById(gridName).Rows.length - 1;
    var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

    if (rowIndex == count) {

        igtbl_addNew(gridName, 0, true, false);

    }
}


var bEnter = true
function uwgGradesTransactions_AfterCellUpdateHandler(gridName, cellId) {
    
    if (!bEnter) return

    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var currCodeCell;
    var Row = igtbl_getRowById(cellId);
    var rowIndex = Row.getIndex();

    var lang = new String();
    lang = GetCookie("Lang");
    var retMsg = "";
    if (lang.indexOf("ar") > -1) {
        retMsg = "لايسمح بإدخال نفس الحركة أكثر من مرة"
    }
    else {
        retMsg = "Not allowed to enter The Same Transaction more than once "
    }


    if (cell.Column.Index == 2) {

        var nxtCell = igtbl_getCellById(cell.getNextCell().Id);
        var maxValue = nxtCell.getValue();

        if (cell.getValue() < 0) {
            cell.setValue(0);
        }
        //-------------------------------0257 MODIFIED-----------------------------------------
        if (cell.getValue() > 922337203685477.5807) {
            cell.setValue(922337203685477.5807);
        }

        //-------------------------------=============-----------------------------------------
        var minValue = cell.Value;

        if (maxValue < minValue) {
            nxtCell.setValue(minValue)
        }



    }

    if (cell.Column.Index == 3) {


        var prvCell = igtbl_getCellById(cell.getPrevCell().Id);
        var minValue = prvCell.getValue();

        var maxValue = cell.Value;

        if (cell.getValue() < 0) {
            cell.setValue(0);
        }
        //-------------------------------0257 MODIFIED-----------------------------------------
        if (cell.getValue() > 922337203685477.5807) {
            cell.setValue(922337203685477.5807);
        }

        //-------------------------------=============-----------------------------------------                   

        if (minValue > maxValue) {
            prvCell.setValue(maxValue)
        }



    }

    if (cell.Column.Index == 1) {
        for (i = 0; i < grid.Rows.length; i++) {
            var currRow = grid.Rows.rows[i];
            try {
                currCodeCell = currRow.cells[1];
                if (currCodeCell == undefined || dateCell == null) {
                    currCodeCell = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                }
            }
            catch (e) {
                currCodeCell = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                if (currCodeCell == null) {
                    continue;
                }
            }

            if (cell.getValue() == currCodeCell.getValue() && rowIndex != i) {
                bEnter = false
                cell.setValue("0");
                alert(retMsg);
                cell.activate();
                cell.beginEdit();
                bEnter = true
                return;
            }
            else if (cell.getValue() == "0" || cell.getValue() == null) {
                bEnter = false
                cell.activate();
                cell.beginEdit();
                bEnter = true
                return;

            }
        }
    }
}
   
//=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 16/08/2007
// Description : Validate on vacations DG
//Screen       :frmGrades
//========================================================================

    function uwgVacations_AfterCellUpdateHandler(gridName, cellId) {
        
        if (!bEnter) return

        var grid = igtbl_getGridById(gridName);
        var cell = igtbl_getCellById(cellId);
        var currCodeCell;
        var Row = igtbl_getRowById(cellId);
        var rowIndex = Row.getIndex();

        var lang = new String();
        lang = GetCookie("Lang");
        var retMsg = "";
        if (lang.indexOf("ar") > -1) {
            retMsg = "لايسمح بإدخال نفس الأجازة أكثر من مرة"
        }
        else {
            retMsg = "Not allowed to enter The Same Vacation more than once "
        }




        if (cell.Column.Index == 3) {

            var degree = cell.Value;
            if (degree <= 0) {
                cell.setValue("1");
            }
            //-------------------------------0257 MODIFIED-----------------------------------------
            if (degree > 255) {
                cell.setValue(255);
            }

            //-------------------------------=============-----------------------------------------    

        }
        if (cell.Column.Index == 2) {
            var degree = cell.Value;
            if (degree < 0) {
                cell.setValue("0");
            }
            //-------------------------------0257 MODIFIED-----------------------------------------
            if (degree > 255) {
                cell.setValue(255);
            }

            //-------------------------------=============-----------------------------------------    
        }



        if (cell.Column.Index == 1) {
            for (i = 0; i < grid.Rows.length; i++) {
                var currRow = grid.Rows.rows[i];
                try {
                    currCodeCell = currRow.cells[1];
                    if (currCodeCell == undefined || dateCell == null) {
                        currCodeCell = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                    }
                }
                catch (e) {
                    currCodeCell = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                    if (currCodeCell == null) {
                        continue;
                    }
                }

                if (cell.getValue() == currCodeCell.getValue() && rowIndex != i) {
                    bEnter = false
                    cell.setValue("0");
                    alert(retMsg);
                    cell.activate();
                    cell.beginEdit();
                    bEnter = true
                    return;
                }
                else if (cell.getValue() == "0" || cell.getValue() == null) {
                    bEnter = false
                    cell.activate();
                    cell.beginEdit();
                    bEnter = true
                    return;

                }
            }
        }
    }

//=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 09/08/2007
// Description : Validate on txtRegularHours is between 1 and 24 
// input       : Nothing 
//Screen       :frmGrades
//========================================================================
    function txtRegularHours_Changed() {

        var webTab = igtab_getTabById("UltraWebTab1");
        if (webTab == null)
            return;
        var txtRegularHours = igtab_getElementById("txtRegularHours", webTab.element);

        //var txtRegularHours=window.document.getElementById("txtRegularHours");
        var val = txtRegularHours.value
        if (val > 24) {
            txtRegularHours.value = 24
        }
        if (val <= 0) {
            txtRegularHours.value = 1
        }
    }

//   ========================================================================
//   ProcedureName  :  txtRegularHoursLostFocusToGrid 
//   Screen         :  frmGrades
//   Project        :  Venus V.
//   Description    :  Get row index from cell id
//   Developer      :  [0260]
//   fn. Arguments  :
//   /////////////////////////////////

 function txtRegularHoursLostFocusToGrid() {
     var webTab = igtab_getTabById("UltraWebTab1");
     if (webTab == null)
         return;
     var gridid = igtab_getElementById("uwgVacations", webTab.element).id;


     var grid = igtbl_getGridById(gridid);

     var cellid = gridid.split("_");
     var nextEditCell;

     if (grid.Rows.rows.length == 0)
         nextEditCell = igtbl_getCellById(grid.Id + "_anc_1");
     else
         nextEditCell = igtbl_getCellById(grid.Id + "_rc_0_1");

     if (nextEditCell != null) {
         nextEditCell.activate();
         nextEditCell.beginEdit();
     }
 }

function uwgGradesTransactions_EditKeyUpHandler(gridName, cellId, key) {
    var cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId);
    var e = window.event;

    AddRow(gridName, cellId);

    if (key == 119) {
        //igtbl_deleteRow(gridName , Row.Id)
        Row.deleteRow();
        Row.remove();
    }
}

function uwgVacations_EditKeyUpHandler(gridName, cellId, key) {
    var cell = igtbl_getCellById(cellId)
    var Row = igtbl_getRowById(cellId);
    var e = window.event;

    AddRow(gridName, cellId);

    if (key == 119) {
        //igtbl_deleteRow(gridName , Row.Id)
        Row.deleteRow();
        Row.remove();
    }
}

function uwgVacations_CellChangeHandler(gridName, cellId) {

    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var currCodeCell;
    var Row = igtbl_getRowById(cellId);
    var firstCell = Row.getCellFromKey("VacationTypeID");
    if (firstCell.getValue() == "0" || firstCell.getValue() == null) {
        firstCell.activate();
        firstCell.beginEdit();
    }
}

function uwgGradesTransactions_CellChangeHandler(gridName, cellId) {

    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var currCodeCell;
    var Row = igtbl_getRowById(cellId);
    var firstCell = Row.getCellFromKey("TransactionTypeID");
    if (firstCell.getValue() == "0" || firstCell.getValue() == null) {
        firstCell.activate();
        firstCell.beginEdit();
    }
}
