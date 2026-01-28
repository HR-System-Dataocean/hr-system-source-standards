/////////////////////////////////
//
//
//
/////////////////////////////////
var bEnterCheck =true

var OtherColumn ;

function UwgSearchCheckColumns(IntColumnIndex){
     
     var ctrlValue   ;
     var CtrNotVisibleValue = window.document.all.item("txtNotVisibleC");
     var CtrReadOnlyValue = window.document.all.item("txtReadOnlyC");
     var CtrGridCountValue = window.document.all.item("txtGridCount");
     
        
     switch(IntColumnIndex)
       {
     
         case 4:
            ctrName = "UltraWebTab1$_ctl0$chkNotVisible" ;
            ctrlValue   = window.document.all.item(ctrName).checked;
            if(ctrlValue==true)
             {
               CtrNotVisibleValue.value= CtrGridCountValue.value;
               CtrReadOnlyValue.value = 0 
             }
            else 
              CtrNotVisibleValue.value = 0 
                        
            UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",5,false);
            window.document.all.item("UltraWebTab1$_ctl0$chkReadOnly").checked=false ;
            break ;
         case 5:
            ctrName = "UltraWebTab1$_ctl0$chkReadOnly" ;
            ctrlValue   = window.document.all.item(ctrName).checked;
            if(ctrlValue==true)
            {
               CtrReadOnlyValue.value= CtrGridCountValue.value;
               CtrNotVisibleValue.value = 0 
            }
            else 
            {
               CtrReadOnlyValue.value = 0 
            }
            
            UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",4,false);
            window.document.all.item("UltraWebTab1$_ctl0$chkNotVisible").checked=false ;
            break ;
       }
       
        
        
        
        UwgSearchUsers_ColumnUpdateHandler("UwgSearchUsers",IntColumnIndex,ctrlValue);
    
}
function UwgSearchUsers_ColumnUpdateHandler(gridName,ColumnIndex,blSign){
     
     var grid              = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
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
    var grid              = igtbl_getGridById("UltraWebTab1xxctl0xUwgSearchUsers");
     
     var i=0 ;
     var CellToChange ; 
     if (grid.Rows.rows.length > 0) {
         for(i=IntStart;i<IntEnd;i++) {
 	          var currRow = grid.Rows.rows[i];
	          var currRow = igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers"+"_r"+i);
	          CellToChange= igtbl_getCellById("UltraWebTab1xxctl0xUwgSearchUsers"+"_rc_"+ i +"_"+ColumnIndex);
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
	var CtrNotVisibleValue = window.document.all.item("txtNotVisibleC");
    var CtrReadOnlyValue = window.document.all.item("txtReadOnlyC");
    var CtrGridCountValue = window.document.all.item("txtGridCount");
    
	if (Cell.getValue() == false )
	{
        switch(Cell.Column.Index )
        { 
        case 4:
        
            window.document.all.item("UltraWebTab1$_ctl0$chkNotVisible").checked=false ;
            CtrNotVisibleValue.value = parseFloat(CtrNotVisibleValue.value)-1 ;
            if (parseFloat(CtrNotVisibleValue.value)<=0 )
                CtrNotVisibleValue.value = 0 ;
            
            
           break;
        case 5:
            
            CtrReadOnlyValue.value = parseFloat(CtrReadOnlyValue.value)-1 ;
            if (parseFloat(CtrReadOnlyValue.value)<=0 )
                CtrReadOnlyValue.value = 0 ;
                
            window.document.all.item("UltraWebTab1$_ctl0$chkReadOnly").checked=false
           break;
        }
    }
    else
    {   
      switch(Cell.Column.Index )
        { 
        case 4:             
            CtrNotVisibleValue.value = parseFloat(CtrNotVisibleValue.value)+1 ;
            if (parseFloat(CtrNotVisibleValue.value)>= parseFloat(CtrGridCountValue.value) )
                {CtrNotVisibleValue.value = CtrGridCountValue.value ;
                 window.document.all.item("UltraWebTab1$_ctl0$chkNotVisible").checked=true ;}
            if (Row.getCell(5).getValue()==true)
              {
                CtrReadOnlyValue.value = parseFloat(CtrReadOnlyValue.value)-1 ;
                if (parseFloat(CtrReadOnlyValue.value)<=0 )
                    CtrReadOnlyValue.value = 0 ;
                Row.getCell(5).setValue(false);    
              }

             break;
        case 5:
            CtrReadOnlyValue.value = parseFloat(CtrReadOnlyValue.value)+1 ;
            if (parseFloat(CtrReadOnlyValue.value)>= parseFloat(CtrGridCountValue.value) )
                {CtrReadOnlyValue.value = CtrGridCountValue.value ;
                window.document.all.item("UltraWebTab1$_ctl0$chkReadOnly").checked=true ;}
               
               if (Row.getCell(4).getValue()==true)
                {
                   CtrNotVisibleValue.value = parseFloat(CtrNotVisibleValue.value)-1 ;
                   if (parseFloat(CtrNotVisibleValue.value)<=0 )
                       CtrNotVisibleValue.value = 0 ;
                   Row.getCell(4).setValue(false);
                } 
               
                break;
        }        
        
    }    
}
//-------------------------------------------