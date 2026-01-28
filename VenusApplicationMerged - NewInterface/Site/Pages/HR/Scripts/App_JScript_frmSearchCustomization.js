// JScript File
function uwgCriteria_ClickCellButtonHandler(gridName,cellId)
{
      var IDCell=igtbl_getCellById(gridName+"_rc_"+cellId.split("_")[2]+"_8");
      var winopen =  window.open("frmColumnsSetting.aspx?ID="+IDCell.getValue()+"&Mode=E&ColumnsType=Criteria" ,"_Parent"+1,"height=250,width=410,resizable=0,menubar=0,toolbar=0,addressbar=0 ,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	  winopen.document.focus();
}


function uwgViewColumns_ClickCellButtonHandler(gridName,cellId)
{
      var IDCell=igtbl_getCellById(gridName+"_rc_"+cellId.split("_")[2]+"_9");
      var winopen =  window.open("frmColumnsSetting.aspx?ID="+IDCell.getValue()+"&Mode=E&ColumnsType=View" ,"_Parent"+1,"height=250,width=410,resizable=0,menubar=0,toolbar=0,addressbar=0 ,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	  winopen.document.focus();
}
function uwgViewColumns_AfterCellUpdateHandler(gridName,cellId,oldval)
{
 var cell = igtbl_getCellById(cellId);
 var Row =  igtbl_getRowById(cellId);
 var grid = igtbl_getGridById(gridName);
// var webTab = igtab_getTabById("UltraWebTab1");
// if(webTab == null)
//    return ;
// var txtTarget = igtab_getElementById("txtTarget",webTab.element);
  if (cell.Column.Index==7)
    {
        if (cell.getValue() == true)
        {    
             //txtTarget.value=Row.getIndex() ;
             for(i=0;i<grid.Rows.length;i++)
	         {
	            var currRow = igtbl_getRowById(gridName+"_r_"+ i);
	            if (currRow.getIndex() == cell.Row.getIndex())
	            {
	                continue ;
	            }
	            var PCell = igtbl_getCellById(gridName+"_rc_"+ currRow.getIndex() +"_7");
	            if (PCell.getValue() == true)
	            {
	                PCell.setValue(false)
	            }
	                   
	         }//End For	         
	    }
     }
      
}


function frmColumnsSettings_Unload()
{
   var TabControl         = window.opener.igtab_getTabById("UltraWebTab1");
   var txtControl        = window.opener.igtab_getElementById("txtReload",TabControl.element);
   txtControl.value="true";
}







var doSubmitSearchTag = 1;

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 22-01-2009
   -- Description   : submint function
   ==============================================*/
function submitSearchFunction()
{
    if(doSubmitSearchTag == 0)
      {return  false;  }
    if(doSubmitSearchTag == 1)
      {return true;    }
}
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 22-01-2009
   -- Description   : uwgNavigation_BeforeRowActivateHandler
   ==============================================*/
function uwgNavigation_BeforeRowActivateHandler(gridName, rowId)
{
    if(rowId == "UltraWebTab1xxctl0xuwgNavigation_flr" )
       {
        doSubmitSearchTag = 0 ;
       }
       else
       {
        doSubmitSearchTag = 1 ;
       }



}





