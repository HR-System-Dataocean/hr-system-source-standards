

//-------------//Remove This fn In Payroll-----------------------------------------------
//    Screen         :  frmEndOfService.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Modified  :  24-04-2008
//    function name  :  uwgEndOfServiceRules_CellChangeHandler

var winopen;


function uwgEndOfServiceRules_CellChangeHandler(gridName,cellId)
{
     var cell              = igtbl_getCellById(cellId);
     
     if (window.event.keyCode==9 && cell.Column.Index== 3) //& cellId.indexOf("anc")> -1 )
     {     
          var controlvalue = cell.getValue();          
	      var queryString="?ControlName="+cellId+"&ControlValue="+controlvalue +"&ControlType=G";
          winopen=window.open("frmFormulaDesigner.aspx"+queryString,"_Parent","height=318px,width=780px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");  
     }  	
                                      
}




//-------------//Remove This fn In Payroll-----------------------------------------------
function uwgEndOfServiceRules_AfterEnterEditModeHandler(gridName,cellId)
{
if (winopen!=null)
{
winopen.focus();
}
}
//-------------//Remove This fn In Payroll-----------------------------------------------
//    ========================================================================
//    Screen         :  frmEndOfService.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Created   :  24-04-2008
//    function name  :  frmEnd_txtArabicLostFocusToGrid
//      
//=========================== [Start] 

function frmEnd_txtArabicLostFocusToGrid()
{
     var webTab = igtab_getTabById("UltraWebTab1");
     if(webTab == null)
        return ;
     
     var control                = igtab_getElementById("uwgEndOfServiceRules", webTab.element) //webTab.findControl(gridName);
     
     var grid   = igtbl_getGridById(control.id)
     var nextEditCell;
    
    if (grid.Rows.length == 0)
    {
         nextEditCell = igtbl_getCellById(grid.Id+"_anc_1");
         frmEnd_NextGridLostFocus(grid.Id,"#_#_#_#")
    }
    else
    {
    frmEnd_NextGridLostFocus(grid.Id,grid.Id+"_rc_0_0")
//        nextEditCell = igtbl_getCellById(grid.Id+"_rc_0_0");
//        nextEditCell.activate();
//        nextEditCell.beginEdit();
    }
    
           
   
}


//    Screen         :  frmEndOfService.aspx
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  Move Tab Between Controls
//    Developer      :  [0261]
//    Date Created   :  24-04-2008
//    function name  :  frmEnd_NextGridLostFocus
//      
//=========================== [Start] 

//-------------//Remove This fn In Payroll-----------------------------------------------
function frmEnd_NextGridLostFocus(gridName,cellId)
{
        //var ultraTab       = igtab_getTabById("UltraWebTab1");
    if (cellId == "")
    {
    return ;
    }
     uwgEndOfServiceRules_CellChangeHandler(gridName,cellId);
      
     var webTab = igtab_getTabById("UltraWebTab1");
     if(webTab == null)
        return ;
     
     var control  = igtab_getElementById("txtCode", webTab.element) 
     var CtrTxt   = igtbl_getGridById(control.id)
       
     
      
      var grid             = igtbl_getGridById(gridName);
      var cell             = igtbl_getCellById(cellId);
        
      var RowsCount = grid.Rows.length -1 ;
        
      var arrCount = cellId.split("_");
	  
	  if(arrCount[1] == "anc"  )
	  { 	     
         CtrTxt.focus();
      }
      else  if(arrCount[2] == "#" && arrCount[3] == "#"  )
      {
        try
        {
            CtrTxt.focus();
        }
        catch(ex)
        {
        }
      }
      else
      {
        cell.activate();
        cell.beginEdit()
      }
      
   
}

//-------------//Remove From App_JScript.js-----------------------------------------------
function uwgEndOfServiceRules_KeyDownHandler(gridName, cellId, key){
	//Add code to handle your event here.
	    e=window.event;
    var code =e.keyCode;
    
    
    if( e.ctrlKey && code==21 )
    {
        var queryString="?ControlName="+controlName+"&ControlValue="+ControlValue+"&ControlType="+ControlType;
        window.open("frmFormulaDesigner.aspx"+queryString,"_Parent","height=368px,width=550px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
        
    } 

}

	
//-------------//Remove From App_JScript.js-----------------------------------------------
function fngrid_EndOfServices(gridName, cellId){
	     
	       var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       var Row               =  igtbl_getRowById(cellId);
	       var PreviousCell ;
	       var NextCell ;
	       var RowIndex          =	Row.getIndex();	 
           if (cell.Column.Index== 2)
             {   
//             uwgEndOfServiceRules_rc_0_2                                  
                   PreviousCell =  igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_1");        
                   
                   if (PreviousCell == null)
                   {
                   PreviousCell =  igtbl_getCellById(gridName+"_anc_1");        
                   }
                   var degree=cell.Value;
                   if (degree == null)
                   {
                    cell.setValue(0);
                    degree = 0;
                   }
                   var cell2compare = PreviousCell.getValue();
                   
                   if (cell2compare ==null)
                       cell2compare = 0;
                   
                   //if (cell2compare !=0)
                   //{
                     if(degree < cell2compare )
                      {
                        
                        //-------------------------------0257 MODIFIED-----------------------------------------
                        cell.setValue(cell2compare);
                        //PreviousCell.setValue(degree);  
                        //-------------------------------=============-----------------------------------------
                      }
                    //}
                }
            if (cell.Column.Index== 1)
             {
                    NextCell = igtbl_getCellById(gridName+"rc_"+ RowIndex +"_2");
                    if (NextCell == null)
                    {
                    NextCell = igtbl_getCellById(gridName+"_anc_2");
                    }
                    var Ndegree=cell.Value;
                    if (Ndegree == null)
                    {
                        cell.setValue(0);
                        Ndegree = 0;
                    }
                    var cell4compare ;
                    if (NextCell != null)
                    {
                   cell4compare = NextCell.getValue();
                                    
                    }
                   if (cell4compare ==null)
                   cell4compare =0;
                    
                    //if (cell4compare !=null && ndegree !=null)
                      //{
                       if (cell4compare != 0)
                       {
                        if (Ndegree > cell4compare)
                          {
                           //cell.setValue("");
                           //-------------------------------0257 MODIFIED-----------------------------------------
                            NextCell.setValue(Ndegree);
                          //-------------------------------=============-----------------------------------------
                          
                          }
                       }
             }  
            CheckOnGaps(gridName,1,2);
}

//--------------------//Remove From App_JScript_a.js-------------------------------------------------

function uwgEndOfServiceRules_CellClickHandler(gridName, cellId, button)
       {
	
	        var grid              = igtbl_getGridById(gridName);
	        var cell              = igtbl_getCellById(cellId);
	     
	        var Row               =  igtbl_getRowById(cellId);
	        var RowIndex          =	Row.getIndex();	 
          
            if (cell.Column.Index== 3)
             {   
                var controlvalue = cell.getValue();
	            var queryString="?ControlName="+cellId+"&ControlValue="+controlvalue +"&ControlType=G";
                window.open("frmFormulaDesigner.aspx"+queryString,"_blank","height=318px,width=780px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            }
            else if (cell.Column.Index == 5) {
                var controlvalue = cell.getValue();
                var queryString = "?ControlName=" + cellId + "&ControlValue=" + controlvalue + "&ControlType=G";
                window.open("frmFormulaDesigner.aspx" + queryString, "_blank", "height=318px,width=780px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            }
      }
 