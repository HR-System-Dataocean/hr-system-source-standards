/////////////////////////////////
//
//
//
/////////////////////////////////
var bEnterCheck =true
function UwgSearchUsers_AfterCellUpdateHandler(gridName, cellId){

    if (!bEnterCheck)
        return ;
    var Cell           = igtbl_getCellById(cellId);
	var Row            = Cell.getRow()
	var CellCheckAll   = Row.getCell(9).getValue(); 
     
    if (Cell.Column.Key == "CheckAll") 
     {
       
       if(CellCheckAll==true) { 
          bEnterCheck = false;
          if(!Row.getCell(8).getValue()) 
          checkReachCount("txtIsSpecificC",true);
          
          if(!Row.getCell(4).getValue()) 
          checkReachCount("txtEditC",true);
          
          if(!Row.getCell(5).getValue()) 
          checkReachCount("txtDeleteC",true);
          
          if(!Row.getCell(6).getValue()) 
          checkReachCount("txtViewC",true);
          
          if(!Row.getCell(7).getValue()) 
          checkReachCount("txtPrintC",true);
          
          Row.getCell(6).setValue(true);
          Row.getCell(4).setValue(true);
          Row.getCell(5).setValue(true);
          Row.getCell(7).setValue(true);
          Row.getCell(8).setValue(true);
          checkReachCount("txtGridCount",true);
          
          ///
          
          bEnterCheck = true; 
         }
       else {
          bEnterCheck = false;
          
          if(Row.getCell(8).getValue()) {
             checkReachCount("txtIsSpecificC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = false;
         }
          
          if(Row.getCell(4).getValue()) {
             checkReachCount("txtEditC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = false;
         }
          
          if(Row.getCell(5).getValue()) {
             checkReachCount("txtDeleteC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = false;
         }
          
          if(Row.getCell(6).getValue()) {
             checkReachCount("txtViewC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = false;
         }
          
          if(Row.getCell(7).getValue()) {
             checkReachCount("txtPrintC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = false;
         }
          
          Row.getCell(6).setValue(false);
          Row.getCell(4).setValue(false);
          Row.getCell(5).setValue(false);
          Row.getCell(7).setValue(false); 
          Row.getCell(8).setValue(false);
          window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = false; 
          checkReachCount("txtGridCount",false);
          ///
          bEnterCheck = true;
         }  

      }
    else if(Cell.Column.Key == "AllowView") 
      {
          var CellAllowView   = Row.getCell(6).getValue(); 
          if(CellAllowView==false){
             bEnterCheck = false;
             if(Row.getCell(8).getValue()) {
                checkReachCount("txtIsSpecificC",false);
                window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = false;
            }
          
             if(Row.getCell(4).getValue()) {
                checkReachCount("txtEditC",false);
                window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = false;
            }
          
             if(Row.getCell(5).getValue()) {
                checkReachCount("txtDeleteC",false);
                window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = false;
            }
          
             if(Row.getCell(7).getValue()) {
                checkReachCount("txtPrintC",false);
                window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = false;
            }
             
             Row.getCell(8).setValue(false);
             Row.getCell(4).setValue(false);
             Row.getCell(5).setValue(false);
             Row.getCell(7).setValue(false);   
             Row.getCell(9).setValue(false);
             window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = false;
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = false; 
             checkAllChecks(Row.getCell(8).getValue(),Row.getCell(4).getValue(),Row.getCell(5).getValue(),Row.getCell(6).getValue(),Row.getCell(7).getValue(),Row.getCell(9));
             checkReachCount("txtViewC",false);
             checkReachCount("txtGridCount",false);
             ////
             
             bEnterCheck = true;
             }   
             else
             checkReachCount("txtViewC",true);
       }
       
     else if(Cell.Column.Key == "CantEdit" || Cell.Column.Key=="CantDelete" || Cell.Column.Key == "CantView" || Cell.Column.Key == "CantPrint" || Cell.Column.Key =="IsSpacific" ) 
      {
          if(Cell.Value==true){
             bEnterCheck = false;
            
           
             switch(Cell.Column.Key )
               { 
               case "CantEdit":
                   checkReachCount("txtEditC",true);
                   break;
               case "CantDelete":
                   checkReachCount("txtDeleteC",true);
                   break;
               case "CantView":
                   checkReachCount("txtViewC",true);
                   break;
               case "IsSpacific":
                   checkReachCount("txtIsSpecificC",true);
                   break;
               case "CantPrint":
                   checkReachCount("txtPrintC",true);
                   break;
                }
               //Row.getCell(6).setValue(true);
               checkAllChecks(Row.getCell(8).getValue(),Row.getCell(4).getValue(),Row.getCell(5).getValue(),Row.getCell(6).getValue(),Row.getCell(7).getValue(),Row.getCell(9));
             bEnterCheck = true;
             }   
           else
             {
             bEnterCheck = false;
             
             switch(Cell.Column.Key )
               { 
               case "CantPrint":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = false;
                   checkReachCount("txtPrintC",false);
                   break;
               case "CantDelete":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = false;
                   checkReachCount("txtDeleteC",false);
                   break;
               case "CantEdit":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = false;
                   checkReachCount("txtEditC",false);
                   break;
               case "IsSpacific":
                   window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = false;
                   checkReachCount("txtIsSpecificC",false);
                   break;
               case "CantView":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = false;
                   checkReachCount("txtViewC",false);
                   break;
                }
           window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = false;
             Row.getCell(9).setValue(false); 
             bEnterCheck = true;
             }
       }
  
    
    
}


function UwgSearchCheckColumns(IntColumnIndex){
     var StrHoldingCountsCtrName ;
     switch(IntColumnIndex)
       {
         case 8:
             ctrName = "UltraWebTab1$_ctl0$chkIsSpecific";
            StrHoldingCountsCtrName  = "txtIsSpecificC" ;
            break ;
         case 4:
             ctrName = "UltraWebTab1$_ctl0$chkAllowEdit";
            StrHoldingCountsCtrName  = "txtEditC" ;
            break ;
         case 5:
             ctrName = "UltraWebTab1$_ctl0$chkAllowDelete";
            StrHoldingCountsCtrName  = "txtDeleteC" 
            break ;
         case 6:
             ctrName = "UltraWebTab1$_ctl0$chkAllowView";
            StrHoldingCountsCtrName  = "txtViewC" 
            break;
         case 7:
             ctrName = "UltraWebTab1$_ctl0$chkAllowPrint";
            StrHoldingCountsCtrName  = "txtPrintC" 
            break;
         case 9:
             ctrName = "UltraWebTab1$_ctl0$chkCheckAll";
            StrHoldingCountsCtrName  = "txtGridCount" 
            break;
     }

     var ctrlValue   = window.document.all.item(ctrName).checked;
     var MaxValue    =  window.document.all.item("txtGridCountAll").value ;
     
     if (ctrlValue   ==true) {
         ctrlValue   = 1;
         //window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked=true;
         //window.document.all.item("txtViewC").value = MaxValue; 
         window.document.all.item(StrHoldingCountsCtrName).value = MaxValue ; 
         if (IntColumnIndex == 9 && ctrlValue ==1)
           {
               window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = true;
               window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = true;
               window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = true;
               window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = true;
               window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = true;

               window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = true;
             
             window.document.all.item("txtIsSpecificC").value = MaxValue ; 
             window.document.all.item("txtEditC").value = MaxValue ; 
             window.document.all.item("txtDeleteC").value = MaxValue ; 
             window.document.all.item("txtPrintC").value = MaxValue ; 
             window.document.all.item("txtViewC").value = MaxValue ; 
             window.document.all.item("txtGridCount").value = MaxValue ; 
       
           }
       }
     else
       {
        ctrlValue   = 0; 
        if (IntColumnIndex == 9 && ctrlValue ==0)
          {
              window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = false;
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = false;
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = false;
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = false;
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = false;
            
            window.document.all.item("txtIsSpecificC").value = 0 ; 
            window.document.all.item("txtEditC").value = 0 ; 
            window.document.all.item("txtDeleteC").value = 0 ; 
            window.document.all.item("txtPrintC").value = 0 ; 
            window.document.all.item("txtViewC").value = 0 ; 
            window.document.all.item("txtGridCount").value = 0 ; 
          }
      else
      window.document.all.item(StrHoldingCountsCtrName).value = 0 ;

      window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = false; 
        }

     UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",IntColumnIndex,ctrlValue);
     CheckColumnsChecks(window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked, window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll"))
}
function UwgSearchUsers_ColumnUpdateHandler(gridName,ColumnIndex,blSign){

    var grid = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
     var gridLength        = grid.Rows.rows.length ; 
     var i=0;
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*0)+","  +Math.floor(gridLength*1/4)+")",10 *1); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*1)+","  +Math.floor(gridLength*2/4)+")",10 *2); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*2)+","  +Math.floor(gridLength*3/4)+")",10 *3); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*3)+","  +Math.floor(gridLength*4/4)+")",10 *4); 
}

var intCounter = 0 ;
function manipulateChecks(gridName,ColumnIndex,blSign,IntStart,IntEnd)
{
    var grid = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
     
     var i=0 ;
     var CellToChange ; 
     if (grid.Rows.rows.length > 0) {
         for(i=IntStart;i<IntEnd;i++) {
 	          var currRow = grid.Rows.rows[i];
 	          var currRow = igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers" + "_r" + i);
 	          CellToChange = igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers" + "_rc_" + i + "_" + ColumnIndex);
	          if(blSign==1)
	             CellToChange.setValue(true);
	           else
	             CellToChange.setValue(false);
	      }//For End
	 }//If statment
	 
	 if(intCounter==4)
	    clearTimeout(0);
	 else
	 intCounter+=1;
}

function checkAllChecks(blAdd,blEdit,blDelete,blView,blPrint,CheckAllCell){
     if(blAdd && blEdit && blDelete && blView && blPrint)
        {
        CheckAllCell.setValue(true);
        //checkReachCount("txtGridCount",true);
        }
     else
       {
        CheckAllCell.setValue(false);
        //checkReachCount("txtGridCount",false);
        }
}

function CheckColumnsChecks(blAdd,blEdit,blDelete,blView,blPrint,CheckAll){
     if(blAdd && blEdit && blDelete && blView && blPrint)
        CheckAll.checked = true;
     else
        CheckAll.checked = false;
}

function checkReachCount(ctrlName,blSgin)
{
  if(blSgin){
       window.document.all.item(ctrlName).value = parseFloat(window.document.all.item(ctrlName).value) + 1 ;
       
       if (parseFloat(window.document.all.item(ctrlName).value) >  parseFloat(window.document.all.item("txtGridCountAll").value))
            window.document.all.item(ctrlName).value            = window.document.all.item("txtGridCountAll").value
       if (parseFloat(window.document.all.item(ctrlName).value) == parseFloat(window.document.all.item("txtGridCountAll").value) )
       switch(ctrlName)
        { 
        case "txtIsSpecificC":
            window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked = true;
              break;
        case "txtEditC":
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked = true;
              break;
        case "txtDeleteC":
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked = true;
              break;
        case "txtViewC":
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked = true;
              break;
        case "txtPrintC":
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked = true;
              break;
         case "txtGridCount":
             window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked = true;
              break;     
       }
       else
       window.document.all.item("txtGridCount").value  = parseFloat(window.document.all.item("txtGridCount").value) +1 ;
    }
  else
    {
       window.document.all.item(ctrlName).value =parseFloat(window.document.all.item(ctrlName).value) - 1 ;
       window.document.all.item("txtGridCount").value  = parseFloat(window.document.all.item("txtGridCount").value) -1 ;
       if(parseFloat(window.document.all.item("txtGridCount").value)< 0 )
          window.document.all.item("txtGridCount").value = 0 
          
       if(parseFloat(window.document.all.item(ctrlName).value)< 0 )
          window.document.all.item(ctrlName).value = 0 
    }

   CheckColumnsChecks(window.document.all.item("UltraWebTab1$_ctl0$chkIsSpecific").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowEdit").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowDelete").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked, window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked, window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll"))          
}