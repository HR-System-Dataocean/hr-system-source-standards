var CDelete                    = 119; 

function uwgGradesSteps_AfterRowActivateHandler(gridName, id)
{

   
   var Grid               = igtbl_getGridById(gridName);
   var row                = igtbl_getRowById(id)
  if(row.getCellFromKey("ID").getValue()!=null && row.getCellFromKey("ID").getValue() > 0)
  {
      //PageMethods.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(), callback_GetRecordPermissionGradesSteps, OnSucceeded, OnFailed)
      //PageMethods.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(), call_backRecordInfoGradesSteps, OnSucceeded, OnFailed)
  }
  else 
  return false;
  
}
 
function callback_GetRecordPermissionGradesSteps(res)
{

    var tlbMain = igtbar_getToolbarById("TlbMainToolbar")
    
    var saveItem = tlbMain.Items.fromKey("Save");
    var delItem = tlbMain.Items.fromKey("Delete");
    var printItem = tlbMain.Items.fromKey("Print");
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
      return ;
    
    var strFormPermission         = igtab_getElementById("txtFormPermission",webTab.element).value;
    
    saveItem.setEnabled(true);
    delItem.setEnabled(true);
    printItem.setEnabled(true);
    
   if(strFormPermission!="")
   {
    var arrform = strFormPermission.split(",");
    if (arrform[0]!="0")
        saveItem.setEnabled(false);
    if (arrform[1]!="0")
        delItem.setEnabled(false);
    if (arrform[2]!="0")
        printItem.setEnabled(false);
  }


    var arr = res.split(",");
    if (arr[0]!="1")
        saveItem.setEnabled(false);
   if (arr[1]!="1")
        delItem.setEnabled(false);
    if (arr[2]!="1")
        printItem.setEnabled(false);
}






function call_backRecordInfoGradesSteps(res)
{
  
    
    var tlbMainNavigation   = igtbar_getToolbarById("TlbMainNavigation")
    var tlbMaintoolbar      = igtbar_getToolbarById("TlbMainToolbar")
    var delItem             = tlbMaintoolbar.Items.fromKey("Delete")
    var reguserItem = tlbMainNavigation.Items.fromKey("RegUserVal") 
    var regdateItem = tlbMainNavigation.Items.fromKey("RegDateVal")
    var canceldateItem = tlbMainNavigation.Items.fromKey("CancelDateVal")
    
    if(res != null)
    {
    var arr = res.split(",")
    
    reguserItem.Element.innerText = arr[0]
    regdateItem.Element.innerText = arr[1]
    canceldateItem.Element.innerText = arr[2]
    if(arr[2]!= "")
      delItem.setEnabled(false);
//    else
//     delItem.setEnabled(true);
    }
    else
    {
        reguserItem.Element.innerText = ""
     regdateItem.Element.innerText = ""
     canceldateItem.Element.innerText = ""
    }
    
    ig_getWebControlById("UltraWebTab1__ctl0_WebAsyncRefreshPanel1").refresh();


    
        
//return true
    /*
    reguserItem.setText(arr[0])
    regdateItem.setText(arr[1])
    canceldateItem.setText(arr[2])
    */
}



function uwgGradesSteps_BeforeRowDeActivateHandler(gridName, rowId)
{
    var row  = igtbl_getRowById(rowId)
    var grid = igtbl_getGridById(gridName)
    if (isFormChanged()==true)
    {
        var msg = returnDiscardMsg();
      
       
        if (window.confirm(msg))
        {
         IsDataChanged = "F";  
         //uwgGradesSteps_AfterRowActivateHandler(gridName,rowId);
         // __doPostBack(grid,null);
         //window.submit();
         return 0;
        }
       else
       {
      //   return false;
       return 1;          
       }
    }
}

var isFirstTime = true
function frmGradesStepuwgGradesStepsTransactions_AfterCellUpdateHandler2(gridName, cellId){
	
	if (!isFirstTime)
	    return
	var cell = igtbl_getCellById(cellId)
	var prevCell 
	
	var lang                = new String(); 
    var msg;
    lang                    = GetCookie("Lang");
   
    if (lang.indexOf("ar")>-1)
     {
        msg =" Amount must between";//"«·ﬁÌ„… ÌÃ» √‰  ﬂÊ‰ »Ì‰ "
     }
    else
     {
        msg =" Amount must between"; 
     }
        
        
	if (cell.Column.Index == 2)
	{
	    prevCell = cell.getPrevCell()
	    var valStr = new String()
	    valStr = prevCell.Element.outerText
	    
	    //var arr = valStr.split('(')[1].split(')')[0].split(',')
	    var arr = valStr.split('#')[1].split('*')
	    
	    arr[0] = arr[0].replace(",","")
	    arr[1] = arr[1].replace(",","")
	    
	    var min = ConvertToNumber(arr[0])
	    var max = ConvertToNumber(arr[1])
	    
	    if ( ConvertToNumber( cell.getValue())< min || ConvertToNumber( cell.getValue())> max )
	    {
	        msg =msg + " " + min + " , " + max ; 
	        isFirstTime = false
	        if (ConvertToNumber( cell.getValue())< min)
	            cell.setValue(min)
	        else 
	            cell.setValue(max)
	        isFirstTime = true
	        alert(msg)
	    }
	    
	    
	}
	
	
}




//    ========================================================================
//    Screen         :  frmGradesSteps
//    Project        :  Venus V.
//    Module         :  PayRoll
//    Description    :  focus to grid.
//    Developer      :  [0260]
//    Date Created   :  24-04-2008
//      
//=========================== [Start] 

function txtArbNameLostFocusToGrid()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
      return ;
    
    var ctrlGridid          = igtab_getElementById("uwgGradesStepsTransactions",webTab.element).id;
    var grid                = igtbl_getGridById(ctrlGridid);
    
    var nextEditCell;
    if (grid.Rows.rows.length == 0)
         nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgGradesStepsTransactions" + "_anc_1");       
    else
         nextEditCell = igtbl_getCellById("UltraWebTab1xxctl0xuwgGradesStepsTransactions" + "_rc_0_1");       
    if(nextEditCell!=null)
      {
        nextEditCell.activate();
        nextEditCell.beginEdit();
      }   
}
//======[End]


function frmGradesStepuwgGradesStepsTransactions_EditKeyDownHandler(gridName, cellId, key)
{   var i = 0;
    if (key == CDelete)
    {
        var cell = igtbl_getCellById(cellId);
        var Row = cell.getRow()
        var prvRow = Row.getPrevRow()
        var prvCell
         
        
        for(i=0 ; i< Row.cells.length ; i++)
        {
            if (Row.cells[i] != null)
                Row.cells[i].setValue(null);
        }
        Row.deleteRow()
        
        if (prvRow != null)
            prvCell = prvRow.cells[1]
        
        if (prvCell != null)
        {
            prvCell.activate()
            prvCell.beginEdit()
        }
    }
}

function OnSucceeded(result, userContext, methodName) {

}
function OnFailed(error) {
    //alert(error.get_message());
}