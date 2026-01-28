function uwgOtherFields_AfterRowActivateHandlerNew(gridName, id)
{
   var Grid               = igtbl_getGridById(gridName);
   var row                = igtbl_getRowById(id)
   var txtOtherFieldID    = window.document.getElementById("txtOtherFieldID")
  if(row.getCellFromKey("ID").getValue()!=null && row.getCellFromKey("ID").getValue() > 0)
  { 
  
    txtOtherFieldID.value = row.getCellFromKey("ID").getValue();
    // Interfaces_frmOtherFields.GetRecordPermissionAjax(row.getCellFromKey("ID").getValue(),callback_GetRecordPermissionOtherFields)
//  Interfaces_frmOtherFields.GetRecordInfoAjax(row.getCellFromKey("ID").getValue(),callback_GetRecordInfoOtherFields)
//  
  }  
  else 
  return false;
  }
  
  
function callback_GetRecordPermissionOtherFields(res)
{
 var tlbMain = igtbar_getToolbarById("TlbMainToolbar")
    var saveItem = tlbMain.Items.fromKey("Save");
    var delItem = tlbMain.Items.fromKey("Delete");
    //var printItem = tlbMain.Items.fromKey("Print");
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
      return ;
    
    var strFormPermission         = igtab_getElementById("txtFormPermission",webTab.element).value;

    saveItem.setEnabled(true);
    delItem.setEnabled(true);
   // printItem.setEnabled(true);
    
   if(strFormPermission!="")
   {
    var arrform = strFormPermission.split(",");
    if (arrform[0]!="0")
        saveItem.setEnabled(false);
    if (arrform[1]!="0")
        delItem.setEnabled(false);
  //  if (arrform[2]!="0")
 //       printItem.setEnabled(false);
   }
        
  
    var arr = res.value.split(",");
    if (arr[0]!="1")
        saveItem.setEnabled(false);
   if (arr[1]!="1")
        delItem.setEnabled(false);
    //if (arr[2]!="1")
      //  printItem.setEnabled(false);
        
        if(Confirm==true)
         ig_getWebControlById("UltraWebTab1__ctl0_WebAsyncRefreshPanel").refresh();  
        
  } 
  
  
 function uwgOtherFields_BeforeRowActivateHandlerNew(gridName, rowId)
{
    var row  = igtbl_getRowById(rowId)
    var grid = igtbl_getGridById(gridName)
    Confirm=false;
    if (isFormChanged()==true)
    {
        var msg = returnDiscardMsg();
        Confirm =true; 
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


//===========Screen     : OtherFields
//===========Developer  :[0261]
//===========Date       :[20-04-2008]
function Change_Data_Lenght()
{
          var webTab = igtab_getTabById("UltraWebTab1");
          if(webTab == null)
             return;
          var ControlDataTypes  = igtab_getElementById("ddlDataTypes", webTab.element);
         
          var  ddlDataTypes   =    ControlDataTypes.value;
          var  txtDataLength  =    igedit_getById("UltraWebTab1__ctl0_txtDataLength");
          //Private arrDataLength() As Integer = {8000, 8, 1, 8}
          //Private arrDataTypes() As String = {"Charachters", "Numeric", "Boolean", "DateTime"}
          if(ddlDataTypes=="Charachters")
          {
          txtDataLength.setValue(8000);
          }
          else if(ddlDataTypes=="Numeric")
          {
          txtDataLength.setValue(8);
          }
          else if(ddlDataTypes=="Boolean")
          {
          txtDataLength.setValue(1);
          }
          else if(ddlDataTypes=="DateTime")
          {
          txtDataLength.setValue(8);
          }
        

}


//===========Screen     : OtherFields
//===========Developer  :[0261]
//===========Date       :[20-04-2008]
function Set_MAx_Value()
{
       var webTab = igtab_getTabById("UltraWebTab1");
          if(webTab == null)
             return;
          var ControlDataTypes  = igtab_getElementById("ddlDataTypes", webTab.element);
         
          var  ddlDataTypes   =    ControlDataTypes.value;
          var  txtDataLength  =    igedit_getById("UltraWebTab1__ctl0_txtDataLength");
          //Private arrDataLength() As Integer = {8000, 8, 1, 8}
          //Private arrDataTypes() As String = {"Charachters", "Numeric", "Boolean", "DateTime"}
          if(ddlDataTypes=="Charachters")
          {
          txtDataLength.setMaxValue(8000);
          }
          else if(ddlDataTypes=="Numeric")
          {
         txtDataLength.setMaxValue(8);
          }
          else if(ddlDataTypes=="Boolean")
          {
         txtDataLength.setMaxValue(1);
          }
          else if(ddlDataTypes=="DateTime")
          {
          txtDataLength.setMaxValue(8);
          }
}




