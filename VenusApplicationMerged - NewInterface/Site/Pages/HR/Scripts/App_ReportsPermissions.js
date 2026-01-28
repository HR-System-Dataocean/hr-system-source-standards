/////////////////////////////////
//
//
//
/////////////////////////////////
var bEnterCheck =true;
function UwgSearchUsers_AfterCellUpdateHandler(gridName, cellId){

    if (!bEnterCheck)
        return ;
    var Cell           = igtbl_getCellById(cellId);
	var Row            = Cell.getRow()
	var CellCheckAll   = Row.getCell(7).getValue(); 
     
    if (Cell.Column.Key == "CheckAll") 
     {
       
       if(CellCheckAll==true || CellCheckAll==1) { 
          bEnterCheck = false;
          
          if(!Row.getCell(3).getValue()) 
             checkReachCount("txtCanViewC",true);
          
          if(!Row.getCell(4).getValue()) 
             checkReachCount("txtCanPrintC",true);
          
          if(!Row.getCell(5).getValue()) 
             checkReachCount("txtCanExportC",true);
          
          Row.getCell(3).setValue(true);
          Row.getCell(4).setValue(true);
          Row.getCell(5).setValue(true);
          
          checkReachCount("txtGridCount",true);
          bEnterCheck = true;   }
         else {
          bEnterCheck = false;
          
          if(Row.getCell(3).getValue()) {
             checkReachCount("txtCanViewC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked=false; }
             
          if(Row.getCell(4).getValue()) {
             checkReachCount("txtCanPrintC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=false; }
             
          if(Row.getCell(5).getValue()) {
             checkReachCount("txtCanExportC",false);
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=false; }
             
          Row.getCell(3).setValue(false);
          Row.getCell(4).setValue(false);
          Row.getCell(5).setValue(false); 
          checkReachCount("txtGridCount",false);
          window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=false; 
          bEnterCheck = true;  }
  
    }    
       
    else if(Cell.Column.Key == "AllowView") 
      {
          var CellAllowView   = Row.getCell(3).getValue(); 
          if(CellAllowView==false){
             bEnterCheck = false;
             
            if(Row.getCell(4).getValue()) {
               checkReachCount("txtCanPrintC",false);
               window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=false; }
          
            if(Row.getCell(5).getValue()) {
               checkReachCount("txtCanExportC",false);
               window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=false; }
               
            // Row.getCell(3).setValue(false);
             Row.getCell(4).setValue(false);
             Row.getCell(5).setValue(false);
             window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=false; 
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked=false; 
                          
             checkAllChecks(Row.getCell(3).getValue(),Row.getCell(4).getValue(),Row.getCell(5).getValue(),Row.getCell(7)) 
             checkReachCount("txtCanViewC",false);
             checkReachCount("txtGridCount",false);
             
            
             bEnterCheck = true;
             }
         else
            checkReachCount("txtCanViewC",true);   
       }
       
     else if(Cell.Column.Key == "AllowPrint" || Cell.Column.Key=="AllowExport" ) 
      {
          if(Cell.Value==true){
             bEnterCheck = false;
             switch(Cell.Column.Key )
               { 
               case "AllowPrint":
                   checkReachCount("txtCanPrintC",true);
                   if(!Row.getCell(3).getValue()) 
                      checkReachCount("txtCanViewC",true);
                   break;
                   
               case "AllowExport":
                   checkReachCount("txtCanExportC",true);
                   if(!Row.getCell(3).getValue()) 
                      checkReachCount("txtCanViewC",true);
                   break;
               
                }
                
             Row.getCell(3).setValue(true);
             checkAllChecks(Row.getCell(3).getValue(),Row.getCell(4).getValue(),Row.getCell(5).getValue(),Row.getCell(7))
             bEnterCheck = true;
            }   
          else
            {
             bEnterCheck = false;
             
             switch(Cell.Column.Key )
               { 
               case "AllowPrint":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=false;  
                   checkReachCount("txtCanPrintC",false);   
                   break;
                   
               case "AllowExport":
                   window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=false;  
                   checkReachCount("txtCanExportC",false);
                   break;
              
                }
             window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=false;
             Row.getCell(7).setValue(false); 
             bEnterCheck = true;
           }
     }
  
     
}


function UwgSearchCheckColumns(IntColumnIndex){
     switch(IntColumnIndex)
       {
            case 3:
                ctrName = "UltraWebTab1$_ctl0$chkAllowView" ;
                StrHoldingCountsCtrName  = "txtCanViewC" ;
                break;

            case 4:
                ctrName = "UltraWebTab1$_ctl0$chkAllowPrint" ;
                StrHoldingCountsCtrName  = "txtCanPrintC" ;
                break;

            case 5:
                ctrName = "UltraWebTab1$_ctl0$chkAllowExport" ;
                StrHoldingCountsCtrName  = "txtCanExportC" ;
                break ;

            case 7:
                ctrName = "UltraWebTab1$_ctl0$chkCheckAll" ;
                StrHoldingCountsCtrName  = "txtGridCount" ;
                break;
     }

     var ctrlValue   = window.document.all.item(ctrName).checked;
     var MaxValue    =  window.document.all.item("txtGridCountAll").value ;
     
     
     if (ctrlValue   ==true) {
         ctrlValue   = 1;
         window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked=true;
         window.document.all.item("txtCanViewC").value = MaxValue ; 
         window.document.all.item(StrHoldingCountsCtrName).value = MaxValue ; 
         
         if (IntColumnIndex == 7 && ctrlValue ==1)
           {
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=true;
             window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=true;
             window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=true; 
             
             window.document.all.item("txtCanViewC").value = MaxValue ; 
             window.document.all.item("txtCanPrintC").value = MaxValue ; 
             window.document.all.item("txtCanExportC").value = MaxValue ; 
             window.document.all.item("txtGridCount").value = MaxValue ; 
           }
       }
     else
       {
        ctrlValue   = 0; 
        if (IntColumnIndex == 7 && ctrlValue ==0)
          {
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=false;
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=false;
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked=false;
            
            window.document.all.item("txtCanViewC").value = 0 ; 
            window.document.all.item("txtCanPrintC").value = 0 ; 
            window.document.all.item("txtCanExportC").value = 0 ; 
            window.document.all.item("txtGridCount").value = 0 ; 
          }
         else if (IntColumnIndex == 3 && ctrlValue ==0)
          {
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked=false;
            window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked=false;
            
            window.document.all.item("txtCanViewC").value = 0 ; 
            window.document.all.item("txtCanPrintC").value = 0 ; 
            window.document.all.item("txtCanExportC").value = 0 ; 
            window.document.all.item("txtGridCount").value = 0 ; 
          }
         else
            window.document.all.item(StrHoldingCountsCtrName).value = 0 ; 
            
          window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=false; 
        }

     UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",IntColumnIndex,ctrlValue);
     CheckColumnsChecks(window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked,window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked,window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked,window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll"))
}
function UwgSearchUsers_ColumnUpdateHandler(gridName,ColumnIndex,blSign){
     
     var grid              = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
     var gridLength        =grid.Rows.length;
     var i=0;
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*0)+","  +Math.floor(gridLength*1/4)+")",10 *1); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*1)+","  +Math.floor(gridLength*2/4)+")",10 *2); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*2)+","  +Math.floor(gridLength*3/4)+")",10 *3); 
          setTimeout ( "manipulateChecks('"+gridName+"',"+ColumnIndex+","+blSign+","+Math.floor(gridLength/4*3)+","  +Math.floor(gridLength*4/4)+")",10 *4); 
}
var intCounter = 0 ;
function manipulateChecks(gridName,ColumnIndex,blSign,IntStart,IntEnd)
{
     var grid              = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
     
     var i=0 ;
     var CellToChange ; 
     if (grid.Rows.rows.length > 0) {
         for(i=IntStart;i<IntEnd;i++) {
 	          var currRow = grid.Rows.rows[i];
	          var currRow = igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers"+"_r"+i);
	          CellToChange= igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers"+"_rc_"+ i +"_"+ColumnIndex);
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

function checkReachCount(ctrlName,blSgin)
{
  if(blSgin){
       window.document.all.item(ctrlName).value = parseFloat(window.document.all.item(ctrlName).value) + 1 ;
       
       if (parseFloat(window.document.all.item(ctrlName).value) >  parseFloat(window.document.all.item("txtGridCountAll").value))
            window.document.all.item(ctrlName).value            = window.document.all.item("txtGridCountAll").value
       if (parseFloat(window.document.all.item(ctrlName).value) == parseFloat(window.document.all.item("txtGridCountAll").value) )
       switch(ctrlName)
        { 
        case "txtCanViewC":
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked= true;
              break;
        case "txtCanPrintC":
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked= true;
              break;
        case "txtCanExportC":
              window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked= true;
              break;
        case "txtGridCount":
              window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked= true;
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
    
     CheckColumnsChecks(window.document.all.item("UltraWebTab1$_ctl0$chkAllowView").checked,window.document.all.item("UltraWebTab1$_ctl0$chkAllowPrint").checked,window.document.all.item("UltraWebTab1$_ctl0$chkAllowExport").checked,window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll"))          
}


function checkAllChecks(blView,blPrint,blExport,CheckAllCell){
     if(blView && blPrint && blExport )
         CheckAllCell.setValue(true);
      else
         CheckAllCell.setValue(false);
}

function CheckColumnsChecks(blView,blPrint,blExport,CheckAll){
     if(blView && blPrint && blExport )
        CheckAll.checked = true;
     else
        CheckAll.checked = false;
}
