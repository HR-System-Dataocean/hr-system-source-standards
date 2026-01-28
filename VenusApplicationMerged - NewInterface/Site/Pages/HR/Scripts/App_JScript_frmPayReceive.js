// JScript File

var cDate = 1;
var cRemain = 2;
var cPaid = 3;
var cAmount = 4;
var cSettle = 5;

 function CloseSettlementAmont()
        {
            var webTab = igtab_getTabById("UltraWebTab1");
            if(webTab == null)
              return ;
            var txtSettlementAmontId = igtab_getElementById("UltraWebTab1__ctl0_txtSettlementAmont").id;
            var Settlement           = igedit_getById(txtSettlementAmontId)
            //window.document.getElementById("txtSettlementAmont")
            //var tlb                     = igtbar_getToolbarById("TlbMainToolbar")
                       
        }
    
function uwgBenetitTemplet_AfterCellUpdateHandler(gridName, cellId)
{

     var webTab = igtab_getTabById("UltraWebTab1");
     if(webTab == null)
       return ;
      
    var grid            = igtbl_getGridById(gridName)
    var cell            = igtbl_getCellById(cellId)
    //var Settlementvalue = Math.abs(window.document.getElementById("txtSettlementAmont").value) 
    var txtSettlementAmontId = igtab_getElementById("UltraWebTab1__ctl0_txtSettlementAmont").id;
    var Settlementvalue      = Math.abs(igedit_getById(txtSettlementAmontId).getValue())

    if (cell.Column.Index == cSettle)
    {
      	            
        var rowindex         = cell.Row.getIndex()
        var cellcheck        = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cSettle)
        var cellamount       = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cAmount)
        var cellpayed        = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cPaid)
        var cellremain       = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cRemain)
        
        var cellamountvalue  = Math.abs(cellamount.getValue())
        var cellpayedvalue   = cellpayed.getValue()
        var cellremainvalue  = cellremain.getValue()
      
      
        var checkvalue  = cellcheck.getValue()      
        if ( checkvalue == "true")
        {    
            if (Settlementvalue >= cellamountvalue)
            {
                 cellpayed.setValue(cellamountvalue)   
                 cellremain.setValue(0)
                 //window.document.getElementById("txtSettlementAmont").value=   Settlementvalue - cellamountvalue
                 igedit_getById(txtSettlementAmontId).setValue(Settlementvalue - cellamountvalue)
            }
            else
            {
                 cellpayed.setValue(Settlementvalue)     
                 cellremain.setValue(cellamountvalue-Settlementvalue)  
                 //window.document.getElementById("txtSettlementAmont").value=   0
                 igedit_getById(txtSettlementAmontId).setValue(0) 
            }
        }
        else
        {
          cellpayed.setValue(0)   
          cellremain.setValue(cellamountvalue)
          //window.document.getElementById("txtSettlementAmont").value=   Settlementvalue + cellpayedvalue
          igedit_getById(txtSettlementAmontId).setValue(Settlementvalue + cellpayedvalue)
        }
        	            	            
    }//End of if (cell.Index=1)
  if (cell.Index == cPaid)
    {
        var rowindex         = cell.Row.getIndex();
        var cellcheck        = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cSettle);
        var checkvalue       = cellcheck.getValue();
        if ( checkvalue == "true") {

            var prvCell = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cAmount); // cell.getNextCell();
            var nxtCell = igtbl_getCellById(gridName + "_rc_" + rowindex + "_" + cRemain); // cell.getPrevCell();
            var prvVal = prvCell.getValue();

//            var prvCell =cell.getPrevCell();
//            var nxtCell =cell.getNextCell();
//            var prvVal  =prvCell.getValue();
        
            var currVal =cell.getValue();
            if(currVal>prvVal)
            {
                cell.setValue(prvVal);
                currVal =cell.getValue();
            }//end if (currVal>prvVal)
        
            if(currVal <0)
            {
                cell.setValue(0);
                currVal =cell.getValue();
            }//end if (currVal<0)
        
            nxtCell.setValue(prvVal-currVal);
        }//end if cellCheck =true
        else
        {
            cell.setValue(0);
        }//end else if cellCheck =true
    
    }//End of if (cell.Index=2)

} //end function uwgBenetitTemplet_AfterCellUpdateHandler

function frmUnLoad() {
    //debugger;
    try {
        window.opener.document.forms[0].submit();
    }
    catch (ex) { 
    }
    window.close();
}

        