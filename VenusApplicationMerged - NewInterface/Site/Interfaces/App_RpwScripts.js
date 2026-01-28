
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
//---------------------------------------------------------------------
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
//---------------------------------------------------------------------
function ArabicKeyPress(e)
{
  var key;
  var obj;


  if (window.event) 
  {
    e = window.event;
    obj = e.srcElement;
    key = e.keyCode;    
  }
   else 
  {
    obj = e.target;
    key = e.charCode;
  }

  if (e.bubbles==false)
    return true;
//---------------------------------------------------------------------
//  Change to English, if user is using an OS non-English keyboard
//--------------------------------------------------------------------- 
  if (key >= 0x00FF) 
  {
    isiri2901_nativelang = 1;
    setArabic();
  } else
    if (isiri2901_nativelang == 1) 
    {
      isiri2901_nativelang = 0;
      setEnglish();
    }

//---------------------------------------------------------------------
// Avoid processing if control or higher than ASCII
// Or ctrl or alt is pressed.
//---------------------------------------------------------------------
  if (key < 0x0020 || key >= 0x007F || e.ctrlKey || e.altKey || e.metaKey)
    return true;

  if (isiri2901_lang == 1) 
  { 
    //If Persian
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
//---------------------------------------------------------------------
function setArabic (obj, quiet)
{
  isiri2901_lang = 1;
  if (obj) {
    obj.style.textAlign = "right";
    obj.style.direction = "rtl";
    }
  if (!quiet)
    window.defaultStatus = "Data Ocean 2008";
}
//---------------------------------------------------------------------
function setEnglish (obj, quiet)
{
  isiri2901_lang = 0;
  if (obj) {
    obj.style.textAlign = "left";
    obj.style.direction = "ltr";
   }
  if (!quiet)
    window.defaultStatus = "Data Ocean 2008";
}
//---------------------------------------------------------------------
function toggleDir (obj, quiet)  
{
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
//---------------------------------------------------------------------
// Inserts a string at cursor
//---------------------------------------------------------------------
function pnhMozStringInsert(elt, newtext) 
{
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
//---------------------------------------------------------------------
function DoRefreshBack()
{
  window.opener.document.forms[0].submit() 
  window.document.focus();
}
//---------------------------------------------------------------------
////////// General Functions 
//---------------------------------------------------------------------
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
//---------------------------------------------------------------------
var ColumnWidth =0 ;
var SizedColumnIndex = new String();
var CurrentRow='';
var ColumnIndex=0 ;
var ColumnBaseName='' ;
var ColumnDataType='';
var ColumnHeaderText='' ;
var fieldKey='';
var SizedColumnName=''; 
//---------------------------------------------------------------------
function REP_W_step5_uwgViewColumns_AfterColumnSizeChangeHandler(gridName, columnId, width)
{
         var ID; 
         
         var Control = window.document.getElementById("txtSizeInfo");
         ID               = igtbl_getColumnById(columnId);
         SizedColumnIndex = ID.Index;
         ColumnWidth      = width;
         SizedColumnIndex = SizedColumnIndex + '_' + width + '$';
         SizedColumnName =  ID.Key + '_' + width + '$';
         Control.value =  Control.value +  ID.Key + '_' + width + '$';
         
         
  }
//---------------------------------------------------------------------
function uwgViewColumns_MouseDownHandler(gridName, id, button)
{
	//Add code to handle your event here.
	    if(button == 2 && CurrentRow != '')
	    {
	       
	       var grid = igtbl_getGridById(gridName);
           var cell = igtbl_getCellById(id);
           var Row  = igtbl_getRowById(id);
               ColumnIndex = cell.Column.Index;
               ColumnBaseName = cell.Column.Id;
           var column = igtbl_getColumnById(cell.Column.Id);
               fieldKey = column.Key;
               SizedColumnName =  fieldKey + '_' + column.Width + '$';
               ColumnDataType = column.DataType;       
               ColumnHeaderText = column.HeaderText;
 		       igmenu_showMenu('UltraWebMenu2', event);
		   return true ;
	    }
}
//---------------------------------------------------------------------
  function UwgViewColumns_AfterRowActivateHandler(gridName, rowId)
    {
      selectedColumnIndex = -1;
      CurrentRow=rowId;  	
    }
 //---------------------------------------------------------------------
    function UltraWebMenu2_ItemClick(menuId, itemId)
    {   

        var Row      =     igtbl_getActiveRow('uwgViewColumns');
        var item     =     igmenu_getItemById(itemId)
        var tagvalue =     item.getTag()  
        //Get The Column Name The Cursor is Over 
        
        var cell     =     igtbl_getCellById('uwgViewColumns' + "_rc_" + Row.getIndex() + "_" + 1);
     
        var formName =     GetValue(tagvalue ,'Form');  
        var mode     =     GetValue(tagvalue ,'Mode');   
        var hight    =     GetValue(tagvalue ,'Height');   
        var width    =     GetValue(tagvalue ,'Width');
        var type     =     GetValue(tagvalue ,'type');   
        var winopen  =     window.open(formName + "?Mode=" + mode+ "&CellIndex="+ColumnIndex +"&SizeInfo="+SizedColumnIndex+"&ColumnDataType="+ColumnDataType+"&ColumnName="+ColumnHeaderText+"&ControlName=txtTarget"+"&fieldKey="+fieldKey+"&SizedColumnName="+SizedColumnName,type,"height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=Yes");
        winopen.document.focus();
  	}
//---------------------------------------------------------------------
    function uwgViewColumns_ClickCellButtonHandler(gridName, cellId)
    {

    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var Row  = igtbl_getRowById(cellId);
    var ID   = Row.getCell(8).getValue();
    var FieldName = Row.getCell(1).getValue();
    var RowIndex          =	Row.getIndex()	 
        if (RowIndex > 0)
        {
            if (cellId == "uwgViewColumns_rc_"+ RowIndex +"_7" )  
            window.open("frmReportsFieldsSettings.aspx?ID=" + ID + "&Mode=V" + "&FieldName=" + FieldName , "_Parent","height=294,width=408,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
        }
    }
//---------------------------------------------------------------------	
 function ReP_W_uwgcriteria_DblClickHandler(gridName, cellId)
 {
		
	       var grid              = igtbl_getGridById(gridName);
   	       var cell              = igtbl_getCellById(cellId);
	       var Row               = igtbl_getRowById(cellId);
	       var ID                = Row.getCell(8).getValue();
	       var FieldName         = Row.getCell(1).getValue();
	       var RowIndex          =	Row.getIndex();
	       var FieldWidth        = 661;
	       var FieldType         = Row.getCell(2).getValue();
	        switch(FieldType.toUpperCase())
				    {
				        case("INTEGER"):
				        {
				           FieldType     = 1;
				           break;
				        }
				        case("NUMERIC"):
				        {
				           FieldType     = 1;
                           break;
				        }
                        case("INT32"):
                        {
				        FieldType        = 1;
				        break;
				        }
                        case("INT16"):
                        {
				           FieldType     = 4;
				           break;
				        }
                        case("SINGLE"):
                        {
				           FieldType     = 1;
				           break;
				        }
                        case("DOUBLE"):
                        {
				           FieldType     = 1;
				           break;
				        }
                        case("DECIMAL"):
                        {
				           FieldType     = 1;
				           break;
				        }
                        case("FLOAT"):
                        {
				           FieldType     = 1;
				           break;
				        }
                        case("MONEY"):
				        {
				           FieldType     = 1;
				           break;
				        }
				        case("DATE"):
				        {
				           FieldType     = 3;
				           break;
				        }
				        case("DATETIME"):
				        {
				           FieldType     = 3;
				           break;
				        }
				        case("BYTE"):
				        {
				           FieldType     = 4;
				           break;
				        }
				     }
				     
		   if(FieldType!=1 && FieldType!=3 && FieldType!=4 )
              {   
				FieldType = 2;
		      }
                     
	       var txtDataSource     = window.document.getElementById("txtDataSource"); 
	       var DataSource        = txtDataSource.value;
	       	 
           if (RowIndex >= 0)
             {
	          window.open("frmReportsFieldsSettings.aspx?ID=" + ID + "&Mode=C" + "&FieldName=" + FieldName +"&DataSource="+DataSource+"&ft="+FieldType, "_Parent","height=294,width=" + FieldWidth + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
             }
              
}
//--------------------------------------------------------------------- 	
function uwgColumnsStyles_CellClickHandler(gridName, cellId, button)
{
	
	          var Row  = igtbl_getRowById(cellId);
              var cell       = igtbl_getCellById(gridName + "_rc_" + Row.getIndex() + "_0")
              var ultraTab   = igtab_getTabById("UltraWebTab1");
              var control    = igtab_getElementById("txtStyle",ultraTab.element);
	                    
	          control.value  = cell.getValue();
	          var nextCell =  igtbl_getCellById(gridName + "_rc_" + Row.getIndex() + "_1")
	          var nextFormatControl = igtab_getElementById("txtStyleFormat",ultraTab.element);
	          var chkcontrol    = igtab_getElementById("chkCurrencySymbol",ultraTab.element);
	          nextFormatControl.value =  nextCell.getValue();

              if (chkcontrol.checked==true)
                {
                  control.value = '$' + control.value;        
                  nextFormatControl.value = '$' + nextFormatControl.value;
                }
              else
                {
                  var  IntCurrencyIndex  = control.value.indexOf('$');
                  if (IntCurrencyIndex>= 0) 
                     {
                       control.value = control.value.substring(1);
                       nextFormatControl.value = nextFormatControl.value.substring(1);
                     }
               }
 }
//--------------------------------------------------------------------- 	    
function DisplayCurrency() 
{
  var ultraTab      = igtab_getTabById("UltraWebTab1");
  var control       = igtab_getElementById("txtStyle",ultraTab.element);
  var styleControl  = igtab_getElementById("txtStyleFormat",ultraTab.element);
  var chkcontrol    = igtab_getElementById("chkCurrencySymbol",ultraTab.element);
 
 if (chkcontrol.checked==true)
 {
         var  IntCurrencyIndex0  = styleControl.value.indexOf('$');
           if (IntCurrencyIndex0 < 0) 
                {
              control.value = '$' + control.value;        
              styleControl.value = '$' + styleControl.value;
                }
                
              
 }
 else
 {
          var  IntCurrencyIndex  = styleControl.value.indexOf('$');
                if (IntCurrencyIndex>= 0) 
                {
                    control.value = control.value.substring(1);
                    styleControl.value = styleControl.value.substring(1);                                      
                }
 
 }
  
}
//---------------------------------------------------------------------
function reportViewerType_IndexChanged()
{
var ddlControl  = window.document.getElementById("ddlViewerType"); 
var txtAttached = window.document.getElementById("txtAttached"); 
var btnNext     = window.document.getElementById("btnNext"); 

var IntSelectedValue = ddlControl.value;
if (IntSelectedValue ==0)
  {
   txtAttached.disabled = true ;
   btnNext.value = "Next"
   txtAttached.value = '';
  }
else
  {
  btnNext.value = "Finish"
  txtAttached.disabled = false ;
  }
}
//---------------------------------------------------------------------
// Report Writer Fields Settings Screen 
//---------------------------------------------------------------------
function ReportFieldsddlStatus_IndexChanged()
{
  var ddlControl  = window.document.getElementById("ddlStatus"); 
  var txtCarrier = window.document.getElementById("txtCarrier"); 
  var IntSelectedValue = ddlControl.value;
  var StrFieldsWithFormula =txtCarrier.innerText;
  var strArrayFields =StrFieldsWithFormula.split("%");
  var i;
  var j=0;
  var strFormulaFieldsTemp;
  var StrArrFields = new Array();
  var strArrFormulas= new Array();
  for(i=1;i<=strArrayFields.length-2;i+=2)
  {
  StrArrFields[j]= strArrayFields[i];
  strArrFormulas[j] = strArrayFields[i+1];
  strFormulaFieldsTemp += "#" + strArrayFields[i+1];
  j+=1;
  }
  
if (IntSelectedValue =="False")
   {
      var txtFieldName  = window.document.getElementById("txtCode"); 
      var FieldName     = txtFieldName.value;
      var FoundedFields = "";
      for (i=0;i<=StrArrFields.length-1;i++)
         {
           if (strArrFormulas[i].indexOf("["+FieldName+"]") >-1)
             {
               FoundedFields += "," + StrArrFields[i];
             }
         }
      FoundedFields =FoundedFields.substring(1)
  if (FoundedFields.length >0)
  {
             alert(" Field " + FieldName + " Is Used by " + FoundedFields + " Formula Field(s) , Removing It Will Disable This(These) Field(s)");
             ddlControl.value="True"
  }
 }
 
}
//---------------------------------------------------------------------
function uwgFieldsDefaults_CellClickHandler(gridName, cellId, button)
{
	          var Row  = igtbl_getRowById(cellId);
       	      var nextCell =  igtbl_getCellById(gridName + "_rc_" + Row.getIndex() + "_0")
	          var txtConstant = window.document.getElementById("txtConstant");
	          txtConstant.value =  nextCell.getValue();
 }
 //---------------------------------------------------------------------
 function uwgFieldsDefaults_DblClickHandler(gridName, cellId)
 {
               var row           = igtbl_getRowById(cellId); 
			   var cell          = row.getCell(0).getValue();
			   var Target        = document.getElementById("txtTargetControl");
			   var lblC          = window.document.getElementById("lblC");
			   var TargetValue   = Target.value;
			   var lblCValue     = lblC.value;
				    
			   window.opener.document.forms[0][TargetValue].value=cell;
			   window.opener.document.forms[0][lblCValue].value="lblC";
			   window.opener.focus();
			   window.opener.document.forms[0][TargetValue].focus();
			   window.close(); 
 }
 //---------------------------------------------------------------------
function btnCancel_Click(oButton, oEvent)
   {
     window.close();
   }
 //---------------------------------------------------------------------  
function btnApply_Click(oButton, oEvent)
{
               var txtConstant = window.document.getElementById("txtConstant");
               var lblC        = window.document.getElementById("lblC");
               var ConstantValue = txtConstant.value;
               var lblCValue     = lblC.value;
               if(ConstantValue!="" && ConstantValue!="0")
                  {
                    var Target      = document.getElementById("txtTargetControl");
			        var TargetValue = Target.value;
				    
          	        window.opener.document.forms[0][TargetValue].value=ConstantValue;
          	        window.opener.document.forms[0][lblCValue].value="lblC";
			        window.opener.focus();
			        window.opener.document.forms[0][TargetValue].focus();
			        window.close(); 
			      }
			    else
			      {
			        alert("Please Select a Default Value ! ");
			      }
}
//---------------------------------------------------------------------
function MnuMainmenu_ItemClick(menuId, itemId)
   {   

        var Menu         =  igmenu_getMenuById(menuId);
        var Target       =  Menu.TargetUrl;
        var item         =  igmenu_getItemById(itemId)
     var targetValue  =     item.getTargetUrl()
     var ReportCode   =     GetValue(targetValue ,'ReportName');   
    getReportTypeByCode(ReportCode, function(rt){
        var t = (rt||'').toUpperCase();
        if (t === 'STI') {
            CallStiReport(ReportCode);
        } else {
            ShowOGScreen(ReportCode);
        }
    });
   }
function getReportTypeByCode(code, cb){
    try{
        var xhr = new XMLHttpRequest();
        xhr.open('POST','../Common/WebServices/ReportsWs.asmx/GetReportType',true);
        xhr.setRequestHeader('Content-Type','application/json; charset=utf-8');
        xhr.onreadystatechange=function(){
            if(xhr.readyState===4){
                if(xhr.status===200){
                    try{ var res = JSON.parse(xhr.responseText); cb(res && res.d ? res.d : ''); }catch(e){ cb(''); }
                } else { cb(''); }
            }
        };
        xhr.send(JSON.stringify({Code: code}));
    }catch(e){ cb(''); }
}
//---------------------------------------------------------------------
function CallStiReport(ReportCode) {
    var hight = 499;
    var width = 722;
    var win = window.open("frmReportsViewerCriteriasSti.aspx?Code=" + ReportCode, "_www", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
    win.focus();
}
function ShowOGScreen(ReportCode)
{
     var hight =499;
     var width =722;
     var win =window.open("frmReportsViewerCriterias.aspx?Code="+ReportCode,"_www","height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
     win.focus();
      }
//---------------------------------------------------------------------
function btnSave_Click(oButton, oEvent)
  {
      doSubmitTag =0;
      var dTypeCtrl = window.document.getElementById("txtSubmit");

      if(dTypeCtrl.value=="4")
      {
         var EngItemsCtrl    = window.document.getElementById("txtEngItems");
         var ArbItemsCtrl    = window.document.getElementById("txtArbItems");
         var ItemsValuesCtrl = window.document.getElementById("txtItemsValues");

         var ArrEngIs        = EngItemsCtrl.value.split(",");
         var ArrArbIs        = ArbItemsCtrl.value.split(",");
         var ArrIs           = ItemsValuesCtrl.value.split(",");
           
         if(ArrEngIs.length != ArrArbIs.length ||ArrEngIs.length != ArrIs.length ||ArrIs.length != ArrArbIs.length  )  
         {
            doSubmitTag= 1;
         }
      }
}
//---------------------------------------------------------------------
function DoSubmit(e)
{
if(doSubmitTag==1)
e.returnValue = false;
}
//---------------------------------------------------------------------    
//=================================================================================
// Fields Selections View
//=================================================================================
  // Report Viewer Criteria 
   var StrInputNames='';
   var StrReportCode='';
   var StrRealSQLNames='';
   var StrNit ='';
   var strNumericOperationsText        =     ["Equal","Not Equal","Grater Than","Greater Than Or Equal","Less Than","Less Than Or Equal "]
   var strNumericOperationsValue       =     ["=","<>",">",">=","<","<="]

   var strCharactersOperationsText     =     ["Like","Not Like","Equal","No Equal","Greater Than","Greater Than Or Equal","Less Than","Less Than Or Equal "]
   var strCharactersOperationsValue    =     ["Like","Not Like","=","<>",">",">=","<","<="]

   var strDatesOperationsText          =     ["Equal","No Equal","Greater Than","Greater Than Or Equal","Less Than","Less Than Or Equal "]
   var strDatesOperationsValue         =     ["=","<>",">",">=","<","<="]

   var strBooleansOperationsText       =     ["Equal","Not Equal"]
   var strBooleansOperationsValue      =     ["=","<>"]
    
 //---------------------------------------------------------------------
   function SetParameter(Names,RealNames,ReportCode,RepFilters)     
   {
        StrInputNames   = Names; 
        StrReportCode   = ReportCode;  
        StrRealSQLNames = RealNames;
        StrNit          = RepFilters;
    }
    //---------------------------------------------------------------------
   function btnCriteriaPreview_Click(oButton, oEvent) {
    
    var ctrTxtOperations = window.document.getElementById("txtOperations");
	var ctrTxtReportCode = window.document.getElementById("txtReportCode");
	var ctrTxtSqlNames   = window.document.getElementById("txtSqlNames");
	var ctrTxtRealNames = window.document.getElementById("txtRealNames");
	var chkLnaguage = igtab_getElementById("ChkArabicView", igtab_getTabById("UltraWebTab1").element)
	
	StrInputNames        = ctrTxtRealNames.value;
	StrReportCode        = ctrTxtReportCode.value;
	StrRealSQLNames      = ctrTxtSqlNames.value;
	StrNit               =ctrTxtOperations.value;
	MainCollect_Start(ChkLnaguage.checked,0) 
   }
   
   function btnCriteriaDisplay_Click(oButton, oEvent)
   {
   	
   	var ctrTxtOperations = window.document.getElementById("txtOperations");
	var ctrTxtReportCode = window.document.getElementById("txtReportCode");
	var ctrTxtSqlNames   = window.document.getElementById("txtSqlNames");
	var ctrTxtRealNames  = window.document.getElementById("txtRealNames");
	var ChkLnaguage = igtab_getElementById("ChkArabicView", igtab_getTabById("UltraWebTab1").element)//window.document.getElementById("ChkArabicView");
	
	StrInputNames        = ctrTxtRealNames.value;
	StrReportCode        = ctrTxtReportCode.value;
	StrRealSQLNames      = ctrTxtSqlNames.value;
	StrNit               =ctrTxtOperations.value;
	MainCollect_Start(ChkLnaguage.checked,1) 
	
	
	}
	//---------------------------------------------------------------------
	function GetOperationForType( StrType , IntIndex)
       {
        
        if (isNaN(IntIndex)) 
        {
            return IntIndex;
        }
        
        switch(StrType)
         {   case("Num"):
            {
                return strNumericOperationsValue[IntIndex];
            }
            
            case("Dte"):
            {
                return strNumericOperationsValue[IntIndex];
            }
            case("Cur"):
            {
                return strNumericOperationsValue[IntIndex];
            }
           case("CHR"):
           {
                return strCharactersOperationsValue[IntIndex];
           }
            case("txt"):
            {
                return strDatesOperationsValue[IntIndex];
            }
            case("BLN"):
            {
                return strBooleansOperationsValue[IntIndex];
            }
            
              case("Drd"):
            {
                return strBooleansOperationsValue[IntIndex];
            }
            
              case("Drl"):
            {
                return strBooleansOperationsValue[IntIndex];
            }
        }
        return "";
 }
//---------------------------------------------------------------------
 function MainCollect_Start(ChkLnaguage,print)
    {
        var Arr = StrInputNames.split("/");
        var Arrsql = StrRealSQLNames.split("/");
               
        var ArrNip = StrNit.split("/");
        
        var Final_Value ="";
        var Final_SQL = "";
        var FinalTest ="";
        var Values = "";
        
        for(i=0;i<Arr.length;i++)
        {
        
            var str=Arr[i].substring(0,3)
            var ultraTab      = igtab_getTabById("UltraWebTab1");
	        //Case Of Stored Procedure DatSource
	        switch(str)
	        {
	            case("txt"):
	                {
                       var Control       = igtab_getElementById(Arr[i],ultraTab.element);
                       if (Control.value!="")
                              {
                                 Final_Value += "|" + Arr[i].substring(3) 
                                 if (GetOperationForType(str,ArrNip[i]).toUpperCase() == "LIKE")
                                   {
                                     Final_SQL   += "|" + Arrsql[i] + " " +                  GetOperationForType(str,ArrNip[i]) + " '$" + Control.value + "$' ";
                                   }
                                 else
                                   {
                                    Final_SQL   += "|"  + Arrsql[i] + " " +                  GetOperationForType(str,ArrNip[i]) + " '" + Control.value + "' ";
                                   }
                                 Values += "|" +Control.value ;                                      
                                 }
                       break;
                   }
               case ("Msk"):
                   {
                       var Control = igtab_getElementById("UltraWebTab1__ctl0_" + Arr[i]);
                       if (Control == null)
                           return;
                       if (Control.value != "") {
                           Final_Value += "|" + Arr[i].substring(3);

                           Final_SQL += "|Convert ( VarChar , " + Arrsql[i] + ",111)" + GetOperationForType(str, ArrNip[i]) + "'" + Control.value + "'";
                           Values += "|" + Control.value;
                       }
                       break;
                   }
               case ("Dte"):
                   {
                       var Control = igdrp_getComboById("UltraWebTab1__ctl0_" + Arr[i]);
                       if (Control == null)
                           return;
                       if (Control.getText() != "") {
                           Final_Value += "|" + Arr[i].substring(3);

                           Final_SQL += "|Convert ( VarChar , " + Arrsql[i] + ",111)" + GetOperationForType(str, ArrNip[i]) + "'" + Control.getText() + "'";
                           Values += "|" + Control.getText();
                       }
                       break;
                   }
		        case("Num"):
		            {
		                var Control;
		                var ControlName ;
                        if (Arr[i].indexOf("=") >0)
                          {
                           ControlName =  Arr[i].substring(0,Arr[i].indexOf("="));
                           Control       = igtab_getElementById(ControlName,ultraTab.element);
                           if (Control.value!="")
                             {
                             Final_Value += "|" + Arr[i].substring(3) 
                             if (Arrsql[i].toUpperCase().indexOf("SELECT") > 0 )
                             {
                                Final_SQL   += "|" + Arrsql[i] + " = '" + Control.value + "') " ; 
                                Values += "|'" + Control.value + "' ";
                             }
                             else
                             {
                               Final_SQL   += "|" + Arrsql[i] + " = " + Control.value  ; 
                               Values += "|" + Control.value ;
                             }
                          }
		                 }
                        else
                        {
                          ControlName   = Arr[i];
                          Control       = igtab_getElementById(ControlName,ultraTab.element);
                          if (Control.value!="")
                            {
                              var g;
                              Final_Value += "|" +  Arr[i].substring(3) 
                              Final_SQL   += "|" +  Arrsql[i] + " " + GetOperationForType(str,ArrNip[i]) + " " +Control.value ;
                              Values += "|" + Control.value;
                            }
                        }
                      break;
		            }
		        case("Cur"):
		            {
		                var Control       = igtab_getElementById(Arr[i],ultraTab.element);
		                   if (Control.value!="")
                              {
                                Final_Value += "|" +  Arr[i].substring(3) 
                                Final_SQL   += "|" +  Arrsql[i] + " " +  GetOperationForType(str,ArrNip[i]) + " " +Control.value ;
                                Values += "|" + Control.value;
                              }
	                break;
		            }
                case("Drd"):
                    {
                        var Control       = igtab_getElementById(Arr[i],ultraTab.element);
                           if (Control.value!="")
                              {
                                 Final_Value += "|" +  Arr[i].substring(3) 
                                 Final_SQL   += "|" +  Arrsql[i] + " " +  GetOperationForType(str,ArrNip[i]) + " " +Control.value ;
                                 Values += "|" + Control.value;
                              }
                        break;
                    }           
    		    case("Drl"):
		            {
		                var Control       = igtab_getElementById(Arr[i],ultraTab.element);
		                if (Control.value!= "")
                          {
                            Final_Value += "|" +  Arr[i].substring(3) 
                            Final_SQL   += "|" +  Arrsql[i] + " " +  GetOperationForType(str,ArrNip[i]) + " " +Control.value ;
                            Values += "|" + Control.options[Control.selectedIndex].value;
                          }
                     break;
		            }    
    		        
	            }
        }
       
            //OpenModal1("frmReportsGridViewer.aspx?Language=" + ChkLnaguage + "&Criteria=" + Final_Value.substring(1) + "&ReportCode=" + StrReportCode + "&sq0=" + Final_SQL.substring(1) + "&v=" + Values.substring(1) + "&preview=" + print, 600, 900, false, "");
          var hight =window.screen.availHeight -35;
	        var width =window.screen.availWidth -10;
	        var win =window.open("frmReportsGridViewer.aspx?Language=" + ChkLnaguage + "&Criteria=" + Final_Value.substring(1)+ "&ReportCode=" + StrReportCode+"&sq0="+Final_SQL.substring(1)+"&v="+Values.substring(1)+"&preview="+print,"_NEW","height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
	        win.moveTo(0,0);
	        win.focus();
 }
//---------------------------------------------------------------------
// Removes leading whitespaces
//---------------------------------------------------------------------
function LTrim( value ) 
{
	
	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");
	
}
//---------------------------------------------------------------------
// Removes ending whitespaces
//---------------------------------------------------------------------
function RTrim( value ) 
{
	
	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");
	
}
//---------------------------------------------------------------------
////// Removes leading and ending whitespaces
//---------------------------------------------------------------------
function trim( value ) 
{
	
	return LTrim(RTrim(value));
	
}
//---------------------------------------------------------------------
function trimAll(sString) 
{ 
while (sString.substring(0,1) == ' ') 
{ 
sString = sString.substring(1, sString.length); 
} 
while (sString.substring(sString.length-1, sString.length) == ' ') 
{ 
sString = sString.substring(0,sString.length-1); 
} 
return sString; 
} 
//---------------------------------------------------------------------
 function GetValueFromString(Expression ,Find) 
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
//---------------------------------------------------------------------              
 function uwgViewColumns_ColumnHeaderClickHandler(gridName, columnId, button)
 {
	  var column = igtbl_getColumnById(columnId);
	  selectedColumn      = column
	  selectedColumnIndex = column.Index;
	  GetColumnSettings();  
}
//---------------------------------------------------------------------
function BarChange(Id)
{
    var bar = window.document.getElementById(Id)
    currToolBar = bar.value;
    if (selectedColumnIndex == -1)
        GetGridSettings()
     else
        GetColumnSettings();
}
//---------------------------------------------------------------------
var gridName;
var headerBarName;
var rowBarName;
var footerBarName;
var selectedColumnIndex;
var selectedColumn

var currToolBar = "H";


var iheaderTextI     =1

var iHeightI         =4
var iWidthI          =6
var iborderTopI      =8  
var iborderLeftI     =8
var iborderRightI    =8
var iborderBottomI   =8
var iborderColorI    =9
var iborderWidthI    =10
//var iborderStyleI    =14
var imarginsI        =13 
var ipaddingI        =14
var itxtAlginLeftI   =16
var itxtAlignCenterI =16
var itxtAlignRightI  =16
//var iverticalAlginI  =22
var iboldI           =19
var iitalicI         =20
var iunderlineI      =21
var ibackcolorI      =23
var iforeColorI      =24
//var ifontSizeI       =30
//var ifontNameI       =31


var headerTextI     ;

var HeightI         ;
var WidthI          ;
var borderTopI      ;  
var borderLeftI     ;
var borderRightI    ;
var borderBottomI   ;
var borderColorI    ;
var borderWidthI    ;
var borderStyleI    ;
var marginsI        ; 
var paddingI        ;
var txtAlginLeftI   ;
var txtAlignCenterI ;
var txtAlignRightI  ;
var verticalAlginI  ;
var boldI           ;
var italicI         ;
var underlineI      ;
var backcolorI      ;
var foreColorI      ;
var fontSizeI       ;
var fontNameI       ;


var backgroundColor;
var borderColor;
var borderBottomColor;
var borderTopColor;
var borderLeftColor;
var borderRightColor;
var borderStyle;
var borderTopStyle;
var borderLeftStyle;
var borderRightStyle;
var borderBottomStyle;
var borderWidth;
var borderTopWidth;
var borderBottomWidth;
var borderLeftWidth;
var borderRightWidth;
var fontFamily;
var fontSize;
var fontStyle_bold;
var fontStyle;
var textDecorationUnderline;
var textDecorationOverline;
var foreColor;
var height;
var textAlign;
var margin;
var marginTop;
var marginBottom;
var marginLeft;
var marginRight;
var padding;
var paddingTop;
var paddingBottom;
var paddingRight;
var paddingLeft;
var textOverflow;
var overflow;
var verticalAlign;
var width;
var HeaderWrap;
var HeaderText;


var ccbackgroundColor;
var ccborderColor;
var ccborderBottomColor;
var ccborderTopColor;
var ccborderLeftColor;
var ccborderRightColor;

var ccborderStyle = "Notset";
var ccborderTopStyle= "Notset";
var ccborderLeftStyle= "Notset";
var ccborderRightStyle= "Notset";
var ccborderBottomStyle= "Notset";

var ccborderWidth;
var ccborderTopWidth;
var ccborderBottomWidth;
var ccborderLeftWidth;
var ccborderRightWidth;
var ccfontFamily;
var ccfontSize;
var ccfontStyle_bold;
var ccfontStyle;
var cctextDecorationUnderline;
var cctextDecorationOverline;
var ccforeColor;
var ccheight;
var cctextAlign;
var ccmargin;
var ccmarginTop;
var ccmarginBottom;
var ccmarginLeft;
var ccmarginRight;
var ccpadding;
var ccpaddingTop;
var ccpaddingBottom;
var ccpaddingRight;
var ccpaddingLeft;
var cctextOverflow;
var ccoverflow;
var ccverticalAlign;
var ccwidth;
var ccHeaderWrap;
var ccHeaderText;

var ccFormat
var ccCalculatedSummary
var ccShowCurrencySymbol
var ccSort



var txtHeaderStyle
var txtHeaderStyle2
var txtRowStyle
var txtRowStyle2
var txtFooterStyle
var txtFooterStyle2

var strHeaderStyle;
var strHeaderStyle2;
var strRowStyle;
var strRowStyle2;
var strFooterStyle;
var strHFooterStyle2;
//---------------------------------------------------------------------
function GetValueFromString(Expression ,Find) 
{
 
          var StrString;
          var IntLocation;
          var DblLenght;
          var StrRightPart;
          var StrFinalResult;
          var IntNextSeparator;

                
          StrString = Find+"="
          IntLocation = Expression.indexOf(StrString)
          DblLenght = StrString.length


 	      if (IntLocation < 0)
          {
             return ""
          }

          StrRightPart = Expression.substring(IntLocation + DblLenght)
          IntNextSeparator = StrRightPart.indexOf(';')
          if (IntNextSeparator > -1) 
          {
             StrRightPart = StrRightPart.substring(0, IntNextSeparator)
          }

          StrFinalResult = StrRightPart

 	      return StrFinalResult
}
//---------------------------------------------------------------------
function SetValueFromString(Expression ,Find,Value)
{
          var StrString;
          var IntLocation;
          var DblLenght;
          var StrRightPart;
          var StrFinalResult;
          var IntNextSeparator;

          var NExpression = new String()
          NExpression   = Expression;
                
          StrString = Find+"="
          IntLocation = NExpression.indexOf(StrString)
          DblLenght = StrString.length
          

 	      if (IntLocation < 0)
          {
             NExpression+=";"+Find+"="+Value; 
          }
          else
          {
            StrRightPart = NExpression.substring(IntLocation + DblLenght )
            IntNextSeparator = StrRightPart.indexOf(';')
            if (IntNextSeparator > -1) 
            {
                StrRightPart = StrRightPart.substring(0, IntNextSeparator)
            }
            NExpression  = NExpression.replace(Find+"="+StrRightPart,Find+"="+Value)
            
            
          }
          return NExpression
 	      
}
//---------------------------------------------------------------------
function GetGridProperties(vgridName,vhBar,vrBar,vfBar)
{
    gridName            = vgridName;
    headerBarName       = vhBar;
    rowBarName          = vrBar;
    footerBarName       = vfBar;
    selectedColumnIndex = -1; 
    
}
//---------------------------------------------------------------------
function GetAllStyles()
{
    txtHeaderStyle      = window.document.getElementById("txtHeaderStyle")
    txtHeaderStyle2     = window.document.getElementById("txtHeaderStyle2")
    txtRowStyle         = window.document.getElementById("txtRowStyle")
    txtRowStyle2        = window.document.getElementById("txtRowStyle2")
    txtFooterStyle      = window.document.getElementById("txtFooterStyle")
    txtFooterStyle2     = window.document.getElementById("txtFooterStyle2")
    
    strHeaderStyle      = txtHeaderStyle.value.replace(" ","")
       
    strRowStyle         = txtRowStyle.value.replace(" ","")
    
    strFooterStyle      = txtFooterStyle.value.replace(" ","") 
}
//---------------------------------------------------------------------
function SetAllStyles()
{
    txtHeaderStyle      = window.document.getElementById("txtHeaderStyle")
    txtHeaderStyle2     = window.document.getElementById("txtHeaderStyle2")
    txtRowStyle         = window.document.getElementById("txtRowStyle")
    txtRowStyle2        = window.document.getElementById("txtRowStyle2")
    txtFooterStyle      = window.document.getElementById("txtFooterStyle")
    txtFooterStyle2     = window.document.getElementById("txtFooterStyle2")
    
    txtHeaderStyle.value  = strHeaderStyle
    txtRowStyle.value     = strRowStyle
    txtFooterStyle.value  = strFooterStyle
}
//---------------------------------------------------------------------
function Loading()
{
    LoadGridColumnsStyles();
    GetGridSettings();
}
//---------------------------------------------------------------------
function GetGridSettings()
{    
    ResetToolBar()
    var grid           = igtbl_getGridById(gridName);
    if (selectedColumnIndex == -1)
    {
            var col                  = igtbl_getColumnById(gridName+"_c_0_0");
            
            if (currToolBar == "R")
            {
                var currRow 
                var currCell;   
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_0");
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
                var rowIndex    = currRow.getIndex();
                if(rowIndex != -1)
                {
                    currCell = igtbl_getCellById(gridName+"_rc_"+rowIndex+"_0"); 
                }
                else
                {
                    currCell = igtbl_getCellById(gridName+"_anc_0");
                }
            
                var currColStyle      =currCell.Element.currentStyle;
                GetToolBarItems("R");
                SetCurrStyleValues(currColStyle,"R")
               //----------------- Modification 30-03-08 [Start]
                   if(currColStyle != null || currColStyle != undefined )
                        GetGlobalGridStyle(currColStyle,grid,"R")
               //----------------- Modification 30-03-08 [ End ]             
                SetToolBarItemsValues("R")
            }
            //================================================================
            else if (currToolBar == "F")
            {
                var currFooterStyle      
                try
                {
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                    currFooterStyle= col._getFootTags()[0].currentStyle;}
            
                }
                catch(e)
                {}
            
                GetToolBarItems("F");
                SetCurrStyleValues(currFooterStyle,"F")
                //----------------- Modification 30-03-08 [Start]
                if(currFooterStyle != null || currFooterStyle != undefined )
                    GetGlobalGridStyle(currFooterStyle,grid,"F")
                //----------------- Modification 30-03-08 [ End ]
                SetToolBarItemsValues("F")
            }
            //================================================================
            else if (currToolBar == "H")
            {
                var currHeaderStyle
                try
                {
                    if (col._getHeadTags()!= null && col._getHeadTags()!= undefined)               
                        currHeaderStyle = col._getHeadTags()[0].currentStyle;
                }
                catch(e){}
            
                GetToolBarItems("H");
                SetCurrStyleValues(currHeaderStyle,"H")
                //----------------- Modification 30-03-08 [Start]
                if(currHeaderStyle != null || currHeaderStyle != undefined )
                    GetGlobalGridStyle(currHeaderStyle,grid,"H")
                //----------------- Modification 30-03-08 [ End ]
                SetToolBarItemsValues("H")
            }          
    }
  GetAllStyles()  
}
//---------------------------------------------------------------------
function GetColumnSettings()
{
    ResetToolBar()
    if (selectedColumnIndex != -1)
    {
            var col                  = selectedColumn;       
             //=================================================================
            if (currToolBar == "R")
            {  
                var currRow 
                var currCell;   
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_0");
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
                var rowIndex    = currRow.getIndex();
                if(rowIndex != -1)
                {
                currCell = igtbl_getCellById(gridName+"_rc_"+rowIndex+"_"+selectedColumn.Index); 
                }
                else
                {
                currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
            
                var currColStyle      =currCell.Element.currentStyle;
                GetToolBarItems("R");
                SetCurrStyleValues(currColStyle,"R")
                HeaderText ="";
                SetToolBarItemsValues("R")
            }
            //================================================================
            else if (currToolBar == "F")
            {
                var currFooterStyle      
                try
                {
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                currFooterStyle= col._getFootTags()[0].currentStyle;}
                }
                catch(e)
                {}
            
                GetToolBarItems("F");
                SetCurrStyleValues(currFooterStyle,"F")
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                HeaderText = col._getFootTags()[0].outerText; }
                width = col.getWidth();
                SetToolBarItemsValues("F")
            }
            else if (currToolBar == "H")
            {
                var currHeaderStyle
                try
                {
                   if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)      
                       currHeaderStyle= col._getHeadTags()[0].currentStyle;
                }
                catch(e)
                {}
                GetToolBarItems("H");
                SetCurrStyleValues(currHeaderStyle,"H")
                if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)
                HeaderText = col._getHeadTags()[0].outerText;
                width = col.getWidth();
                SetToolBarItemsValues("H")
            }
            
            FillStyleOnType(col.DataType,col.MaskDisplay);
            var ddlSort = window.document.getElementById("ddlSort") //finish
            if (col.SortIndicator == 1)
                   ddlSort.value = 1
            else if(col.SortIndicator == 2)
                   ddlSort.value = 2
            else
                   ddlSort.value = "0"
                
    }
}
//---------------------------------------------------------------------
function ResetToolBar()
{
    GetToolBarItems("H");
    
    var headerTextI      = window.document.getElementById("txtHeader");
    headerTextI.value= "";
    WidthI.setText("");
    HeightI.setText("");
    
    borderWidthI.setText("")
    borderColorI.value = "-1"
    borderStyleI.value = "-1"
        
    verticalAlginI.value = 0;
        
    marginsI.setText("");
    paddingI.setText("");
    
    boldI.setSelected(false);
    italicI.setSelected(false);
    underlineI.setSelected(false);
        
    backcolorI.value = "-1"
    foreColorI.value = "-1"
    fontSizeI.value = "-1"
    fontNameI.value = "-1"
    
}
//---------------------------------------------------------------------
function GetGlobalGridStyle(currStyle,grid,sectionType)
{
    var currCol;
    var currColStyle
    for(i=1;i<grid.Bands[0].Columns.length;i++)
    {
        currCol = grid.Bands[0].Columns[i]
        if(sectionType == "H")
            if (currCol._getHeadTags()== null || currCol._getHeadTags()== undefined)
                continue ;
            else                
                currColStyle = currCol._getHeadTags()[0].currentStyle
        else if (sectionType == "F")
        if (currCol._getFootTags()== null || currCol._getFootTags()== undefined)
                continue ;
            else                
               currColStyle = currCol._getFootTags()[0].currentStyle
        else
        {
            var firstCell = igtbl_getCellById(gridName+"_rc_0_"+currCol.Index)
            if (firstCell.Element == null || firstCell.Element == undefined )
                continue ;
            else
                currColStyle = firstCell.Element.currentStyle;
        }
        if(currColStyle.backgroundColor!= currStyle.backgroundColor)
        {
           backgroundColor = "" 
        }
        if(currColStyle.borderColor!= currStyle.borderColor)
        {
            borderColor = ""
        }
        if(currColStyle.borderBottomColor != currStyle.borderBottomColor)
        {
            borderBottomColor = ""
        }
        if(currColStyle.borderTopColor!= currStyle.borderTopColor)
        {    
            borderTopColor = ""
        }
        if (currColStyle.borderLeftColor!= currStyle.borderLeftColor)
        {
            borderLeftColor = ""
        }
        if(currColStyle.borderRightColor!= currStyle.borderRightColor)
        {
            borderRightColor = ""
        }
        if(currColStyle.borderStyle!= currStyle.borderStyle)
        {
            borderStyle = "none"
        }
        if(currColStyle.borderTopStyle!= currStyle.borderTopStyle)
        {
           borderTopStyle ="none"
        }
        if(currColStyle.borderLeftStyle!= currStyle.borderLeftStyle)
        {
           borderLeftStyle = "none"
        }
        if(currColStyle.borderRightStyle!= currStyle.borderRightStyle)
        {
           borderRightStyle="none"
        }
        if(currColStyle.borderBottomStyle!= currStyle.borderBottomStyle)
        {
           borderBottomStyle = "none" 
        }
        if(currColStyle.borderWidth!= currStyle.borderWidth)
        {
           borderWidth = ""
        }
        if (currColStyle.borderTopWidth!= currStyle.borderTopWidth)
        {
           borderTopWidth = ""
        }
        if (currColStyle.borderBottomWidth!= currStyle.borderBottomWidth)
        {
           borderBottomWidth = ""
        }
        if (currColStyle.borderRightWidth!= currStyle.borderRightWidth)
        {
           borderRightWidth = ""
        }
        if (currColStyle.borderLeftWidth!= currStyle.borderLeftWidth)
        {
           borderLeftWidth = ""
        }
        if(currColStyle.fontFamily!= currStyle.fontFamily)
        {
           fontFamily = "-1" 
        }
        if(currColStyle.fontSize!= currStyle.fontSize)
        {
           fontSize = "-1"
        }
        if(currColStyle.fontStyle.bold != currStyle.fontStyle.bold)
        {
           fontStyle_bold = false
        }
        if(currColStyle.fontStyle != currStyle.fontStyle)
        {
           fontStyle = "normal"
        }
        if(currColStyle.textDecoration != currStyle.textDecoration)
        {
           textDecorationUnderline = false; 
        }
        if (currColStyle.color != currStyle.color)
        {
           foreColor = "" 
        }
        if(currColStyle.height!=currStyle.height)
        {
           height = ""
        }
        if(currColStyle.textAlign != currStyle.textAlign)
        {
           textAlign = "auto" 
        }
        if(currColStyle.margin !=currStyle.margin)
        {
           margin = ""
        }
        if(currColStyle.padding !=currStyle.padding)
        {
           padding = "" 
        }
        if(currColStyle.marginTop!= currStyle.marginTop)
        {
           marginTop = "" 
        }
        if(currColStyle.marginBottom!= currStyle.marginBottom)
        {
           marginBottom = "" 
        }
        if(currColStyle.marginLeft!= currStyle.marginLeft)
        {
           marginLeft = "" 
        }
        if(currColStyle.marginRight!= currStyle.marginRight)
        {
           marginRight = "" 
        }
        if(currColStyle.paddingTop!= currStyle.paddingTop)
        {
           paddingTop =""  
        }
        if(currColStyle.paddingBottom!= currStyle.paddingBottom)
        {
           paddingBottom =""  
        }
        if(currColStyle.paddingRight!= currStyle.paddingRight)
        {
           paddingRight =""
             
        }
        if(currColStyle.paddingLeft!= currStyle.paddingLeft)
        {
           paddingLeft = ""  
        }
        if(currColStyle.verticalAlign!= currStyle.verticalAlign)
        {
            verticalAlign = "auto"
        }
    }//End For
}
//---------------------------------------------------------------------
function FillStyleOnType(type,strFormat)
{
    var dateTypeTxt    = ["Default Long Format","Default Short Format","3/1/99 13:20","3/1/99 1:20pm",
                            "01-Mar-1999 13:20","01-Mar-1999 1:20pm","March 1, 1999 13:20",
                             "March 1, 1999 1:20pm","13:20 3/1/99","1:20pm 01-Mar-1999",
                             "13:20","1:20pm","13:20:45","1:20:45pm","3/1","3/01","3/1/99","03/01/1999",
                             "01 - Mar","1-Mar-99","1-Mar-1999","01-Mar-1999","01-March-1999",
                             "Mar-99","March 1999","March 01, 1999","Monday, March 1, 1999",
                             "Monday, 1 March, 1999","3","3-99"]
    
    var dateTypeVal    = ["dddd, dd MMMM yyyy","MM/dd/yyyy","MM/dd/yy H:mm","MM/dd/yy h:mm tt",
                            "dd-MMM-yyyy H:mm","dd-MMM-yyyy h:mm tt","MMMM dd, yyyy H:mm",
                            "MMMM dd, yyyy h:mm tt","HH:mm MM/dd/yy","h:mm tt dd-MMM-yyyy",
                            "H:mm","h:mm tt","H:mm:ss","h:mm:ss tt","M/d","M/dd","M/d/yy","MM/dd/yyyy",
                            "dd - MMM","dd-MMM-yy","d-MMM-yyyy","dd-MMM-yyyy","dd-MMMM-yyyy",
                            "MMM-yy","MMMM yyyy","MMMM dd, yyyy","dddd, MMMM dd, yyyy",
                            "dddd, dd MMMM, yyyy","M","M-yy"]
                            
   var numberTypeTxt   = ["Default Number Format","-1123","-1,123","-1123.00","-1,123.00","-1123.0000",
                          "-1,123.0000","(1123)","(1,123)","((1123.00))","((1,123.00))","((1123.0000))",
                          "((1,123.0000))"]
   
   var numberTypeVal   = ["#,###.#","####","#,###","####.##","#,###.##","####.####",
                          "#,###.####","(####)","(#,###)","(####.##)","(#,###.##)","(####.####)",
                          "(#,###.##)"]
   
   var ddlStyle = window.document.getElementById("ddlStyle")                       
   ddlStyle.length = 0
   var txtFormat   = window.document.getElementById("txtFormat")//ig
   txtFormat.value = ""
   if (type == 7 || type == 111 || type == 61)
   {
         len = dateTypeTxt.length
         ddlStyle.length = len+1
         
         ddlStyle.options[0].innerText = "Select Your Choice"
         ddlStyle.options[0].value = ""
         for (i=0;i<len;i++)
         {
                ddlStyle.options[i+1].innerText = dateTypeTxt[i]
                ddlStyle.options[i+1].value     = dateTypeVal[i]
         }
   }
   else if (type ==2||type ==3 || type == 4 || type == 14)
   {
   
        len = numberTypeTxt.length
        ddlStyle.length = len+1
        ddlStyle.options[0].innerText = "Select Your Choice"
        ddlStyle.options[0].value = ""
        for (i=0;i<len;i++)
        {
                ddlStyle.options[i+1].innerText = numberTypeTxt[i]
                ddlStyle.options[i+1].value     = numberTypeVal[i]
        }
         
   }
   else
   {
        ddlStyle.length = 0;
   }
   
   if (strFormat != null || strFormat != undefined || ddlStyle.length !=0)
      {
       ddlStyle.value = strFormat
      }
   
   
                          
  
}
//---------------------------------------------------------------------
function StyleChange(tlbId)
{
    var ddlStyle    = window.document.getElementById("ddlStyle")
    var val         = ddlStyle.value;
    var txtFormat   = window.document.getElementById("txtFormat");
    txtFormat.value = val;
    SetSelectedColumnFormat(val);
    
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function SetSelectedColumnFormat(format)
{
    if (selectedColumnIndex != -1)
    {
            var col         = igtbl_getColumnById(gridName+"_c_0_"+selectedColumn.Index);
            if(format != null && format != "")
                col.MaskDisplay = format;
    }
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function txtFormatChange()
{
    var txtFormat   = window.document.getElementById("txtFormat")
    var val = txtFormat.value ;
    if (val != null && val != "") 
        SetSelectedColumnFormat(val);
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function ddlCalculatedSummaryChange()
{
    var ddlCalculatedSummary = window.document.getElementById("ddlCalculatedSummary")
    var val = ddlCalculatedSummary.value
    if (selectedColumnIndex != -1)
    {
            var col         = igtbl_getColumnById(gridName+"_c_0_"+selectedColumn.Index);
            
    }  
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function GetToolBarItems(tlbBarType)
{
           var tlbName;
           var no;
           var no2;
           if (tlbBarType == "H")
           { 
                 tlbName          = headerBarName;
                 headerTextI      = window.document.getElementById("txtHeader");
                 no = 1;
                 no2 = 4;
           }
           else if (tlbBarType == "R")
           {
                tlbName           = rowBarName;
                no = 1;
                no2 = 4;
           }
           else if (tlbBarType == "F")
           {     
                tlbName           = footerBarName;
                headerTextI      = window.document.getElementById("txtHeader");
                no = 1;
                no2 = 4;
           }
             
           tlbBarType = "H"
                       
            HeightI         = igedit_getById("txtHeight"+"H" );
            WidthI          = igedit_getById("txtWidth"+"H");
            
            borderTopI      = igtbar_getItemById(tlbName+"_Item_"+iborderTopI).Items[0];  
            borderLeftI     = igtbar_getItemById(tlbName+"_Item_"+iborderLeftI).Items[1];
            borderRightI    = igtbar_getItemById(tlbName+"_Item_"+iborderRightI).Items[2];
            borderBottomI   = igtbar_getItemById(tlbName+"_Item_"+iborderBottomI).Items[3];
            
            borderColorI      = window.document.getElementById("ddlBorderColor"+tlbBarType);
            borderWidthI    = igedit_getById("txtBorderWidth"+tlbBarType);
           
            borderStyleI    = window.document.getElementById("wcbLineStyle"+tlbBarType);
            
            marginsI        = igedit_getById("txtMargins"+tlbBarType); 
            paddingI        = igedit_getById("txtPadding"+tlbBarType);
            
            
            txtAlginLeftI   = igtbar_getItemById(tlbName+"_Item_"+itxtAlginLeftI).Items[0];
            txtAlignCenterI = igtbar_getItemById(tlbName+"_Item_"+itxtAlignCenterI).Items[1];
            txtAlignRightI  = igtbar_getItemById(tlbName+"_Item_"+itxtAlignRightI).Items[2];
            
            verticalAlginI  = window.document.getElementById("DropDownList"+no);
            
            boldI           = igtbar_getItemById(tlbName+"_Item_"+iboldI);
            italicI         = igtbar_getItemById(tlbName+"_Item_"+iitalicI);
            underlineI      = igtbar_getItemById(tlbName+"_Item_"+iunderlineI);
            
            backcolorI      = window.document.getElementById("ddlBackColor"+tlbBarType)
            foreColorI      = window.document.getElementById("ddlForeColor"+tlbBarType)
            
            fontSizeI       = window.document.getElementById("DropDownList"+no2);
            fontNameI       = window.document.getElementById("ddlFontslist"+tlbBarType);
  
}
//---------------------------------------------------------------------
function SetTag(vcolor,vwidth,vstyle)
{
    var str = new String();
    str =vcolor+","+TrimPx(vwidth);
    if (vstyle == "auto")
        str+= "," + "notset";
    else
        str+= ","+vstyle;
    
    return str;
}
//---------------------------------------------------------------------
function TrimPx(par)
{
    if (par == null)
        return "";
    var str = new String();
    str = par;
    var pind = str.indexOf("p");
    var retStr = new String();
    if (pind == -1)
        retStr = str;
    else
    {
       retStr = str.substr(0,pind); 
    } 
   
    return retStr;
}
//---------------------------------------------------------------------
function SetToolBarItemsValues(tlbBarType)
{
    //tlbBar { H || R || F }
    var tlbName;
    if (tlbBarType == "H")
        tlbName     = headerBarName;
    else if (tlbBarType == "R")
        tlbName     = rowBarName;
    else if (tlbBarType == "F")
        tlbName     = footerBarName;
    
    tlbBarType = "H";
    if ( (tlbBarType == "H"  ) && selectedColumnIndex != -1)
    {
        headerTextI.value= HeaderText;
        WidthI.setText(width);
    }
    else if (tlbBarType == "R" )
    {
        WidthI.setText(width);
    }
    else if (tlbBarType == "F")
    {
        headerTextI.value= HeaderText;
        WidthI.setText(width);
    }
    HeightI.setText(TrimPx(height));
    
    if(textAlign == "left")
    {
       txtAlginLeftI.setSelected(true);        
    }
    else if (textAlign == "right")
    {
       txtAlignRightI.setSelected(true);
    }
    else if (textAlign == "center")
    {
       txtAlignCenterI.setSelected(true);
    }
        
    borderTopI.setTag(SetTag(borderTopColor,borderTopWidth,borderTopStyle));
    borderBottomI.setTag(SetTag(borderBottomColor,borderBottomWidth,borderBottomStyle));
    borderRightI.setTag(SetTag(borderRightColor,borderRightWidth,borderRightStyle));
    borderLeftI.setTag(SetTag(borderLeftColor,borderLeftWidth,borderLeftStyle));
    
    
    var borderButtons   = igtbar_getItemById(tlbName+"_Item_"+iborderTopI);
    if (borderButtons.SelectedButton != null)
    {
        var tag = borderButtons.SelectedButton.Tag;
        var tagArr = tag.split(",");
        
        var cborderColorI    = window.document.getElementById("ddlBorderColor"+"H");
        var cborderWidthI    = igedit_getById("txtBorderWidth"+"H");
        var cborderStyleI    = window.document.getElementById("wcbLineStyle"+"H");
        
        cborderColorI.value = tagArr[0];
        cborderWidthI.setText(tagArr[1]);
        cborderStyleI.value = tagArr[2];
    }
    else
    {
        
    }
   
    if (verticalAlign == "middle")
       verticalAlginI.value = 2;
    else if (verticalAlign == "top")
        verticalAlginI.value = 1;
    else if (verticalAlign == "bottom")
        verticalAlginI.value = 3;
    else
         verticalAlginI.value = 0;
    
        
    var marginVal; 
    var paddingVal;
    if (margin == "auto")
        marginVal ="0px"
    else
        marginVal =margin;
        
    if (padding == "auto")
        paddingVal ="0px"
    else
        paddingVal =padding;
     
    marginsI.setText(TrimPx(marginVal));
    paddingI.setText(TrimPx(paddingVal));
    
    if (fontStyle_bold)
        boldI.setSelected(true);
    else
        boldI.setSelected(false);
    
    if (fontStyle == "italic")
        italicI.setSelected(true);
    else
        italicI.setSelected(false);
    
    if (textDecorationUnderline)
        underlineI.setSelected(true);
    else
        underlineI.setSelected(false);
    
    /*
    var strFS = new String()
    strFS = fontSize;
    strFS = strFS.substr(0,2);
    if(strFS.indexOf("p")!=-1)
        strFS = strFS.substr(0,1);
    */
    
    backcolorI.value = backgroundColor
    
    fontSizeI.value = TrimPx(fontSize);
    
    fontNameI.value = fontFamily
    
    
}
//---------------------------------------------------------------------
function SetCurrStyleValues(currStyle,tlbBarType)
{
             //tlbBar { H || R || F }      
            try
            { 
            backgroundColor= currStyle.backgroundColor
            borderColor= currStyle.borderColor
            borderBottomColor= currStyle.borderBottomColor
            borderTopColor= currStyle.borderTopColor
            borderLeftColor= currStyle.borderLeftColor
            borderRightColor= currStyle.borderRightColor
            borderStyle= currStyle.borderStyle
            borderTopStyle= currStyle.borderTopStyle
            borderLeftStyle= currStyle.borderLeftStyle
            borderRightStyle= currStyle.borderRightStyle
            borderBottomStyle= currStyle.borderBottomStyle
            borderWidth= currStyle.borderWidth
            borderTopWidth= currStyle.borderTopWidth
            borderBottomWidth= currStyle.borderBottomWidth
            borderLeftWidth= currStyle.borderLeftWidth
            borderRightWidth= currStyle.borderRightWidth
            
            
            fontFamily= currStyle.fontFamily           
            fontSize= currStyle.fontSize
            //TmpChange [Start] 
            fontStyle_bold=false             
            fontStyle= currStyle.fontStyle
            if (currStyle.textDecoration =="underline")
               textDecorationUnderline = true
            else
                textDecorationUnderline = false
                  
            textDecorationOverline= currStyle.textDecorationOverline
            foreColor= currStyle.color;
            height= currStyle.height
            textAlign= currStyle.textAlign
            margin= currStyle.margin
            marginTop= currStyle.marginTop
            marginBottom= currStyle.marginBottom
            marginLeft= currStyle.marginLeft
            marginRight= currStyle.marginRight
            padding= currStyle.padding
            paddingTop= currStyle.paddingTop
            paddingBottom= currStyle.paddingBottom
            paddingRight= currStyle.paddingRight
            paddingLeft= currStyle.paddingLeft
            textOverflow= currStyle.textOverflow
            overflow= currStyle.overflow
            verticalAlign= currStyle.verticalAlign
           if( (tlbBarType == "H" || tlbBarType == "F") && selectedColumnIndex != -1)
            {
               //HeaderText= currStyle.getHeaderText(); 
            }
            else if ((tlbBarType == "H" || tlbBarType == "F") && selectedColumnIndex == -1)
            {
                HeaderText = "";
            }
            }
            catch(e){}
       

}
//---------------------------------------------------------------------
function ccSetCurrStyleValues(currStyle,tlbBarType)
{
             //tlbBar { H || R || F }
            try
            { 
            ccbackgroundColor= currStyle.backgroundColor
            
            ccborderColor       = currStyle.borderColor
            ccborderBottomColor = currStyle.borderBottomColor
            ccborderTopColor    = currStyle.borderTopColor
            ccborderLeftColor   = currStyle.borderLeftColor
            ccborderRightColor  = currStyle.borderRightColor
            
            ccborderWidth       = currStyle.borderWidth
            ccborderTopWidth    = currStyle.borderTopWidth
            ccborderBottomWidth = currStyle.borderBottomWidth
            ccborderLeftWidth   = currStyle.borderLeftWidth
            ccborderRightWidth  = currStyle.borderRightWidth
            
            ccborderStyle       = currStyle.borderStyle
            ccborderTopStyle    = currStyle.borderTopStyle
            ccborderLeftStyle   = currStyle.borderLeftStyle
            ccborderRightStyle  = currStyle.borderRightStyle
            ccborderBottomStyle = currStyle.borderBottomStyle
            
            if(ccborderWidth == "medium")
            {
              ccborderWidth = "0px"  
            }
            if(ccborderTopWidth == "medium")
            {
              ccborderTopWidth = "0px"  
            }
            if(ccborderBottomWidth == "medium")
            {
              ccborderBottomWidth = "0px"  
            }
            if(ccborderLeftWidth == "medium")
            {
              ccborderLeftWidth = "0px"  
            }
            if(ccborderRightWidth == "medium")
            {
              ccborderRightWidth = "0px"  
            }
            ccfontFamily= currStyle.fontFamily
            ccfontSize= currStyle.fontSize
            
            //Tmp Change [Start] 
            ccfontStyle_bold=false             
            ccfontStyle= currStyle.fontStyle
            if (currStyle.textDecoration =="underline")
               cctextDecorationUnderline = true
            else
               cctextDecorationUnderline = false
                  
            cctextDecorationOverline= currStyle.textDecorationOverline
            ccforeColor= currStyle.color;
            ccheight= currStyle.height
            cctextAlign= currStyle.textAlign
            ccmargin= currStyle.margin
            ccmarginTop= currStyle.marginTop
            ccmarginBottom= currStyle.marginBottom
            ccmarginLeft= currStyle.marginLeft
            ccmarginRight= currStyle.marginRight
            ccpadding= currStyle.padding
            ccpaddingTop= currStyle.paddingTop
            ccpaddingBottom= currStyle.paddingBottom
            ccpaddingRight= currStyle.paddingRight
            ccpaddingLeft= currStyle.paddingLeft
            cctextOverflow= currStyle.textOverflow
            ccoverflow= currStyle.overflow
            ccverticalAlign= currStyle.verticalAlign
           
            if( (tlbBarType == "H" || tlbBarType == "F") && selectedColumnIndex != -1)
            {
               //HeaderText= currStyle.getHeaderText(); 
            }
            else if ((tlbBarType == "H" || tlbBarType == "F") && selectedColumnIndex == -1)
            {
                ccHeaderText = "";
            }
            }
            catch(e){}

}
//---------------------------------------------------------------------
function ApplyOneStyleChanges(ctlbType)
{
    var tlbType = ctlbType
    
    var currtxtStyle;
    if (ctlbType == "H")
    {
        currtxtStyle = strHeaderStyle
        
    }
    else if (ctlbType == "R")
    {
        currtxtStyle = strRowStyle
        tlbType = "RS"
    }
    else if (ctlbType == "F")
    {
        currtxtStyle = strFooterStyle
    }
    
    
    
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_W",ccwidth)   
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_H",TrimPx(ccheight))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BKC",ccbackgroundColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FC",ccforeColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BS",ccborderStyle.split(" ")[0])
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BSL",ccborderLeftStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BSR",ccborderRightStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BST",ccborderTopStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BSB",ccborderBottomStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BW",TrimPx(ccborderWidth.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BWL",TrimPx(ccborderLeftWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BWR",TrimPx( ccborderRightWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BWT",TrimPx( ccborderTopWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BWB",TrimPx( ccborderBottomWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BC",ccborderColor.split(" ")[0])
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BCL",ccborderLeftColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BCR",ccborderRightColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BCT",ccborderTopColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_BCB",ccborderBottomColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_P",TrimPx( ccpadding.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_M",TrimPx( ccmargin.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_HA",cctextAlign)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_VA",ccverticalAlign)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FN",ccfontFamily)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FS",TrimPx(ccfontSize))
    var bool
    if (ccfontStyle_bold)
        bool = "True"
    else
        bool = "False"
        
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FIB",bool)
    
    if (ccfontStyle == "italic")
        bool = "True"
    else
        bool = "False"
        
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FII",bool)
    
   if (cctextDecorationUnderline)
        bool = "True"
    else
        bool = "False"
        
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"_FIU",bool) 
    
    
    if (ctlbType == "H")
         strHeaderStyle = currtxtStyle
    else if (ctlbType == "R")
        strRowStyle     = currtxtStyle
    else if (ctlbType == "F")
        strFooterStyle  = currtxtStyle
    
    SetAllStyles()
    
    
}
//---------------------------------------------------------------------
function ApplyOneColumnStyleChanges(ctlbType)
{
    var tlbType = selectedColumn.Key+"_"
    
    var txtColumnsStyles = window.document.getElementById("txtColumnsStyles")
    var currtxtStyle = txtColumnsStyles.value;
    var sss = new String()
    
    if (ctlbType == "H")
    {
         tlbType +="CH" 
         currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"TXT",ccHeaderText)
    }
    else if (ctlbType == "R")
    {    
        tlbType +="CC" 
    }
    else if (ctlbType == "F")
    {
        tlbType +="CF" 
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"TXT",ccHeaderText)
    }
    
    
    
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"W",ccwidth)   
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"H",TrimPx(ccheight))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BKC",ccbackgroundColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FC",ccforeColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BS",ccborderStyle.split(" ")[0])
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSL",ccborderLeftStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSR",ccborderRightStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BST",ccborderTopStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSB",ccborderBottomStyle)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BW",TrimPx(ccborderWidth.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWL",TrimPx(ccborderLeftWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWR",TrimPx( ccborderRightWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWT",TrimPx( ccborderTopWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWB",TrimPx( ccborderBottomWidth))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BC",ccborderColor.split(" ")[0])
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCL",ccborderLeftColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCR",ccborderRightColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCT",ccborderTopColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCB",ccborderBottomColor)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"P",TrimPx( ccpadding.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"M",TrimPx( ccmargin.split(" ")[0]))
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"HA",cctextAlign)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"VA",ccverticalAlign)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FN",ccfontFamily)
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FS",TrimPx(ccfontSize))
    var bool
    if (ccfontStyle_bold)
        bool = "True"
    else
        bool = "False"
        
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FIB",bool)
    
    if (ccfontStyle == "italic")
        bool = "True"
    else
        bool = "False"
        
    currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FII",bool)
    
   if (cctextDecorationUnderline)
        bool = "True"
    else
        bool = "False"
        
   currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FIU",bool) 
   
   if(ccFormat != null && ccFormat != "")
        currtxtStyle = SetValueFromString(currtxtStyle,selectedColumn.Key+"_CFORMAT",ccFormat) 
   
   if (ccShowCurrencySymbol)
        bool = "1"
   else
        bool ="0"
   currtxtStyle = SetValueFromString(currtxtStyle,selectedColumn.Key+"_CCCURR",bool)
   
   if(ccCalculatedSummary=="-1")
        ccCalculatedSummary = "0"
   currtxtStyle = SetValueFromString(currtxtStyle,selectedColumn.Key+"_CFTOTAL",ccCalculatedSummary)
   
   if (ccSort == "0")
        ccSort = "0"
   currtxtStyle = SetValueFromString(currtxtStyle,selectedColumn.Key+"_CSORT",ccSort)       
    
   txtColumnsStyles.value = currtxtStyle;
    
    
}
//---------------------------------------------------------------------
function ApplyAllColumnStyleChanges(ctlbType)
{
    var grid = igtbl_getGridById(gridName);
    var currCol;
    var txtColumnsStyles = window.document.getElementById("txtColumnsStyles")
    var currtxtStyle = txtColumnsStyles.value;
    for(i=0;i<grid.Bands[0].Columns.length;i++)
    {
        currCol = grid.Bands[0].Columns[i]
        var tlbType = currCol.Key+"_"
    
        
        var sss = new String()
    
        if (ctlbType == "H")
        {
            var currHeaderStyle
            try
            {
                if (currCol._getHeadTags()!= null && currCol._getHeadTags()!= undefined)
                    currHeaderStyle = currCol._getHeadTags()[0].currentStyle;
            }
            catch(e){}
            ccSetCurrStyleValues(currHeaderStyle,"H") 
            if (currCol._getHeadTags()!= null && currCol._getHeadTags()!= undefined)
                ccbackgroundColor = currCol._getHeadTags()[0].style.backgroundColor   
            tlbType +="CH" 
             }
        else if (ctlbType == "R")
        {   
                var currRow 
                var currCell;   
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_0");
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
                var rowIndex    = currRow.getIndex();
                if(rowIndex != -1)
                {
                    currCell = igtbl_getCellById(gridName+"_rc_"+rowIndex+"_"+currCol.Index); 
                }
                else
                {
                    currCell = igtbl_getCellById(gridName+"_anc_"+currCol.Index);
                }
            
            if (currCell.Element != null && currCell.Element != undefined){
                var currColStyle      =currCell.Element.currentStyle;
                    ccSetCurrStyleValues(currColStyle,"R")
                                 }
            

                if (currCell.Element != null && currCell.Element != undefined){
                ccbackgroundColor = currCell.Element.style.backgroundColor }
                tlbType +="CC" 
        }
        else if (ctlbType == "F")
        {
            var currFooterStyle      
            try
            {

            if (currCol._getFootTags() != null && currCol._getFootTags() != undefined){
                    currFooterStyle= currCol._getFootTags()[0].currentStyle; }
            
            }
            catch(e)
            {}
            ccSetCurrStyleValues(currFooterStyle,"F")
            if (currCol._getFootTags() != null && currCol._getFootTags() != undefined){
            ccbackgroundColor = currCol._getFootTags()[0].style.backgroundColor ; }
            tlbType +="CF" 
        }
        ccwidth = currCol.getWidth();
     
           
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"W",ccwidth)   
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"H",TrimPx(ccheight))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BKC",ccbackgroundColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FC",ccforeColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BS",ccborderStyle.split(" ")[0])
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSL",ccborderLeftStyle)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSR",ccborderRightStyle)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BST",ccborderTopStyle)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BSB",ccborderBottomStyle)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BW",TrimPx(ccborderWidth.split(" ")[0]))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWL",TrimPx(ccborderLeftWidth))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWR",TrimPx( ccborderRightWidth))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWT",TrimPx( ccborderTopWidth))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BWB",TrimPx( ccborderBottomWidth))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BC",ccborderColor.split(" ")[0])
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCL",ccborderLeftColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCR",ccborderRightColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCT",ccborderTopColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"BCB",ccborderBottomColor)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"P",TrimPx( ccpadding.split(" ")[0]))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"M",TrimPx( ccmargin.split(" ")[0]))
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"HA",cctextAlign)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"VA",ccverticalAlign)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FN",ccfontFamily)
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FS",TrimPx(ccfontSize))
        var bool
        if (ccfontStyle_bold)
            bool = "True"
        else
            bool = "False"
        
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FIB",bool)
        
        if (ccfontStyle == "italic")
            bool = "True"
        else
            bool = "False"
            
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FII",bool)
    
        if (cctextDecorationUnderline)
            bool = "True"
        else
            bool = "False"
        
        currtxtStyle = SetValueFromString(currtxtStyle,tlbType+"FIU",bool) 
   
        if (ccFormat != null && ccFormat != "")
            currtxtStyle = SetValueFromString(currtxtStyle,currCol.Key+"_CFORMAT",ccFormat) 
   
        if (ccShowCurrencySymbol)
            bool = "1"
        else
            bool ="0"
        currtxtStyle = SetValueFromString(currtxtStyle,currCol.Key+"_CCCURR",bool)
   
        if(ccCalculatedSummary=="-1")
            ccCalculatedSummary = "0"
        currtxtStyle = SetValueFromString(currtxtStyle,currCol.Key+"_CFTOTAL",ccCalculatedSummary)
   
        if (ccSort == "0")
            ccSort = "0"
        currtxtStyle = SetValueFromString(currtxtStyle,currCol.Key+"_CSORT",ccSort)       
    
        txtColumnsStyles.value = currtxtStyle;
    
    }//End For
}
//---------------------------------------------------------------------
function ApplyGridChanges()
{
    var grid           = igtbl_getGridById(gridName);
    if (selectedColumnIndex == -1)
    {
            var col                  = igtbl_getColumnById(gridName+"_c_0_0");
            if (currToolBar == "R")
            {
                var currRow 
                var currCell;   
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_0");
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
                var rowIndex    = currRow.getIndex();
                if(rowIndex != -1)
                {
                    currCell = igtbl_getCellById(gridName+"_rc_"+rowIndex+"_0"); 
                }
                else
                {
                    currCell = igtbl_getCellById(gridName+"_anc_0");
                }
            
                var currColStyle      =currCell.Element.currentStyle;
                
                ccwidth = col.getWidth();
                ccSetCurrStyleValues(currColStyle,"R")
                ApplyOneStyleChanges("R")
                ApplyAllColumnStyleChanges("R")
                
                
            }
            //================================================================
            else if (currToolBar == "F")
            {
                var currFooterStyle      
                try
                {
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                    currFooterStyle= col._getFootTags()[0].currentStyle; }
            
                }
                catch(e)
                {}
            
                ccwidth = col.getWidth();
                ccSetCurrStyleValues(currFooterStyle,"F")
                ApplyOneStyleChanges("F")
                ApplyAllColumnStyleChanges("F")
            }
            //================================================================
            else if (currToolBar == "H")
            {
                var currHeaderStyle
                try
                {
                    if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)
                        currHeaderStyle = col._getHeadTags()[0].currentStyle;
                }
                catch(e){}
               ccwidth = col.getWidth();
               ccSetCurrStyleValues(currHeaderStyle,"H")
               ApplyOneStyleChanges("H")
               ApplyAllColumnStyleChanges("H")
            }
          
    }
    else
    {
            var col                  = selectedColumn;
            var showCurrency         = igtbar_getItemById("uwtGridProperties_Item_16").Selected;
            var sortNum              = window.document.getElementById("ddlSort").value
            if (currToolBar == "R")
            {
                var currRow 
                var currCell;   
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_0");
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
                var rowIndex    = currRow.getIndex();
                if(rowIndex != -1)
                {
                    currCell = igtbl_getCellById(gridName+"_rc_"+rowIndex+"_"+selectedColumn.Index); 
                }
                else
                {
                    currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
            
                var currColStyle      =currCell.Element.currentStyle;
                
                ccwidth                     = col.getWidth();
                ccHeaderText                =""
                ccFormat                    = window.document.getElementById("txtFormat").value
                ccCalculatedSummary         = window.document.getElementById("ddlCalculatedSummary").value
                ccShowCurrencySymbol        = igtbar_getItemById("uwtGridProperties_Item_16").Selected;
                ccSort                      = window.document.getElementById("ddlSort").value
                ccSetCurrStyleValues(currColStyle,"R")
                if (currCell.Element != null && currCell.Element != undefined){
                ccbackgroundColor = currCell.Element.style.backgroundColor}
                
                ApplyOneColumnStyleChanges("R")
                
                
            }
            //================================================================
            else if (currToolBar == "F")
            {
                var currFooterStyle      
                try
                {
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                    currFooterStyle= col._getFootTags()[0].currentStyle; }
            
                }
                catch(e)
                {}
            
                ccwidth                     = col.getWidth();
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                ccHeaderText                = col._getFootTags()[0].outerText ; } 
                ccFormat                    = window.document.getElementById("txtFormat").value
                ccCalculatedSummary         = window.document.getElementById("ddlCalculatedSummary").value
                ccShowCurrencySymbol        = igtbar_getItemById("uwtGridProperties_Item_16").Selected;
                ccSort                      = window.document.getElementById("ddlSort").value
                ccSetCurrStyleValues(currFooterStyle,"F")
                if (col._getFootTags() != null && col._getFootTags() != undefined){
                ccbackgroundColor = col._getFootTags()[0].style.backgroundColor ; } 
                
                ApplyOneColumnStyleChanges("F")
            }
            //================================================================
            else if (currToolBar == "H")
            {
                var currHeaderStyle
                try
                {
                if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)
                    currHeaderStyle = col._getHeadTags()[0].currentStyle;
                }
                catch(e){}
               ccwidth                      = col.getWidth();
               if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)
                    ccHeaderText                 = col._getHeadTags()[0].outerText
               ccFormat                     = window.document.getElementById("txtFormat").value
               ccCalculatedSummary          = window.document.getElementById("ddlCalculatedSummary").value
               ccShowCurrencySymbol         = igtbar_getItemById("uwtGridProperties_Item_16").Selected;
               ccSort                       = window.document.getElementById("ddlSort").value
               
               ccSetCurrStyleValues(currHeaderStyle,"H")
               if(col._getHeadTags()!= null && col._getHeadTags()!= undefined)
                ccbackgroundColor = col._getHeadTags()[0].style.backgroundColor
               
               ApplyOneColumnStyleChanges("H")
            }

    }
}
//---------------------------------------------------------------------
function uwtGridProperties_Click(oToolbar, oButton, oEvent)
{
	var grid = igtbl_getGridById(gridName);
	if (oButton.Index == 18)
	{
	     window.open("frmCalcFieldsFormulaDesigner.aspx?ControlName=txtTarget","_EE","height=318px,width=785px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
       
	}
	if (oButton.Index == 20)//Group By
    {
        if(oButton.Selected)
        {
            grid.SelectTypeColumn = 1
        }
        else
        {
            grid.SelectTypeColumn = 2
        }
       
    }
    ApplyGridChanges()
	
}
//---------------------------------------------------------------------
function Toolbar_Click(oToolbar, oButton, oEvent,tlbBarType)
{
    var grid = igtbl_getGridById(gridName) 
    if (oButton.IsGroupButton)   
    if (oButton.Parent.Index == 8)
    {
         var tag = oButton.Tag;
         if (tag != null && tag!="")
         {
            var tagArr = tag.split(",");
            
            var cborderColorI    = window.document.getElementById("ddlBorderColor"+"H")//igtbar_getItemById(oToolbar.Id+"_Item_"+iborderColorI);
            var cborderWidthI    = igedit_getById("txtBorderWidth"+"H");
            var cborderStyleI    = window.document.getElementById("wcbLineStyle"+"H");
            
            cborderColorI.value = tagArr[0];
            cborderWidthI.setText(tagArr[1]);
            cborderStyleI.value = tagArr[2];
         }
    }
    if(oButton.IsGroupButton)
    if(oButton.Parent.Index == 16)
    {
        //TextHAlignChange(tlbBarType);
        
        if (tlbBarType == "H")
        {
            if(selectedColumnIndex != -1)
            {
                
                if (oButton.Index == 1)
                {
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                     grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.textAlign = "left"
                }
                else if (oButton.Index == 2)
                {
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.textAlign = "center"
                }
                else if (oButton.Index == 3)
                {
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.textAlign = "right"
                }
            }
            else
            {
              for (i=0;i<grid.Bands[0].Columns.length ;i++)
               {
                    if (oButton.Index == 1)
                    {
                        if (grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.textAlign = "left"
                    }
                    else if (oButton.Index == 2)
                    {
                        if (grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.textAlign = "center"
                    }
                    else if (oButton.Index == 3)
                    {
                        if (grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.textAlign = "right"
                    }
               }    
                
            }
        }
        else if (tlbBarType == "R")
        {
            if(selectedColumnIndex != -1)
            {
                for (i=0;i<grid.Rows.length ;i++)
                {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                        continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    if (oButton.Index == 1)
                        currCell.Element.style.textAlign = "left";
                    else if (oButton.Index == 2)
                        currCell.Element.style.textAlign = "center";
                    else if (oButton.Index == 3)
                        currCell.Element.style.textAlign = "right";}
                                       
                   
        
                }//End For of Rows 
            }
            else
            {
                for (i=0;i<grid.Rows.length ;i++)
                {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                        continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (oButton.Index == 1)
                            currCell.Element.style.textAlign = "left";
                        else if (oButton.Index == 2)
                             currCell.Element.style.textAlign = "center";
                        else if (oButton.Index == 3)
                             currCell.Element.style.textAlign = "right";}
                                       
                    }//End For of Cells
        
                }//End For of Rows 
            }
        }
        else if (tlbBarType == "F")
        {
            if(selectedColumnIndex != -1)
            {
            if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                if (oButton.Index == 1)
                {
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.textAlign = "left"
                }
                else if (oButton.Index == 2)
                {
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.textAlign = "center"
                }
                else if (oButton.Index == 3)
                {
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.textAlign = "right"
                }
             }
            }
            else
            {
               for (i=0;i<grid.Bands[0].Columns.length ;i++)
               {
               if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                    if (oButton.Index == 1)
                    {
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.textAlign = "left"
                    }
                    else if (oButton.Index == 2)
                    {
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.textAlign = "center"
                    }
                    else if (oButton.Index == 3)
                    {
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.textAlign = "right"
                    }
                    }
               }
            }
        }
    }
    
    
    if(!oButton.IsGroupButton)
    {
        if(oButton.Index == 19)//Bold
        {
            SetFontStyle(oButton.Selected,tlbBarType,"Bold")
        }
        if(oButton.Index == 20)//italic
        {
            SetFontStyle(oButton.Selected,tlbBarType,"Italic")
        }
        if(oButton.Index == 21)//underline
        {
            SetFontStyle(oButton.Selected,tlbBarType,"Underline")
        }
    }
    
  ApplyGridChanges()  
}
//---------------------------------------------------------------------
function SetFontStyle(oButton,tlbType,styleType)
{
     tlbType = currToolBar;
     var grid = igtbl_getGridById(gridName)
     if (tlbType == "H")
        {
            if(selectedColumnIndex != -1)
            {
                if (styleType == "Bold")
                {
                    if (oButton)
                    {
                        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontStyle.bold = true;
                    }
                    else
                    {
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontStyle.bold = false;
                    }
                }
                else if (styleType == "Italic")
                {
                   if (oButton)
                   {
                   if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontStyle="italic";
                   }
                   else
                   {
                   if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontStyle="normal"; 
                   }
                }
                else if (styleType == "Underline")
                {
                    if (oButton)  
                    {  
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.textDecorationUnderline = true;
                    }
                    else
                    {
                    if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
                        grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.textDecorationUnderline = false;
                    }
                }
                
            }
            else
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                    if (styleType == "Bold")
                    {
                        if (oButton)
                        {
                        if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontStyle.bold = true;
                        }
                        else
                        {
                        if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontStyle.bold = false;
                        }
                    }
                    else if (styleType == "Italic")
                    {
                        if (oButton)
                        {
                        if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontStyle="italic";
                        }
                        else
                        {
                         if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontStyle="normal"; 
                        }
                    }
                    else if (styleType == "Underline")
                    {
                        if (oButton)  
                        {
                        if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)  
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.textDecorationUnderline = true;
                        }
                        else
                        {
                        if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                            grid.Bands[0].Columns[i]._getHeadTags()[0].style.textDecorationUnderline = false;
                        }
                    }
                }
            }
        }
        else if (tlbType == "R")
        {
            if(selectedColumnIndex != -1)
            {
                for (i=0;i<grid.Rows.length ;i++)
                {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                        continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    
                    if(rowIndex != -1)
                    {
                         currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                    }
                    else
                    {
                         currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                    }
           
                    var str = new String();
                    if (currCell.getValue() != null)
                        str     = currCell.getValue().toString();
                    else
                        continue;
           
           if (currCell.Element != null && currCell.Element != undefined){
                    if (styleType == "Bold")
                       if (oButton)
                           currCell.Element.style.fontStyle.bold = true;
                       else
                           currCell.Element.style.fontStyle.bold = false;
              
                    else if (styleType == "Italic")
                      if (oButton)
                           currCell.Element.style.fontStyle="italic";
                      else 
                           currCell.Element.style.fontStyle="normal";
                 
                    else if (styleType == "Underline") 
                       if (oButton)   
                            currCell.Element.style.textDecorationUnderline = true;
                       else
                            currCell.Element.style.textDecorationUnderline = false;
                    }
                    
        
                }//End For of Rows
            
            }
            else
            {
                for (i=0;i<grid.Rows.length ;i++)
                {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                        continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
           
                        var str = new String();
                        if (currCell.getValue() != null)
                            str     = currCell.getValue().toString();
                        else
                            continue;
           
           if (currCell.Element != null && currCell.Element != undefined){
                        if (styleType == "Bold")
                            if (oButton)
                                currCell.Element.style.fontStyle.bold = true;
                            else
                                currCell.Element.style.fontStyle.bold = false;
              
                        
                       
               
                        if (styleType == "Italic")
                            if (oButton)
                                currCell.Element.style.fontStyle="italic";
                            else 
                                currCell.Element.style.fontStyle="normal";
                 
                        if (styleType == "Underline") 
                            if (oButton)   
                                currCell.Element.style.textDecorationUnderline = true;
                            else
                                currCell.Element.style.textDecorationUnderline = false;
                    }
                    }//End For of Cells
        
                }//End For of Rows
            }
        }
        else if(tlbType == "F")
        {
            if(selectedColumnIndex != -1)
            {
            if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                if (styleType == "Bold")
                {
                    if (oButton)
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontStyle.bold = true;
                    else
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontStyle.bold = false;
                }
                else if (styleType == "Italic")
                {
                   if (oButton)
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontStyle="italic";
                   else
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontStyle="normal"; 
                }
                else if (styleType == "Underline")
                {
                    if (oButton)    
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.textDecorationUnderline = true;
                    else
                        grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.textDecorationUnderline = false;
                }
                }

            }
            else
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                
                if (grid.Bands[0].Columns[i]._getFootTags() == null || grid.Bands[0].Columns[i]._getFootTags() == undefined)
                {
                continue ;
                }
                    if (styleType == "Bold")
                    {
                        if (oButton)
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.fontStyle.bold = true;
                        else
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.fontStyle.bold = false;
                    }
                    else if (styleType == "Italic")
                    {
                        if (oButton)
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.fontStyle="italic";
                        else
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.fontStyle="normal"; 
                    }
                    else if (styleType == "Underline")
                    {
                        if (oButton)    
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.textDecorationUnderline = true;
                        else
                            grid.Bands[0].Columns[i]._getFootTags()[0].style.textDecorationUnderline = false;
                    }
                }
            }
        }

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function uwtColumnToolbar_Click(oToolbar, oButton, oEvent)
{
	Toolbar_Click(oToolbar, oButton, oEvent,currToolBar);
}
//---------------------------------------------------------------------
function uwtRowToolbar_Click(oToolbar, oButton, oEvent)
{
	Toolbar_Click(oToolbar, oButton, oEvent,"R");
}
//---------------------------------------------------------------------
function uwtFooterToolbar_Click(oToolbar, oButton, oEvent)
{
	Toolbar_Click(oToolbar, oButton, oEvent,"F");
}
//---------------------------------------------------------------------
function txtHeaderChange()
{
      var grid           = igtbl_getGridById(gridName);
      var txt            = window.document.getElementById("txtHeader");
      var val            = txt.value;
      if (selectedColumnIndex!=null && selectedColumnIndex != -1 && val != "")
      {
          if (currToolBar == "H")
            grid.Bands[0].Columns[selectedColumn.Index].setHeaderText(val);
          else if (currToolBar == "F")
            grid.Bands[0].Columns[selectedColumn.Index].setFooterText(val);
      }
      ApplyGridChanges()
}
//---------------------------------------------------------------------
function txtHeaderKeyDown()
{
    var e= window.event;
    if (e.keyCode == 13)
    {
        txtHeaderChange();
    }
}
//---------------------------------------------------------------------
function HeightChange(tlbType)
{
    var grid = igtbl_getGridById(gridName);
    var val = igedit_getById("txtHeight"+"H" ).getValue();
    tlbType = currToolBar;
    if (val != null && val!="")
    if (tlbType == "H")
    {
        
        if(selectedColumnIndex != -1)
        {
            if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
               grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.height = val;
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
              if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.height = val;
            } 
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                       continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                if(rowIndex != -1)
                {
                    currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                    currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.height = val;}
                
        
            }//End For of Rows
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                       continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.height = val;}
                }//End For of Cells
        
            }//End For of Rows
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.height = val; }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                grid.Bands[0].Columns[i]._getFootTags()[0].style.height = val; }
            }   
        }
    }

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function WidthChanged(tlbType)
{
    var grid = igtbl_getGridById(gridName);
    var val  = igedit_getById("txtWidth"+"H").getValue();
    tlbType = currToolBar;
    
    
    if(selectedColumnIndex != -1)
    {
          grid.Bands[0].Columns[selectedColumn.Index].setWidth(val);
    }
    else
    {
          for (i=0;i<grid.Bands[0].Columns.length ;i++)
          {
               grid.Bands[0].Columns[i].setWidth(val);
          }
     }
    
    /*
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
             grid.Bands[0].Columns[selectedColumnIndex].setWidth(val);
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
                 grid.Bands[0].Columns[i].setWidth(val);
            }
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
        }
        else
        {
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
             grid.Bands[0].Columns[selectedColumnIndex].setWidth(val);
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
                 grid.Bands[0].Columns[i].setWidth(val);
            }
        }
    }
    */
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function ChangeBorderStyle(tlbType,changeWhat)
{
    var tlbName;
    var grid = igtbl_getGridById(gridName);
    if (tlbType == "H")
       tlbName          = headerBarName;
    else if (tlbType == "R")
       tlbName           = rowBarName;
    else if (tlbType == "F")
       tlbName           = footerBarName;
       
    
    
    
    var borderButtons   = igtbar_getItemById(tlbName+"_Item_"+iborderTopI);
    var val
    var copyVal;  
    
    if (changeWhat =="Width")  
        val = igedit_getById("txtBorderWidth"+"H").getValue();
    else if (changeWhat == "Style")
    {
        val = window.document.getElementById("wcbLineStyle"+"H").value;
        try
        {
            copyVal = window.document.getElementById("wcbLineStyle"+"H").options[window.document.getElementById("wcbLineStyle"+"H").selectedIndex].innerText;
        }
        catch(e)
        {
            
        }
        if (val == "notset")
            val = "none"
    }
    else if (changeWhat == "Color")
    {
        val = window.document.getElementById("ddlBorderColor"+"H").value;
    }
    tlbType = currToolBar;
    var currBorder;
    if (borderButtons.SelectedButton != null)
    {
        if( borderButtons.SelectedButton.Index == 1)
        {
            currBorder = "Top"
            if (changeWhat == "Style")
                ccborderTopStyle = copyVal
        }
        else if( borderButtons.SelectedButton.Index == 2)
        {
            currBorder = "Left"
            if (changeWhat == "Style")
                ccborderLeftStyle = copyVal
        }
        else if( borderButtons.SelectedButton.Index == 3)
        {
            currBorder = "Right"
            if (changeWhat == "Style")
                ccborderRightStyle = copyVal
        }
        else if( borderButtons.SelectedButton.Index == 4)
        {
           currBorder = "Bottom" 
           if (changeWhat == "Style")
                ccborderBottomStyle = copyVal
        }
        
       
    }
    else
    {
        currBorder = "All"
        if (changeWhat == "Style")
        {
            ccborderStyle = copyVal
            ccborderTopStyle = copyVal
            ccborderLeftStyle = copyVal
            ccborderRightStyle = copyVal
            ccborderBottomStyle = copyVal
         }
    }
    if (val!=null && val!="")
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
            
            if (currBorder == "Top")
           if (currBorder == "Top")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopColor = val;
                    }                        
                }
            }
            else if (currBorder == "Bottom")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomColor = val;
                  }
                }
            }
            
            else if (currBorder == "Left")
            {
            if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
                if (changeWhat =="Width")
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftWidth = val;
                else if (changeWhat =="Style")
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftStyle = val;
                else if (changeWhat == "Color")
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftColor = val;
                 }
            }
            else if (currBorder == "Right")
            {
            if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
                if (changeWhat =="Width")
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightWidth = val;
                else if (changeWhat =="Style")
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightStyle = val;
                else if (changeWhat == "Color")
                     grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightColor = val;
                }
            }
            else
            {
                if (changeWhat =="Width")
                {
                 if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderTopWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderBottomWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightWidth = val;
                    }
                }
                else if (changeWhat =="Style")
                {
                if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderTopStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderBottomStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftStyle = val;
                    }
                }
                else if (changeWhat == "Color")
                {
                if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderTopColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderBottomColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderLeftColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.borderRightColor = val;
                    }
                    
                }
            }
            GetColumnSettings()
        }
        else
        {
            if (currBorder == "Top")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopColor = val;
                   }
                }
            }
            else if (currBorder == "Bottom")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomColor = val;
                 }
                }
            }
            else if (currBorder == "Left")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftColor = val;
                   }
                }
            }
            else if (currBorder == "Right")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                    if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightWidth = val;
                    else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightStyle = val;
                    else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightColor = val;
                        }
                }
            }
            else
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                    if (changeWhat =="Width")   
                    {
                    if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderWidth = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopWidth = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomWidth = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftWidth = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightWidth = val; }
                    }
                    else if (changeWhat =="Style")
                    {
                    if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderStyle = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomStyle = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopStyle = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftStyle = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightStyle = val;}
                    }
                    else if (changeWhat == "Color")
                    {
                    if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined){
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderColor = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderBottomColor = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderTopColor = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderLeftColor = val;
                        grid.Bands[0].Columns[i]._getHeadTags()[0].style.borderRightColor = val;}
                    }
                }  
            }
            GetGridSettings();
        }
       
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            if (currBorder == "Top")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderTopWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderTopStyle = val;
                        else if (changeWhat == "Color")
                            currCell.Element.style.borderTopColor = val;}
                    }//End For of Cells
               }//End For of Rows 
            
            
            }
            else if (currBorder == "Bottom")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderBottomWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderBottomStyle = val;
                        else if (changeWhat == "Color")
                            currCell.Element.style.borderBottomColor = val;}
                    }//End For of Cells
               }//End For of Rows  
            }
            else if (currBorder == "Left")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    if(rowIndex != -1)
                    {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                    }
                    else
                    {
                            currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    if (changeWhat =="Width")
                        currCell.Element.style.borderLeftWidth = val;
                    else if (changeWhat =="Style")
                        currCell.Element.style.borderLeftStyle = val;
                    else if (changeWhat == "Color")
                        currCell.Element.style.borderLeftColor = val;}
                    
               }//End For of Rows 
            }
            else if (currBorder == "Right")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    if(rowIndex != -1)
                    {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                    }
                    else
                    {
                            currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    if (changeWhat =="Width")
                        currCell.Element.style.borderRightWidth = val;
                    else if (changeWhat =="Style")
                        currCell.Element.style.borderRightStyle = val;
                    else if (changeWhat == "Color")
                        currCell.Element.style.borderRightColor = val;}
                    
               }//End For of Rows 
            }
            else
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    if(rowIndex != -1)
                    {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                    }
                    else
                    {
                            currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    if (changeWhat =="Width")
                    {
                        currCell.Element.style.borderWidth = val;
                        currCell.Element.style.borderBottomWidth = val;
                        currCell.Element.style.borderTopWidth = val;
                        currCell.Element.style.borderLeftWidth = val;
                        currCell.Element.style.borderRightWidth = val;
                    }
                    else if (changeWhat =="Style")
                    {    
                        currCell.Element.style.borderStyle = val;
                        currCell.Element.style.borderBottomStyle = val;
                        currCell.Element.style.borderTopStyle = val;
                        currCell.Element.style.borderLeftStyle = val;
                        currCell.Element.style.borderRightStyle = val;
                    }
                    else if (changeWhat == "Color")
                    {
                        currCell.Element.style.borderColor = val;
                        currCell.Element.style.borderBottomColor = val;
                        currCell.Element.style.borderTopColor = val;
                        currCell.Element.style.borderLeftColor = val;
                        currCell.Element.style.borderRightColor = val;
                    }}
                    
               }//End For of Rows 
            }
            GetColumnSettings();
        }
        else
        {
            if (currBorder == "Top")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderTopWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderTopStyle = val;
                        else if (changeWhat == "Color")
                            currCell.Element.style.borderTopColor = val;}
                    }//End For of Cells
               }//End For of Rows 
            
            
            }
            else if (currBorder == "Bottom")
            {
               for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderBottomWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderBottomStyle = val;
                        else if (changeWhat == "Color")
                            currCell.Element.style.borderBottomColor = val;}
                    }//End For of Cells
               }//End For of Rows  
            }
            else if (currBorder == "Left")
            {
                for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderLeftWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderLeftStyle = val;
                        else if (changeWhat == "Color")
                            currCell.Element.style.borderLeftColor = val;}
                    }//End For of Cells
               }//End For of Rows 
                
            }
            else if (currBorder == "Right")
            {
                for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (currCell.Element != null && currCell.Element != undefined){
                        if (changeWhat =="Width")
                            currCell.Element.style.borderRightWidth = val;
                        else if (changeWhat =="Style")
                            currCell.Element.style.borderRightStyle = val;
                        else if (changeWhat == "Color")
                             currCell.Element.style.borderRightColor = val;}
                    }//End For of Cells
               }//End For of Rows 
               
            }
            else
            {
                for (i=0;i<grid.Rows.length ;i++)
               {
                    var currRow    
                    try
                    { 
                        currRow = igtbl_getRowById(gridName+"_r_"+i);
                    }
                    catch(e)
                    {
                        currRow = igtbl_getRowById(gridName+"_anr");
                    }
        
                    if (currRow == null)
                    continue;
         
                    var cellsLength = currRow.cells.length;
                    var rowIndex    = currRow.getIndex();
        
                    var currCell ; 
                    for (j=0;j<cellsLength;j++)
                    {
                        if(rowIndex != -1)
                        {
                            currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                        }
                        else
                        {
                            currCell = igtbl_getCellById(gridName+"_anc_"+j);
                        }
                        if (changeWhat =="Width")
                        {
                        if (currCell.Element != null && currCell.Element != undefined){
                            currCell.Element.style.borderWidth = val;
                            currCell.Element.style.borderBottomWidth = val;
                            currCell.Element.style.borderTopWidth = val;
                            currCell.Element.style.borderRightWidth = val;
                            currCell.Element.style.borderLeftWidth = val;}
                        }
                        else if (changeWhat =="Style")
                        {
                        if (currCell.Element != null && currCell.Element != undefined){
                            currCell.Element.style.borderStyle = val;
                            currCell.Element.style.borderBottomStyle = val;
                            currCell.Element.style.borderTopStyle = val;
                            currCell.Element.style.borderRightStyle = val;
                            currCell.Element.style.borderLeftStyle = val;}
                        }
                        else if (changeWhat == "Color")
                        {
                        if (currCell.Element != null && currCell.Element != undefined){
                            currCell.Element.style.borderColor = val;
                            currCell.Element.style.borderTopColor = val;
                            currCell.Element.style.borderBottomColor = val;
                            currCell.Element.style.borderLeftColor = val;
                            currCell.Element.style.borderRightColor = val;}
                        }
                    }//End For of Cells
               }//End For of Rows 
                
            }
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
            if (currBorder == "Top")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                
                if (grid.Bands[0].Columns[i]._getFootTags() == null || grid.Bands[0].Columns[i]._getFootTags() == undefined)
                {
                continue ;
                }
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopColor = val;
                }   
            }
            else if (currBorder == "Bottom")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                
                    if (grid.Bands[0].Columns[i]._getFootTags() == null || grid.Bands[0].Columns[i]._getFootTags() == undefined)
                    {
                    continue ;
                    }
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomColor = val;
                }
            }
            else if (currBorder == "Left")
            {
            if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                if (changeWhat =="Width")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftWidth = val;
                else if (changeWhat =="Style")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftStyle = val;
                else if (changeWhat == "Color")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftColor = val;
                    }
            }
            else if (currBorder == "Right")
            {
            if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                if (changeWhat =="Width")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightWidth = val;
                else if (changeWhat =="Style")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightStyle = val;
                else if (changeWhat == "Color")
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightColor = val;
                    }
            }
            else
            {
                if (changeWhat =="Width")
                {
                if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderTopWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderBottomWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftWidth = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightWidth = val;
                    }
                }
                else if (changeWhat =="Style")
                {
                if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderBottomStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderTopStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftStyle = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightStyle = val;
                    }
                }
                else if (changeWhat == "Color")
                {
                if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderBottomColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderTopColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderRightColor = val;
                    grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.borderLeftColor = val;
                    }
                }
            }
            GetColumnSettings();
        }
        else
        {
            if (currBorder == "Top")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined)
                {
                    continue ;
                }
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopColor = val;
                }   
            }
            else if (currBorder == "Bottom")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined)
                {
                    continue ;
                }
                
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomColor = val;
                }
            }
            else if (currBorder == "Left")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined)
                {
                    continue ;
                }
                
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftColor = val;
                }
            }
            else if (currBorder == "Right")
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined)
                {
                    continue ;
                }
                
                     if (changeWhat =="Width")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightWidth = val;
                     else if (changeWhat =="Style")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightStyle = val;
                     else if (changeWhat == "Color")
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightColor = val;
                } 
            }
            else
            {
                for (i=0;i<grid.Bands[0].Columns.length ;i++)
                {
                     if (changeWhat =="Width")
                     {
                    if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){ 
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderWidth = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomWidth = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopWidth = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftWidth = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightWidth = val;
                        }
                     }
                     else if (changeWhat =="Style")
                     {
                     if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){ 
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderStyle = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomStyle = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopStyle = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftStyle = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightStyle = val;
                        }
                     }
                     else if (changeWhat == "Color")
                     {
                     if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){ 
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderColor = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderBottomColor = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderTopColor = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderLeftColor = val;
                        grid.Bands[0].Columns[i]._getFootTags()[0].style.borderRightColor = val;
                        }
                     }
                } 
            }
            GetGridSettings();
        }
    }

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function BorderWidthChanged(tlbType)
{
    ChangeBorderStyle(currToolBar,"Width")
}
//---------------------------------------------------------------------
function BorderStyleChange(tlbType)
{
    ChangeBorderStyle(currToolBar,"Style")
}
//---------------------------------------------------------------------
function BorderColorChange(tlbType)
{
   ChangeBorderStyle(currToolBar,"Color") 
}
//---------------------------------------------------------------------
function MarginsChanged(tlbType)
{
    var grid = igtbl_getGridById(gridName);
    var val  = igedit_getById("txtMargins"+"H").getValue();
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined)
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.margin = val; 
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.margin = val;
            }
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.margin = val; } //marginLeft 
               
        
            }//End For of Rows
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.margin = val;} //marginLeft
                }//End For of Cells
        
            }//End For of Rows
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.margin = val; }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.margin = val;}
            }
        }
    }

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function PaddingChanged(tlbType)
{
    var grid = igtbl_getGridById(gridName);
    var val  = igedit_getById("txtPadding"+"H").getValue();
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.padding = val;
            GetColumnSettings() 
            }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.padding = val;
            }
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.padding = val; } //marginLeft
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.padding = val; } //marginLeft
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.padding = val;
             GetColumnSettings(); }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.padding = val;
                }
            }
            GetGridSettings();
        }
    }

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function VerticalAlignChange(tlbType)
{  
    var grid = igtbl_getGridById(gridName);
    var val  = window.document.getElementById("DropDownList"+"1").value;
    
    if (val == 1)
    {
        val = "top"
    }
    else if(val == 2)
    {
        val = "middle"
    }
    else if (val == 3)
    {
        val = "bottom"
    }
    else
    {
        val = "auto"
    }
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.verticalAlign = val;
            GetColumnSettings() }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.verticalAlign = val;
            }
            
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.verticalAlign = val; }
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.verticalAlign = val; }
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.verticalAlign = val;
             GetColumnSettings();}
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.verticalAlign = val;
                 }
            }
            GetGridSettings();
        }
    } 
    
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function FontSizeChange(tlbType,Id)
{
    var grid = igtbl_getGridById(gridName);
    var val = window.document.getElementById(Id).value
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontSize = val;
            GetColumnSettings() 
            }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontSize = val;
            }
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.fontSize = val; }
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.fontSize = val; }
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontSize = val;
             GetColumnSettings();
             }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.fontSize = val;
                 }
            }
            GetGridSettings();
        }
    } 

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function FontNameChange(tlbType,Id)
{
    var grid = igtbl_getGridById(gridName);
    var val = window.document.getElementById(Id).value
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.fontFamily = val;
            GetColumnSettings() 
            }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
                if (grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                    grid.Bands[0].Columns[i]._getHeadTags()[0].style.fontFamily = val;
            }
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.fontFamily = val;}
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.fontFamily = val;}
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.fontFamily = val;
             GetColumnSettings();}
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.fontFamily = val;}
            }
            GetGridSettings();
        }
    } 

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function BackColorChange(tlbType,Id)
{
    var grid = igtbl_getGridById(gridName);
    var val = window.document.getElementById(Id).value
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.backgroundColor = val;
            GetColumnSettings() }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.backgroundColor = val;
            }
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.backgroundColor = val;}
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        if (currCell != null && currCell != undefined){
                currRow.Element.style.background = val ;}
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.backgroundColor = val;}
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.backgroundColor = val;
             GetColumnSettings();}
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.backgroundColor = val;}
            }
            GetGridSettings();
        }
    } 

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function ForeColorChange(tlbType,Id)
{
    var grid = igtbl_getGridById(gridName);
    var val = window.document.getElementById(Id).value
    tlbType = currToolBar;
    if (tlbType == "H")
    {
        if(selectedColumnIndex != -1)
        {
        if(grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= null && grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()!= undefined){
            grid.Bands[0].Columns[selectedColumn.Index]._getHeadTags()[0].style.color = val;
            GetColumnSettings() 
            }
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
                if(grid.Bands[0].Columns[i]._getHeadTags()!= null && grid.Bands[0].Columns[i]._getHeadTags()!= undefined)
                 grid.Bands[0].Columns[i]._getHeadTags()[0].style.color = val;
            }
            GetGridSettings()
        }
    }
    else if (tlbType == "R")
    {
        if(selectedColumnIndex != -1)
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
               
                if(rowIndex != -1)
                {
                     currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+selectedColumn.Index); 
                }
                else
                {
                     currCell = igtbl_getCellById(gridName+"_anc_"+selectedColumn.Index);
                }
                if (currCell.Element != null && currCell.Element != undefined){
                currCell.Element.style.color = val; }
               
        
            }//End For of Rows
            GetColumnSettings()
        }
        else
        {
            for (i=0;i<grid.Rows.length ;i++)
            {
                var currRow    
                try
                { 
                    currRow = igtbl_getRowById(gridName+"_r_"+i);
                }
                catch(e)
                {
                    currRow = igtbl_getRowById(gridName+"_anr");
                }
        
                if (currRow == null)
                    continue;
         
                var cellsLength = currRow.cells.length;
                var rowIndex    = currRow.getIndex();
        
                var currCell ; 
                for (j=0;j<cellsLength;j++)
                {
                    if(rowIndex != -1)
                    {
                        currCell = igtbl_getCellById(gridName+"_rc_"+i+"_"+j); 
                    }
                    else
                    {
                        currCell = igtbl_getCellById(gridName+"_anc_"+j);
                    }
                    if (currCell.Element != null && currCell.Element != undefined){
                    currCell.Element.style.color = val; }
                }//End For of Cells
        
            }//End For of Rows
            GetGridSettings();
        }
    }
    else if(tlbType == "F")
    {
        if(selectedColumnIndex != -1)
        {
        if (grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != null && grid.Bands[0].Columns[selectedColumn.Index]._getFootTags() != undefined){
             grid.Bands[0].Columns[selectedColumn.Index]._getFootTags()[0].style.color = val;
             GetColumnSettings();}
        }
        else
        {
            for (i=0;i<grid.Bands[0].Columns.length ;i++)
            {
            if (grid.Bands[0].Columns[i]._getFootTags() != null && grid.Bands[0].Columns[i]._getFootTags() != undefined){
                 grid.Bands[0].Columns[i]._getFootTags()[0].style.color = val;}
            }
            GetGridSettings();
        }
    } 

    ApplyGridChanges()
}
//---------------------------------------------------------------------
function uwtColumnToolbar_MouseOut(oToolbar, oButton, oEvent)
{
	var item = 5
}
//---------------------------------------------------------------------
function SortChange(Id)
{
    var ddlSort                 = window.document.getElementById(Id)
    var grid                    = igtbl_getGridById(gridName)
    var val                     = ddlSort.value
    if(selectedColumnIndex      != -1)
    {
      var col = grid.Bands[0].Columns[selectedColumn.Index]
      //-----------------------------------------------------
      if (val               > "0")
      {
      SetGridSortIndecator(selectedColumn.Index,gridName)
         if (val               == "2")
          {
            col.SortIndicator   = 1
            grid.AllowSort =1
            grid.addSortColumn(col.Id,true)            
            grid.sort();
            col.SortIndicator   = 1
           grid.AllowSort = 0
          }
          else if (val          == "1")
          {
            col.SortIndicator   = 2
            grid.AllowSort = 1
            grid.addSortColumn(col.Id,true)
            grid.sort();
            col.SortIndicator   = 2
            grid.AllowSort = 0
          }
      }   
      //-----------------------------------------------------    
             
    }
    ApplyGridChanges()
}
//---------------------------------------------------------------------
function B1()
{
    var b1 = window.document.getElementById("Button1")
    b1.value = " Ahm "
    
    var grid = igtbl_getGridById(gridName)
    var col = grid.Bands[0].Columns[0];
    

}
//---------------------------------------------------------------------
// cRITERIA sCREEN 
function  CheckDateEntryValue(DateValue,MinValue,MaxValue)
{

var ultraTab          = igtab_getTabById("UltraWebTab1");
var oCombo    = igtab_getElementById(DateValue ,ultraTab.element);

 if(oCombo.getValue != "" ||oCombo.getValue !=undefined)
   {
    if(oCombo.getValue < MaxValue)
      {
      if(oCombo.getValue > MinValue)
        {
        }
        else 
            {
            oCombo.setValue="";
            }
       }
        else
            {
             oCombo.setValue="";
            }
    }
}
//---------------------------------------------------------------------
function  CheckTxtEntryValue(TxtBox,MinValue,MaxValue)
{
var ultraTab          = igtab_getTabById("UltraWebTab1");
var oCombo    = igtab_getElementById(TxtBox ,ultraTab.element);

 if(oCombo.value != "" ||oCombo.value !=undefined)
   {
   var max      = ConvertToNumber(MaxValue)
   var min      = ConvertToNumber(MinValue)
   var ddlmax   = ConvertToNumber(oCombo.value)
   var ddlmin   = ConvertToNumber(oCombo.value)
   
    if(ddlmax <= max)
      {
      if(ddlmin >= min)
        {
            return true;
        }
        else
            {
            oCombo.value="";
            }
        }
        else 
           {
            oCombo.value="";
           }
    }
}	
//---------------------------------------------------------------------
function CloseTheWindow()
{
    window.close()
}
//---------------------------------------------------------------------
function Button1_onclick() 
{
B1()
}
//---------------------------------------------------------------------
///////////// Other Fields Module Functions 
//---------------------------------------------------------------------
function OpenOtherFieldsForm(ObjectID , TableName)
{
				    var winopen = window.open("frmOtherFields.aspx?ObjectID= "+ObjectID+"&TableName="+TableName,"_Parent","height=376,width=467,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,titlebar=0,dependent=No");
				    winopen.focus();
}
//---------------------------------------------------------------------
function DisplayOtherFields(TableName,ObjectID,RecordID)
{
                var winopen = window.open("frmOtherFieldsDynamic.aspx?ObjectID="+ObjectID+"&TableName="+TableName+"&RecordID="+RecordID , "_WindowName","height=450,width=500,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
		        winopen.focus();
  	        
}
//---------------------------------------------------------------------
function TlbMainToolbar_Click(oToolbar, oButton, oEvent)
{
	//Add code to handle your event here.
	if (oButton.Key  = "Save")
	SaveOtherFieldsData();
}
//---------------------------------------------------------------------
 function SetGridSortIndecator(SortColIndex,gridName)
 {
    var grid                    = igtbl_getGridById(gridName)
    var strTXTColumnsStyles = window.document.getElementById("txtColumnsStyles")
    var strCurrtxtStyle = txtColumnsStyles.value;
    for(i=0;i< grid.Bands[0].Columns.length ;i++)
     {
        if(i!=SortColIndex)
        {
            var col = grid.Bands[0].Columns[i];
            col.SortIndicator   = 0                
            strCurrtxtStyle  = SetValueFromString(strCurrtxtStyle ,col.Key+"_CSORT",'0')       
        }
     
     }
     strTXTColumnsStyles.value = strCurrtxtStyle ;
}

function ConvertToNumber(objValue)
{
    var tmp= objValue-1;
    tmp = tmp+1;
    return tmp;
}