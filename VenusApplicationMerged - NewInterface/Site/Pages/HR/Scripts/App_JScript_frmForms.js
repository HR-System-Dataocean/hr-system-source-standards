var FormsSubmitFlag=0;
var blnAfetrUpdate  = true ;
var blnBeforeChange = true ;
var blnNext         = true ;
var cEnter          = 13 ;
var oActiveCell     = '' ;
//----------------------------------------
function frmForms_Do_Submit_Flag()
{
    if(FormsSubmitFlag==0)
    {
     return true;
    }
      if(FormsSubmitFlag==1)
      {
      return false;
      }
}
//----------------------------------------
function frmForms_MainToolBarSave_Other_Fields()
{
    var ddlModule    = window.document.getElementById("UltraWebTab1__ctl0_ddlModule"); 
    var tlbControl   = igtbar_getToolbarById("TlbMainToolbar");
    FormsSubmitFlag  = 0 ;
    TlbMainToolbarNotNavigation_Click();  
            if (tlbControl.Items.fromKey("Save").Selected ==true)
            {
                    if ( ddlModule.value == 0 || ddlModule.value ==null)
                    {
                    FormsSubmitFlag  = 1 ;
                    alert("Please Choose Moduele Before Saving ");
                    }
                    else
                    {
                    SaveOtherFieldsData()     
                    }
            }  
	    	  
} 
//----------------------------------------
function txtWidthLostFocus2()
{
 var grid                = igtbl_getGridById("UltraWebTab1xxctl0xuwgFormParameters");
 var nextEditCell;     
    if(grid.Rows.rows.length == 0)
    {
        nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgFormParameters_anc_2");       
        oActiveCell  = "UltraWebTab1xxctl0xuwgFormParameters_anc_2" ;
    }
    else
    {
        nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgFormParameters_rc_0_2");       
        oActiveCell  = "UltraWebTab1xxctl0xuwgFormParameters_rc_0_2" ;
    }
    if(nextEditCell != null)
    {
        var Row      = igtbl_getRowById(nextEditCell.Id);
        Row.activate;
        Row.select ;
        nextEditCell.activate();
        nextEditCell.select;
        nextEditCell.beginEdit(); 
        return 1 ;
    }
}
//----------------------------------------
function uwgFormsParameters_AfterCellUpdateHandler(gridName, cellId)
{
    if(!blnAfetrUpdate)
    return ;
    if(cellId=="") 
    return;     
	 var ActiveCell ;
        ActiveCell = cellId;
	 var lang   = new String(); 
     lang       = GetCookie("Lang");
     var retMsg = "";
     if (lang.indexOf("ar")>-1)
     {
        retMsg  = "لايسمح بإدخال الاسم أكثر من مرة"
     }
     else
     {
        retMsg  = "Not allowed to enter Parameter Name more than once "
     }
     var grid                = igtbl_getGridById(gridName);
     var cell                = igtbl_getCellById(cellId);
     var Row                 = igtbl_getRowById(cellId);
     var rowIndex            = Row.getIndex();
     var groupCell;
     var currgroupCell;     
    if (cell.Column.Index == 2) 
       {	
            try
            {
                currgroupCell = Row.getCellFromKey("ParamName");
                if (currgroupCell == undefined || currgroupCell == null)
                {
                    currgroupCell = igtbl_getCellById(gridName+"_rc_"+ rowIndex +"_2");
                }
            }
            catch(e)
            {
                currgroupCell = igtbl_getCellById(gridName+"_rc_"+ rowIndex +"_2");
            }
           if(currgroupCell.getValue() == null) return ;
            for(i=0;i<grid.Rows.length;i++)
            {
                var currRow = grid.Rows.rows[i];
                    try
                    {
                       groupCell = currRow.cells[2];
                       if(groupCell == undefined || groupCell == null)
                        {
                          groupCell = igtbl_getCellById(gridName+"_rc_"+ i +"_2");
                        }
                    }
                    catch (e)
                    {
                        groupCell = igtbl_getCellById(gridName+"_rc_"+ i +"_2");
                        if(groupCell == null)
                        {
                            continue;
                        }
                    }
             if(currgroupCell.getValue() == groupCell.getValue() && rowIndex != i)
                {
                    blnAfetrUpdate  = false
                    alert(retMsg);
                    Row.activate;
                    Row.select ;
                    currgroupCell.setValue("");
                    currgroupCell.activate();
                    currgroupCell.select;
                    currgroupCell.beginEdit();
                    blnAfetrUpdate  = true ;                    
                    return 1;                     
                }
            }
     }
}
//----------------------------------------
function uwgFormsParameters_BeforeCellChangeHandler(gridName, cellId)
{ 
    if(!blnBeforeChange)
        return ;
    if(cellId == null)
        return ; 
    var cell              = igtbl_getCellById(cellId);
    var Row               = igtbl_getRowById(cellId);
    var grid              = igtbl_getGridById(gridName);
    var ParamNameCell        = Row.getCellFromKey("ParamName");
    var ParamNameCellValue   = Row.getCellFromKey("ParamName").getValue() ;
    var ParamValueCell       = Row.getCellFromKey("ParamValue")
    var ParamValueCellValue  = Row.getCellFromKey("ParamValue").getValue();    
    if(cell.Column.Index == 3)
      {
        if(ParamNameCellValue == null || ParamNameCellValue == undefined )
          {
            blnBeforeChange = false
            Row.activate;
            Row.select ;
            Row.getCellFromKey("ParamName").activate();
            Row.getCellFromKey("ParamName").select;
            Row.getCellFromKey("ParamName").beginEdit();
            blnBeforeChange = true
            return  1 ; 
          }
      }  
}
//----------------------------------------
function GetNextActiveCellFunction(cell)
{
    var nextcell ;
        nextcell = cell.getNextTabCell();
   if(nextcell == null) 
    {
      var grid  = igtbl_getGridById("UltraWebTab1xxctl0xuwgFormParameters");
      var Row   = igtbl_getRowById(cell.Id);       
      if(Row.Id == grid.Rows.getLastRowId() )
        {     
          var NewRow  = igtbl_addNew('UltraWebTab1xxctl0xuwgFormParameters',0,true,true)
          return NewRow.getCellFromKey("ParamName") ;
        }
        else return cell ;        
    }
    if (nextcell.isEditable() == false)
    {
        return GetNextActiveCell(nextcell)
    }
    else 
    {
//        if (cell.Column.Index == 3)
//        {
//            var Row                = igtbl_getRowById(cell.Id);
//            var ParamNameCell      = Row.getCellFromKey("ParamName");
//            var ParamNameCellValue = Row.getCellFromKey("ParamName").getValue() ;
//                if (ParamNameCellValue == null || ParamNameCellValue == undefined)
//                return  ParamNameCell
//                else return nextcell  ;
//        }
        //else 
        return nextcell ;      
    }
}
//----------------------------------------
function uwgFormsParameters_AfterEnterEditModeHandler(gridName, cellId)
{
    var arrCellId   = cellId.split('_');
    var NameCell    = igtbl_getCellById(arrCellId[0]+'_'+arrCellId[1]+"_"+arrCellId[2]+"_2")
    var ValueCell   = igtbl_getCellById(arrCellId[0]+'_'+arrCellId[1]+"_"+arrCellId[2]+"_3")
   if(NameCell.getValue() == null || NameCell.getValue() == undefined)
    {
        oActiveCell =  cellId
        if(cellId != "UltraWebTab1xxctl0xuwgFormParameters_rc_" + arrCellId[2] + "_2")  
        {
            var Row  = igtbl_getRowById(NameCell.Id);
            Row.activate;
            Row.select ;
            NameCell.activate();
            NameCell.select;
            NameCell.beginEdit(); 
            return 1 ;
        }        
    }
   else
    {
       oActiveCell = cellId
    }   
}
//----------------------------------------
function WteNames_KeyDown(oEdit, keyCode, oEvent)
{
	switch (keyCode)
    {
        case (cEnter) : 
        { 
            var cell     = igtbl_getCellById(oActiveCell) 
            var nextcell = GetNextActiveCellFunction(cell)
            var Row      = igtbl_getRowById(nextcell.Id);
            Row.activate;
            Row.select ;
            nextcell.activate();
            nextcell.select;
            nextcell.beginEdit(); 
            break;
        }
        case(119) :
        {
            var Row  = igtbl_getRowById(oActiveCell);
            var e    = window.event;
            if (Row != null )
            {	
                Row.deleteRow();
                Row.remove();
                txtWidthLostFocus2();
            }
        }
    }		
}
//----------------------------------------
function wteValues_KeyDown(oEdit, keyCode, oEvent)
{
	switch (keyCode)
    {
        case (cEnter) : 
        { 
            var cell     = igtbl_getCellById(oActiveCell) 
            var nextcell = GetNextActiveCellFunction(cell)
            var Row      = igtbl_getRowById(nextcell.Id);
            Row.activate;
            Row.select ;
            nextcell.activate();
            nextcell.select;
            nextcell.beginEdit(); 
            break;
        }
        case(119) :
        {
            var Row  = igtbl_getRowById(oActiveCell);
            var e    = window.event;	
            if(Row != null)
            {
                Row.deleteRow();
                Row.remove();
                txtWidthLostFocus2();
            }
        }
    }		
}
//----------------------------------------