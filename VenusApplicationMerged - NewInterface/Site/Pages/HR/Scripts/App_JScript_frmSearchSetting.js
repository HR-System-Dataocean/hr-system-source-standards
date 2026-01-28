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











