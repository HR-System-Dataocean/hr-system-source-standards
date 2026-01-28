



function UwgSearchCheckColumns(IntColumnIndex)
{  
   var ctrlValue   = window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked;
   if (ctrlValue == true)
       window.document.getElementById("txtCanViewC").value = window.document.getElementById("txtGridCount").value;
   else
       window.document.getElementById("txtCanViewC").value = 0;
       
       UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",IntColumnIndex,ctrlValue);
    
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
	          if(blSign==true)
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












//-------------------------------------------
function UwgSearchUsers_AfterCellUpdateHandler(gridName, cellId)
{
    var Cell           = igtbl_getCellById(cellId);
	var Row            = Cell.getRow();	
	var ctrViewCount   =         window.document.getElementById("txtCanViewC");
	var ctrGirdCount   =        window.document.getElementById("txtGridCount");
	if (Cell.getValue() == false )
	{
        //if(Cell.Column.Index == 3)
       // {      
           window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=false ;        
           ctrViewCount.value = parseFloat(ctrViewCount.value) - 1
           if(parseFloat(ctrViewCount.value)<=0)
              ctrViewCount.value = 0 ;
       // }
    }
   else
   {
      ctrViewCount.value = parseFloat(ctrViewCount.value) + 1;
      if(parseFloat(ctrViewCount.value)>= parseFloat(ctrGirdCount.value))
         ctrViewCount.value = parseFloat(ctrGirdCount.value) ;
      if(parseFloat(ctrViewCount.value)==parseFloat(ctrGirdCount.value))
         window.document.all.item("UltraWebTab1$_ctl0$chkCheckAll").checked=true ;        
      
      
   } 
}
//-------------------------------------------


