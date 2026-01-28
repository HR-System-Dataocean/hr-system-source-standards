var doSubmitSearchTag = 1;
/* =============================================
   -- Author        : DataOcean
   -- Date Created  : 
   -- Description   : uwgOtherFields_CellClickHandler
   ==============================================*/
function uwgOtherFields_CellClickHandler(gridName, cellId, button)
{
           var webTab = igtab_getTabById("UltraWebTab1");
           if(webTab == null)
             return ;
           
           var grid     = igtbl_getGridById(gridName);
	       var cell     = igtbl_getCellById(cellId);
	       var Row      = igtbl_getRowById(cellId);
	       var ActiveId;

	       ActiveId = igtbl_getGridById("UltraWebTab1xxctl0xuwgNavigation").ActiveCell;
	       if (ActiveId == "")
	           ActiveId = igtbl_getGridById("UltraWebTab1xxctl0xuwgNavigation").ActiveRow;

	       var ObjectId = igtbl_getRowById(ActiveId).getCellFromKey("ID").getValue();
	       var ID    = Row.getCellFromKey("ID").getValue()
	 
	       var winopen=window.open("frmOtherFields.aspx?ObjID=" + ObjectId + "&OtherFieldID=" + ID + "&Mode=E", "_Parent","height=530px,width=740px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	     winopen.focus();

	 }


     function btnAddField() {
         var ActiveId;

         ActiveId = igtbl_getGridById("UltraWebTab1xxctl0xuwgNavigation").ActiveCell;
         if (ActiveId == "")
             ActiveId = igtbl_getGridById("UltraWebTab1xxctl0xuwgNavigation").ActiveRow;

         var ObjectId = igtbl_getRowById(ActiveId).getCellFromKey("ID").getValue();
         
	     var winopen = window.open("frmOtherFields.aspx?ObjID=" + ObjectId + "&OtherFieldID=" + "&Mode=N", "_blank", "height=530px,width=740px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
	     winopen.focus();
	 }
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 26-01-2009
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
   -- Date Created  : 26-01-2009
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