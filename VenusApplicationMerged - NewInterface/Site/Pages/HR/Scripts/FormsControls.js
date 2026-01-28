var FormsCon_doSubmitTag  = 0 ;
/* =============================================
   -- Author        : DataOcean
   -- Date Created  : 
   -- Description   : submint function
   ==============================================*/
function frmFormsControls_Submitfn(){
 if(FormsCon_doSubmitTag == 1)
return  false;
if(FormsCon_doSubmitTag  == 0 )
 return true;
}
/* =============================================
   -- Author        : DataOcean
   -- Date Created  : 
   -- Description   : FormControls_CellUpdateHandler
   ==============================================*/
function FormControls_CellUpdateHandler(gridName,cellId)
{
var currRow    = igtbl_getCellById(cellId)
var grid                = igtbl_getGridById(gridName);

    var arrRow = cellId.split("_");
    var MinRow = igtbl_getCellById("UltraWebTab1xxctl0xUwgFormControls_rc_"+ arrRow[2] + "_16")
    var MaxRow = igtbl_getCellById("UltraWebTab1xxctl0xUwgFormControls_rc_"+ arrRow[2] + "_17")
    var FocusCell  = igtbl_getCellById("UltraWebTab1xxctl0xUwgFormControls_rc_"+ arrRow[2] + "_14")
 
    if( arrRow[3]== "16" ) 
    {
         if( currRow.getValue() !="" && currRow.getValue()> 0)
           {
                if( MaxRow.getValue() >= currRow.getValue() || MaxRow.getValue() == null )
                 {   
                 }
                 else 
                 {
                 currRow.setValue(MaxRow.getValue() ) ;
                 }
                 }
           else 
           {
           currRow.setValue("") ;
           }
                
            
    }
    //Chech Max 
    else  if( arrRow[3]== "17" )
    {
      if( currRow.getValue() !=null  && currRow.getValue()> 0)
        {
          if( MinRow.getValue() <= currRow.getValue())
            {
            }
          else 
            {
            currRow.setValue(MinRow.getValue())  ;
            }
            }
      else
         {
         currRow.setValue("") ;
         }
    }
    
     //Chech Max 
    else  if( arrRow[3]== "14" )
    {
        if( FocusCell.getValue()==true)
           {
           
            for( Count = 0 ; Count < grid.Rows.length ; Count ++)
                {
                    if(Count != arrRow[2])
                    {
                    var NewFocusCell  = igtbl_getCellById("UltraWebTab1xxctl0xUwgFormControls_rc_"+ Count + "_14")
                    NewFocusCell.setValue(false);
                    }
                }
            } 
    }
    else if (arrRow[1]== "anc" )
            {
                if (arrRow[2]== "14")
                {
                currRow.setValue(false);
                }
            else if (arrRow[2]== "16")
                {
                currRow.setValue("");
                }
            else if (arrRow[2]== "17")
                {
                currRow.setValue("");
                }
                
                else if (arrRow[2]== "2")
                {
                 currRow.setValue("");
                }
            }
}
/* =============================================
   -- Author        : DataOcean
   -- Date Created  : 
   -- Description   : FormControls_SaveValidation
   ==============================================*/
function FormControls_SaveValidation()
{
var grid                = igtbl_getGridById("UwgFormControls");

   for( CountRow = 0 ; CountRow < grid.Rows.length ; CountRow++)
      {
     var FieldCell  = igtbl_getCellById("UltraWebTab1xxctl0xUwgFormControls_rc_"+ CountRow + "_2")  
          if(FieldCell.getValue()==null)
            {
            FormsCon_doSubmitTag =1;
            alert("Rows Must Have Field");
            return ;
            }
            else
            {
            }
        }
}
/* =============================================
   -- Author         : [0261]
   -- Date Modified  : 
   -- Description    : UwgFormControls_KeyUpHandler
   ==============================================*/
function UwgFormControls_KeyUpHandler(gridName, cellId, key){

	var cell         = igtbl_getCellById(cellId);
	if ( cell == null ) { return ; }
	var e=window.event;
	if(cell.Column.Index == 18 && key == 120)
    {   
        var searchctrl=window.document.getElementById("txHiddenSearchID");
        var winopen = window.open("frmSearchScreen.aspx?TargetControl="+cellId+"&SearchID=126,_Parent"+1,"height=560,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");		
        winopen.document.focus();
    }
	
}