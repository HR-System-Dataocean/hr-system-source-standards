
//============================= Integrate Data Update Schedule with Arabic Events [Start]
/*
function LoadDataUpdateSchedulesForArabicText(e,formName,controlName,recordID)
{
    e=window.event;
    var code =e.keyCode;
    
    if( e.ctrlKey && code==21 )
    {
        var queryString="?FormName="+formName+"&ControlName="+controlName+"&RecordID="+recordID;
        window.open("frmDataUpdateSchedule.aspx"+queryString,"_Parent","height=368px,width=550px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
        //window.open("frmDataUpdateSchedule.aspx"+queryString)
    } 
    
   ArabicKeyPress(e);
}
*/
//============================= Integrate Data Update Schedule with Arabic Events [ End ]


var isiri2901_lang = 1;        // 1: Arabic, 0: English
var isiri2901_nativelang = 0;  // 1: Arabic, 0: English

// Arabic keyboard map based on ISIRI-2901

var isirikey = [
0x0020,0x0637,0x0637,0x0637,0x0637,0x0637,0x0637,0x0637,0x0029,0x0028,
0x0632,0x003D,0x0648,0x002D,0x0632,0x0638,0x0660,0x0661,0x0662,0x0663,
0x0664,0x0665,0x0666,0x0667,0x0668,0x0669,0x0632,0x0643,0x002C,0x002B,
0x002E,0x061F,0x0664,0x0665,0x0666,0x0667,0x0668,0x0669,0x0632,0x0638,
0x0623,0x0661,0x0640,0x0663,0x0664,0x0665,0x0666,0x0667,0x0668,0x0669,
0x0632,0x0638,0x0660,0x0661,0x0662,0x0663,0x0664,0x0665,0x062C,0x062C,
0x0630,0x062F,0x0632,0x0638,0x0634,0x0634,0x0624,0x0624,0x064A,0x062B,
0x0628,0x0644,0x0627,0x0647,0x062A,0x0646,0x0645,0x0629,0x0649,0x062E,
0x062D,0x0636,0x0642,0x0633,0x0641,0x0639,0x0631,0x0635,0x0621,0x063A,
0x0626
];

function ArabicKeyDown(e)
{
  if (window.event)
    e = window.event;
  if (e.ctrlKey && e.altKey) 
  {
    if (isiri2901_lang == 0)
      setArabic();
    else
      setEnglish();
    try 
     {
        e.preventDefault();
     } catch (err) 
     { }
    return false;
  }
  return true;
}

var pk_test_ev;

function ArabicKeyPress(e)
{
  var key;
  var obj;


  if (window.event) {
    e = window.event;
    obj = e.srcElement;
    key = e.keyCode;
    //alert(key)
  } else {
    obj = e.target;
    key = e.charCode;
  }

  if (e.bubbles==false)
    return true;

 //  Change to English, if user is using an OS non-English keyboard
 //================================================================
 
  if (key >= 0x00FF) {
    isiri2901_nativelang = 1;
    setArabic();
  } else
    if (isiri2901_nativelang == 1) {
      isiri2901_nativelang = 0;
      setEnglish();
    }


  // Avoid processing if control or higher than ASCII
  // Or ctrl or alt is pressed.
  if (key < 0x0020 || key >= 0x007F || e.ctrlKey || e.altKey || e.metaKey)
    return true;

  if (isiri2901_lang == 1) { //If Persian

    // rewrite key
    var newkey;
    if (key == 0x0020 && e.shiftKey) // Shift-space -> ZWNJ
      newkey = 0x200C;
    else
    {
             //alert(key)
             newkey = isirikey[key - 0x0020];
    }
     
    
    if (newkey == key) 
      return true;
    

    try {
      // Gecko 
      var new_event=document.createEvent("KeyEvents");
      new_event.initKeyEvent("keypress", false, true, document.defaultView, false, false, false, false, 0, newkey);
      obj.dispatchEvent(new_event);
      e.preventDefault();
    } catch (err) {
    try {
      // Windows
      e.keyCode = newkey;
    } catch (err) {
    try {
      
      // Try inserting at cursor position
      pnhMozStringInsert(obj, String.fromCharCode(newkey));
      e.preventDefault();
    } catch (err) {
      // Everything else, simply add to the end of buffer
      obj.value += String.fromCharCode(newkey);
      e.preventDefault();
    }}}
  }
  return true;
}

function setArabic (obj, quiet)
{
  isiri2901_lang = 1;
  if (obj) {
    obj.style.textAlign = "right";
    obj.style.direction = "rtl";
    //obj.focus();
  }
  if (!quiet)
    window.defaultStatus = "Data Ocean 2008";
}


function setEnglish (obj, quiet)
{
  isiri2901_lang = 0;
  if (obj) {
    obj.style.textAlign = "left";
    obj.style.direction = "ltr";
   //obj.focus();
  }
  if (!quiet)
    window.defaultStatus = "Data Ocean 2008";
}


function toggleDir (obj, quiet) {
  var isrtl = 0;
  if (obj)
    isrtl = obj.style.direction != 'ltr';
  else
    isrtl = isiri2901_lang;
  if (isrtl)
   setEnglish(obj, quiet);
  else
   setArabic(obj, quiet);
}
// Inserts a string at cursor
function pnhMozStringInsert(elt, newtext) {
	var posStart = elt.selectionStart;
	var posEnd = elt.selectionEnd;
	var scrollTop = elt.scrollTop;
	var scrollLeft = elt.scrollLeft;
	
        elt.value = elt.value.slice(0,posStart)+newtext+elt.value.slice(posEnd);
        var newpos = posStart+newtext.length;
        elt.selectionStart = newpos;
        elt.selectionEnd = newpos;	
        elt.scrollTop = scrollTop;
        elt.scrollLeft = scrollLeft;
        elt.focus();
}

function DoRefreshBack()
{
  //var intW = window.screen.availWidth;
  //var intH = window.screen.availHeight;
  //window.opener.document.forms[0].submit();
  //window.document.form[0].moveTo(0,0);
  //window.moveTo(0,0);
  //window.resizeTo(intW,intH);
  window.focus();
  window.document.focus();

}

function AllowAlphaNumericOnly(e) {
    
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    else if (event) keycode = event.keyCode;
    else if (e) keycode = e.which;
    else return true;
    if ((keycode >= 48 && keycode <= 57) || (keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122)) {
        return true;
    }
    else {
        return false;
    }
    return true; 
}


function CheckNumeric(e) {
   
    if (window.event) // IE 
    {
        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
            event.returnValue = false;
            return false;

        }

    }
    else { // Fire Fox
        if ((e.which < 48 || e.which > 57) & e.which != 8) {
            e.preventDefault();
            return false;

        }
    }
}


// General Functions ==================================================================================
//====================================================================================================


           function  GetValue(Expression ,Find) 
            {
                var StrString;
                var IntLocation;
                var DblLenght;
                var StrRightPart;
                var StrFinalResult;
                var IntNextSeparator;

                
                StrString = Find
                IntLocation = Expression.indexOf(StrString)
                DblLenght = StrString.length

                if (IntLocation < 0)
                {
                    return ""
                }

                StrRightPart = Expression.substring(IntLocation + DblLenght + 1)
                IntNextSeparator = StrRightPart.indexOf(';')
                if (IntNextSeparator > 0) 
                {
                    StrRightPart = StrRightPart.substring(0, IntNextSeparator)
                }

                StrFinalResult = StrRightPart
                
                return StrFinalResult
            }
            
            
//==========================================================================

    function uwgBenetitTemplet_BeforeCellUpdateHandler(gridName, cellId, value)
    {
      //  var Row               =  igtbl_getRowById(cellId)
      //  var RowIndex          =	Row.getIndex()	 
      //  var PreviousRowIndex  
       
      //  if (RowIndex > 0)
       //  {
      //    PreviousRowIndex = RowIndex - 1
      //    var Cell            = igtbl_getCellById("UltraWebTab1xxctl0xuwgData_rc_" + RowIndex + "_2")
      //    var PreviousCell    = igtbl_getCellById("UltraWebTab1xxctl0xuwgData_rc_" + PreviousRowIndex + "_2")
      //    if (value == PreviousCell.getValue())
      //       {
      //        alert("Wrong value")
       //       Cell.setValue("")
       //       Cell.setEditable()
       //       }
       //   }
      
    }
    //==========================================================================
    //==========================================================================
//Name : [MAE]Mah Abdel-aziz 
//Date : 19/7/2007
//Description :switch between two case of measure type(degrees or absolute value)
//Module : Venus.Evaluation 
//Screen : frmEvaluationTypeElements 

    function DoSelect()
            {
             //alert("Working")
//             var rbtn           =window.document.getElementById("rbtnSelectedDateType")
             
                var txtdegree      =window.document.getElementById("txtDegree");
                var lbabs          =window.document.getElementById("lbAbsolute");
             
//                var txtdegree      =window.document.form1.txtDegree;
//                var lbabs          =window.document.form1.lbAbsolute;
//            
//                var txtdegree       =igtbar_getItemById("txtDegree");
//                var lbabs          =igtbar_getItemById("lbAbsolute");
           
                if (window.document.form1.rbtnSelectedDateType[0].checked==false)
                {
                    txtdegree.disabled=true;
                    lbabs.disabled=false;
                    //window.document.form1.txtDegree.disabled=true
                }
                else
                {
                   txtdegree.disabled=false;
                   lbabs.disabled=true;
                   //window.document.form1.txtDegree.disabled=false
                }
              }
             
            
            
//===========================================================================            
 
//Name : [MAE]Mah AbdelAziz
//Date : 25/7/2007
//Description :Validate on input degree <= measure value if found
//             otherwise record has absolute value validate on input degree = (0 or 1)
//Module : Venus.Evaluation 
//Screen : frmEmployeeEvaluation
           
     
//     var currRow;
//     
//      function uwgTransactionDetails_AfterRowActivateHandler(gridName, rowId){
//	currRow=igtbl_getRowById(rowId)
//}
     
            


    function uwgTransactionDetails_AfterCellUpdateHandler(gridName, cellId){
	 var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	         
            if (cell.Column.Index== 3 && cell.Band.Index== 1)//&& cell.Row.getIndex() == grid.Rows.length-1)
             {
//                    var CurrentCell =igtbl_addNew(gridName,0,true,true).cells[0]
//                    grid.setActiveCell(CurrentCell,true)
//                    CurrentCell.beginEdit()
               
                    //var measurValue=grid.Rows[cell.Row.getIndex()].Cell[2].Value;
                    //var measurValue=cell.Row.Cells[2].Value;
                    
                    
                    var currRow=igtbl_getRowById(cell.Row.Id);
                    var prvCell=igtbl_getCellById (cell.getPrevCell().Id);
                    var measurValue=prvCell.getValue();
                    
                    var degree=cell.Value;
                    
                    if(measurValue==null)
                    {
                        if(degree>1)
                        {
                         cell.setValue("");
                         //alert("Evaluation Degree must be absoulte (0 or 1)");
                         //return;
                        }
                    }
                    else
                    {
                        if(degree>measurValue ||degree<0)
                        {
                         cell.setValue("");
                         //alert("Evaluation Degree greater than Measure Value");
                         //return;
                        }
                    }
              }		         
}



//Name : [MAE]Mah AbdelAziz
//Date : 26/7/2007
//Description : Validate on new input percent is between 1 and 100
//Module : Venus.Evaluation 
//Screen : frmEvaluationMainElements

function uwgEvaluations_AfterCellUpdateHandler(gridName, cellId){

           var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
            if (cell.Column.Index== 3)
             {                                     
                                      
                    var degree=cell.Value;
                    if(degree <=0 || degree>100)
                    {
                        cell.setValue("");
                    }
              }
}

function txtPercent_Changed()
{
 //alert("HIIIIIIIIi");

  var txtPercent =window.document.getElementById("txtPercent");
  
  var val =txtPercent.value;
  if(val <=0 || val >100)
  {
    txtPercent.value="";
  }
  
  
}


function txtShift4_Changed()
{
  
  var txtshift =window.document.getElementById("txtshift4");
  var val =txtshift4.value;
  if(val <=0 || val >24.59)
  {
    txtshift4.value="";
  }
   
}


	
	        function MainSearch_OpenWin(Target,ModuleID)
			    {
				    var winopen = window.open("frmSearchScreen.aspx?TargetControl="+Target+"&SearchID="+ModuleID,"_Parent"+ModuleID,"height=525,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
				    winopen.document.focus();
			    }
	
//==================================================== Search [End]
///////////// Other Fields Module Functions 
function OpenOtherFieldsForm(ObjectID , TableName)
{
				    var winopen = window.open("frmOtherFields.aspx?ObjectID= "+ObjectID+"&TableName="+TableName,"_Parent","height=376,width=467,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,titlebar=0,dependent=No");		
				    //var winopen = window.open("frmOtherFields.aspx",_Parent","height=376,width=467,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
				    winopen.focus();
}

//===========================================================
function DisplayOtherFields(TableName,ObjectID,RecordID)
{
                var winopen = window.open("frmOtherFieldsDynamic.aspx?ObjectID="+ObjectID+"&TableName="+TableName+"&RecordID="+RecordID , "_WindowName","height=450,width=500,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
		        winopen.focus();
  	        
}

//============================================================
function TlbMainToolbar_Click(oToolbar, oButton, oEvent){
	//Add code to handle your event here.
	if (oButton.Key  = "Save")
	SaveOtherFieldsData();
}

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

// AGL 
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
             
           
function ConvertToNumber(objValue)
    {
        var tmp= objValue-1;
	    tmp = tmp+1;
	    return tmp;
    }//End of function ConvertToNumber
    
    
//=================================================
//Description: Check that no gaps in intervals
//=================================================  
function CheckOnGaps(gridName,fromColInd,toColIndex)
{
    var grid = igtbl_getGridById(gridName);
    //============ Validate on Gaps [Start]
             
    var length                  = grid.Rows.length;
    var intCnt;
    var prevToVal;
    var fromCell;
    var toCell;
    var fromVal;
    var toVal;
    for(intCnt=0;intCnt<length;intCnt++)
       {
         fromCell           =   grid.Rows.rows[intCnt].cells[fromColInd];
         if (fromCell == "undefined" || fromCell == null)
	     {
	         fromCell = igtbl_getCellById(gridName+"_rc_"+ intCnt+"_"+fromColInd);
	     }
	                
	                
	     toCell             =   grid.Rows.rows[intCnt].cells[toColIndex];
	     if (toCell == "undefined" || toCell == null)
	     {
	        toCell = igtbl_getCellById(gridName+"_rc_"+ intCnt+"_"+toColIndex);
	     }
	     
	     fromVal            = fromCell.getValue();
	     toVal              = toCell.getValue();
	     
	     if (fromVal == null)
	     {
	        fromVal = 0;
	        fromCell.setValue(0);
	     }
	     if (toVal  == null)
	     {
	        toVal  = 0;
	        toCell.setValue(0);
	     }
	     
	     
	     if (intCnt != 0)
	     {
	        if (fromVal <= prevToVal || ((fromVal-prevToVal)>1)  )
	        {
	            fromCell.setValue(prevToVal+1);
	            fromVal = prevToVal+1;
	        }
	        if (toVal < fromVal)
	        {
	            toCell.setValue(fromVal);
	            toVal = fromVal;
	        }
	     }
	     
	     
	     prevToVal = toVal;  
	     
      }//End For
             
             
     //============ Validate on Gaps [End  ]
}


function UwgSearchEmployees_InDetails(gridName, cellId){
  var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       var Row               =  igtbl_getRowById(cellId);
	       var PreviousCell ;
	       var NextCell ;
	       var RowIndex          =	Row.getIndex();	 
            if (cell.Column.Index== 4)
            {
            
            }
	//Add code to handle your event here.
}

function CHeckDiff()
    {
        if (window.confirm("The Data Have been changed Are you sure you want to discard changes?"))
    {
        window.close()
    }
    }
    

//  var winopen = window.open("frmOtherFieldsDynamic.aspx?ObjectID="+ObjectID+"&TableName="+TableName+"&RecordID="+RecordID , "_WindowName","height=450,width=500,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
//				    winopen.document.focus();
// Created by [AGL] & [TMD]
function UwgSearchEmployees_CellClickHandler(gridName, cellId, button)
{
           
     
}

function UwgSearchEmployeesPrepare_DblClickHandler(gridName, cellId)
{
           var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       var Row  = igtbl_getRowById(cellId);
	       var EmpID = Row.getCell(6).getValue()
	       var Control =window.document.getElementById("DdlPeriods");
	       var Fisical = Control.value;
	       if (EmpID != null && cell.Column.Index != 7)
	       {
	           if (Row.getCell(4).getValue() == "1") {
	               var winOpend = window.open("frmEmployeesMonthlyTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=E", "_Parent","height=585px,width=824px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	               winOpend.focus();
	           }
	           else {
	               var winOpend = window.open("frmEmployeesMonthlyTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=N", "_Parent", "height=585px,width=824px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	               winOpend.focus();
	           }
	       }
	           
}


//Created By [AGL] 09-09-2007 
function UwgSearchEmployeesVac_CellClickHandler(gridName, cellId, button){
	       var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       var Row  = igtbl_getRowById(cellId);
	       var EmpID = Row.getCell(7).getValue();
	       // 
	       var ContractDays = Row.getCell(5).getValue();
	       var RemainingDays = Row.getCell(6).getValue();
	       //
	       
	       var Control =window.document.getElementById("DdlPeriods");
	       var Fisical = Control.value;
	       //var ID    = Row.getCell(5).getValue()
	       if (Row.getCell(4).getValue()=="True")
	       
	       //Fisical Period ID 
	       
	       {
	        window.open("frmEmployeesVacationTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=E&ContractDays=" + ContractDays + "&RemainingDays=" +RemainingDays, "_Parent","height=550,width=715,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	       }
	       else
	       {
	       window.open("frmEmployeesVacationTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=N&ContractDays=" + ContractDays + "&RemainingDays=" +RemainingDays, "_Parent","height=550,width=715,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	       }
     
}




//by :[MAE] Mah Abdel-aziz
//Date :   1-8-2007
//Screen :frmFascalYears.aspx

function uwgFiscalYearsPeriods_AfterCellUpdateHandler(gridName, cellId){
	       var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       
	       var fromDate =new Date
	       var toDate   =new Date
            if (cell.Column.Index== 3)
             {
                    var currRow=igtbl_getRowById(cell.Row.Id);
                    var prvCell=igtbl_getCellById (cell.getPrevCell().Id);
                    var measurValue=prvCell.getValue();
                    toDate= degree=cell.Value;
                    fromDate=measurValue
                   if(toDate<fromDate)
                    {
                       cell.setValue(fromDate)
                    }
             }
}
/////////////////////////////////////////////////////////////////////////
//Tamer Items Screen    
function txtReceivedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent){
	var ReturnDate=igdrp_getComboById('txtReturnedDate')
    var RecievDate=oDateChooser.getValue()
    if (RecievDate>ReturnDate.getValue())
    {
        //alert("Invalid Return Date!")
        ReturnDate.setValue('')
        oDateChooser.focus
    }
}
//Tamer Items Screen
function txtReceivedDate_ValueChanged(oDateChooser, newValue, oEvent){
	var ReturnDate=igdrp_getComboById('txtReturnedDate')
    var RecievDate=oDateChooser.getValue()
    if (RecievDate>ReturnDate.getValue())
    {
        //alert("Invalid Return Date!")
        ReturnDate.setValue('')
        oDateChooser.focus
    }
}

//Tamer Items Screen
function txtReturnedDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent){
	var RecievDate=igdrp_getComboById('txtReceivedDate')
    var ReturnDate=oDateChooser.getValue()
    if (RecievDate.getValue()>ReturnDate)
    {
        alert("Invalid Return Date!")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}
//Tamer Items Screen
function txtReturnedDate_ValueChanged(oDateChooser, newValue, oEvent){
	var RecievDate=igdrp_getComboById('txtReceivedDate')
    var ReturnDate=oDateChooser.getValue()
    if (RecievDate.getValue()>ReturnDate)
    {
        alert("Invalid Return Date!")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}
    
//Tamer Contract Screen
function txtEndDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent)
{
    var EndDate=oDateChooser.getValue()
    var StartDate=igdrp_getComboById('txtStartDate')
    if (StartDate.getValue()>EndDate)
    {
        alert("Envalid End Date")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}
//Tamer Contract Screen
function txtEndDate_ValueChanged(oDateChooser, newValue, oEvent)
{
    var EndDate=oDateChooser.getValue();
    var StartDate=igdrp_getComboById('txtStartDate');
    //-------------------------------0257 MODIFIED-----------------------------------------
   //if (StartDate.getValue()>=EndDate)
   //-------------------------------=============-----------------------------------------
    if (StartDate.getValue()>=EndDate)
    {
        alert("Envalid End Date");
        oDateChooser.setValue('');
        oDateChooser.focus
    }
    
}
//Tamer Contract Screen
function txtStartDate_AfterCloseUp(oDateChooser, dropDownPanel, oEvent)
{
    var EndDate=igdrp_getComboById('txtEndDate')
    var StartDate=oDateChooser.getValue();
    //-------------------------------0257 MODIFIED-----------------------------------------
   //if (StartDate.getValue()>=EndDate)
   //-------------------------------=============-----------------------------------------
    if (StartDate>=EndDate.getValue())
    {
        alert("Envalid Start Date");
        oDateChooser.setValue('');
        oDateChooser.focus
    }

}
//Tamer Contract Screen
function txtStartDate_ValueChanged(oDateChooser, newValue, oEvent)
{
 var EndDate=igdrp_getComboById('txtEndDate')
    var StartDate=oDateChooser.getValue()
    if (StartDate>EndDate.getValue())
    {
        alert("Envalid Start Date")
        oDateChooser.setValue('')
        oDateChooser.focus
    }
}


    
// Tamer Companies _ Departmenets _ Structure _ Posistions  Screens Scripts
function UltraWebTree1B_NodeClick(treeId, nodeId, button){
    var BranchID= igtree_getNodeById(nodeId)

	var winopen = window.open("frmCompanies.aspx?ID="+BranchID.getTag()+"&Mode=E","_Parent","height=515,width=780,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	winopen.document.focus();
}
function UltraWebTree1D_NodeClick(treeId, nodeId, button){
	var DepartmentID= igtree_getNodeById(nodeId)

	var winopen = window.open("frmDepartments.aspx?ID="+DepartmentID.getTag()+"&Mode=E","_Parent","height=515,width=780,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	winopen.document.focus();
}
function UltraWebTree1P_NodeClick(treeId, nodeId, button){
	var PostionID= igtree_getNodeById(nodeId)

	var winopen = window.open("frmPositions.aspx?ID="+PostionID.getTag()+"&Mode=E","_Parent","height=515,width=780,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	winopen.document.focus();
}


//frmPrepareVacations=====================================================================================

var CurrentRow='';
//Created By [Dataocean] 09-09-2007 
function UwgSearchEmployees_DblClickHandler(gridName, cellId)
{
var grid = igtbl_getGridById(gridName);
var cell = igtbl_getCellById(cellId);
var Row = igtbl_getRowById(cellId);
var EmpID = Row.getCell(9).getValue();
// 
var ContractDays = Row.getCell(7).getValue();
var RemainingDays = Row.getCell(8).getValue();
//

var Control =window.document.getElementById("wdcToDate");
var Fisical = Control.value;
if (CurrentRow != 'UwgSearchEmployees_flr')
{
if (Row.getCell(6).getValue()=="True")
{
window.open("frmEmployeesVacationTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=E&ContractDays=" + ContractDays + "&RemainingDays=" +RemainingDays, "_Parent","height=550,width=715,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No"); 
}
else
{
window.open("frmEmployeesVacationTransactions.aspx?Fisical=" + Fisical + "&ID=" + EmpID + "&Mode=N&ContractDays=" + ContractDays + "&RemainingDays=" +RemainingDays, "_Parent","height=550,width=715,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No"); 
}
}

}
function UwgSearchUsers_AfterRowActivateHandler(gridName, rowId)
{
CurrentRow=rowId; 
}

//============================================================================================================
//Name : DataOcean
//Date : 06/05/2008
//Description : open the menu item 
//Module : navigation handler  
//Screen : frmmenuleft
//============================================================================================================
function MainMenuTree_NodeClick(treeId, nodeId, button)
{
	 var item         =  igtree_getNodeById(nodeId)
     var targetValue  =  item.getTag()
     if (targetValue != null)
	 {
        var RelatedForm     =  GetValue(targetValue ,'RelatedForm');     
        var Height          =  GetValue(targetValue ,'Height');
        var Width           =  GetValue(targetValue ,'Width');
        var OperationName   =  GetValue(targetValue ,'OperationName')
        var paymentID       =  GetValue(targetValue ,'PaymentTypeID')  
	    ShowScreen(RelatedForm,Height,Width,OperationName,paymentID);
	 }
}

function ShowScreen(formName,hight,width,OperationName,PaymentID)
{
    var win = window.open(formName + "?PaymentTypeID=" + PaymentID + "&OperationName=" + OperationName, formName.substring(0, formName.length - 5), "height=" + hight + ",width=" + width + ",top=160,left=240,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
     win.focus();
}
//============================================================================================================
//Name : DataOcean
//Date : 11/05/2008
//Description : Resize the menu in order to fit the screen size  
//Module : navigation handler  
//Screen : frmmenuleft
//============================================================================================================

function UWLAvaliableModules_InitializeListbar(oListbar, oEvent)
{
            var hight   =   window.frameElement.height - 38 - (oListbar.Groups.length * 32);
			var width   =   window.frameElement.width -3;
            var bar     =	iglbar_getListbarById('UWLAvaliableModules');
            var pan     = window.document.getElementById("Panel1")
            var oToolbar    = igtbar_getToolbarById("UltraWebToolbar1")
            oListbar.Element.style.height = hight + "px"
            oListbar.Element.style.width  = width + "px"
            pan.style.visibility = "hidden"
            oToolbar.Element.style.visibility = "visible"       
}

//============================================================================================================
//Name : DataOcean
//Date : 12/05/2008
//Description : Resize the menu tab in order to close   
//Module : navigation handler  
//Screen : frmmenuleft
//============================================================================================================

function UltraWebToolbar1_Click(oToolbar, oButton, oEvent)
{
    
    var pan = window.document.getElementById("Panel1")
    var values = window.frameElement.parentElement.cols
    var valuesArr = values.split(',')
    window.frameElement.parentElement.cols = "32,79%," + valuesArr[2]
    pan.style.visibility = "visible"
    oToolbar.Element.style.visibility = "hidden"
}

//============================================================================================================
//Name : DataOcean
//Date : 12/05/2008
//Description : open the main menu pan   
//Module : navigation handler  
//Screen : frmmenuleft
//============================================================================================================

function WIBOpenMainMenu_Click(oButton, oEvent)
{
    var pan         = window.document.getElementById("Panel1")
    var oToolbar    = igtbar_getToolbarById("UltraWebToolbar1")
    var values = window.frameElement.parentElement.cols
    var valuesArr = values.split(',')
    pan.style.visibility = "hidden"
    oToolbar.Element.style.visibility = "visible"   
    window.frameElement.parentElement.cols = "225,79%," + valuesArr[2]
}


//============================================================================================================
//Name : DataOcean
//Date : 12/05/2008
//Description : Resize the menu in order to fit the screen size  
//Module : navigation handler  
//Screen : frmmenuRight
//============================================================================================================
function UWLAvaliableReports_InitializeListbar(oListbar, oEvent)
{
            var hight   =   window.frameElement.height - 38 - (oListbar.Groups.length * 32);
			var width   =   window.frameElement.width -3;
            var bar     =	iglbar_getListbarById('UWLAvaliableReports');
             var pan    =   window.document.getElementById("Panel1")
             var oToolbar    = igtbar_getToolbarById("UltraWebToolbar1")
            oListbar.Element.style.height = hight + "px"
            oListbar.Element.style.width  = width + "px"
            pan.style.visibility = "hidden"
            oToolbar.Element.style.visibility = "visible"       
}

//============================================================================================================
//Name : DataOcean
//Date : 12/05/2008
//Description : Resize the menu tab in order to close   
//Module : navigation handler  
//Screen : frmmenuReport
//============================================================================================================


function UltraWebToolbar1Report_Click(oToolbar, oButton, oEvent)
{
    
    var pan = window.document.getElementById("Panel1")
    var values = window.frameElement.parentElement.cols
    var valuesArr = values.split(',')
    window.frameElement.parentElement.cols = valuesArr[0] + ",79%,32"  
    pan.style.visibility = "visible"
    oToolbar.Element.style.visibility = "hidden"
}

//============================================================================================================
//Name : DataOcean
//Date : 12/05/2008
//Description : Resize the menu tab in order to open
//Module : navigation handler  
//Screen : frmmenuReport
//============================================================================================================

function WIBOpenMainReport_Click(oButton, oEvent)
{
    var pan         = window.document.getElementById("Panel1")
    var oToolbar    = igtbar_getToolbarById("UltraWebToolbar1")
    var values = window.frameElement.parentElement.cols
    var valuesArr = values.split(',')
    pan.style.visibility = "hidden"
    oToolbar.Element.style.visibility = "visible"   
    window.frameElement.parentElement.cols = valuesArr[0] + ",79%,225" 
}

//============================================================================================================
//Name : DataOcean
//Date : 06/05/2008
//Description : open the menu item 
//Module : navigation handler  
//Screen : frmmenuleft
//============================================================================================================

function UltraWebTree1ReportTree_NodeClick(treeId, nodeId, button)
{
	 var item         =  igtree_getNodeById(nodeId)
     var targetValue  =  item.getTag()
     var startvalue   =  targetValue.substring(0,2)
     if (startvalue == 'r=')
	 {
        ShowReportScreen(targetValue.substring(2));
	 }
}

function ShowReportScreen(ReportCode)
{
     var win = window.open("frmReportsViewerCriterias.aspx?Code=" + ReportCode,"ReportScreen","height=500,width=730,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
     win.focus();
}


function checkRecordCancelDate()
{
    var tab         = igtab_getTabById("UltraWebTab1")
    var chkbox      = tab.findControl("CheckBox1");
    var datectrl    = igdrp_getComboById('UltraWebTab1__ctl1_WebDateChooser1');
 if(chkbox.checked ==true)
     datectrl.setEnabled(true);
 else
    { datectrl.setEnabled(false);
      datectrl.setValue(null);
    }
}


function EliminateSpecialCharacters() 
{
 var e=window.event;
 if (e.keyCode ==222 || e.keyCode ==32)
     event.returnValue=false;
}


