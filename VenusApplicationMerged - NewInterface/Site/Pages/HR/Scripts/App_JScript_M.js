// JScript File

//============================ Set Timer [START]

var STimeout;
var bCofirm        = false;
var bCancelWait    = false;
var currDlg;
var bLoginPage     = false;

function setSessionTimeout(val)
{
    STimeout = val-60000;
    if (STimeout <= 0)
    {
        STimeout = 60000;
    }
}

function InitializeTimer()
{
    window.setTimeout("TimeEnd()",STimeout);
}

function TimeEnd()
{
    
    var lang                = new String(); 
    var msg;
    lang                    = GetCookie("Lang");
   
    if (lang.indexOf("ar")>-1)
        msg = "لقد إنتهت ال Session هل تود المتابعة؟";
    else
        msg =" Sesssion Ended , Do you want to continue?"; 
   
   
    var currDlg             = openModelDialoge("frmConfirm.aspx?Msg="+msg,731,371);
    
    if (currDlg == true)
    {
        
        window.focus();
        
    }
    else
    {
        var openerWindow = window.opener;
        openerWindow.ShowLoginPage(window);
        
        //newLoginPage = true;
        //window.setTimeout("OpenLoginPageFromParent()",2000)
    }
    
}

/*
var newLoginPage = false;
function setNewLogin()
{   
   newLoginPage = false; 
}
function MainPageLoad()
{
    window.setTimeout("CheckForNewLogin()",500);
    
}
function CheckForNewLogin()
{
    if( newLoginPage)
    {
        openModelDialoge("frmLoginOnly.aspx",531,271)
    }
    else
    {
        MainPageLoad();
    }
}
*/
function OpenLoginPageFromParent()
{
    openModelDialoge("frmLoginOnly.aspx",531,271);
}

function IntializeExitTimerOfDlg()
{
    window.setTimeout("waitUntilTime()",30000); 
}
function waitUntilTime()
{ 
    frmConfirmbtnExit_onclick()
}
function frmConfirmbtnExit_onclick(){
  
   window.returnValue = false;
   window.close();
}

function openModelDialoge(url,width,height)
{
    var strFeatures = "resizable=no;dialogWidth:"+width+"px;dialogHeight:"+height+"px;help:no;maximize:yes;minimize:yes;scrollbars:no";
    return window.showModalDialog(url,'',strFeatures)
}

function openModelDialoge2(url,width,height,win)
{
    var strFeatures = "resizable=no;dialogWidth:"+width+"px;dialogHeight:"+height+"px;help:no;maximize:yes;minimize:yes;scrollbars:no";
    return win.showModalDialog(url,'',strFeatures)
}

function openLoginPage()
{
    window.open("frmLoginOnly.aspx","_Parent","height=271px,width=531px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
}

function frmConfirmbtnOK_onclick() {
     window.returnValue = true;
     window.close();
}

function frmConfirmbtnCancel_onclick() {
     window.returnValue = false;
     window.close();
}

//============================ Set Timer [ END ]

function RefreshBack()
{
if (window.document.readyState=="complete")
  {
    window.opener.document.forms[0].submit();
  }
}

function LoadDataUpdateSchedules(e,formName,controlName,recordID)
{
    e=window.event;
    var code =e.keyCode;
    
    if( e.ctrlKey && code==21 )
    {
        var queryString="?FormName="+formName+"&ControlName="+controlName+"&RecordID="+recordID;
        window.open("frmDataUpdateSchedule.aspx"+queryString,"_Parent","height=368px,width=550px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
        //window.open("frmDataUpdateSchedule.aspx"+queryString)
    } 
    else
    {
        //IsDataChanged = true;
        if (code != 13)
            IsDataChanged = "T";
    }
   
}

function IntializeDataChanged()
{
    IsDataChanged = "F";
}

/*function DataChanged(val)
{
    IsDataChanged = val;
}
*/

function formSubmit(e)
{

    //var x 
    //x=window.event;
    if (IsDataChanged == "T")
    {
        var msg = returnDiscardMsg();
        if (window.confirm(msg))
        {
                
        }
        else
        {
            //window.onbeforeunload = false;
            e.returnValue = false;
            window.focus();
        }
    }
 
}




var isFormSubmit = false;
function confirmSubmit()
{
    isFormSubmit = true;
}

function returnSubmit()
{
    return isFormSubmit;
}

//========================= Do Exit & Submit [Start]
var IsDataChanged = "F";
//window.document.forms[0].tagUrn ="";
var IsNavigation = true;
function TlbMainToolbarNotNavigation_Click(oToolbar, oButton, oEvent){
	IsNavigation = false
}
function DataChanged()
{
    IsDataChanged = "T";
    //alert("Change");
    //window.document.forms[0].tagUrn ="T";
    //window.event.returnValue = "T"
}
function confirmChange()
{
     if (IsDataChanged == "T")
     {
        IsDataChanged = "T";
     }
}
function isFormChanged()
{
    if (IsDataChanged == "T" && IsNavigation)
    //if (window.document.forms[0].tagUrn == "T" && IsNavigation)
    {
        return true;
    } 
    else
    {
        //window.close();
        //IsNavigation = true; 
        return false;
    }
}

function returnDiscardMsg()
{
    var str = new String();
    str  =GetCookie("Lang"); //window.clientInformation.userLanguage;
    if( str.indexOf("ar")>-1)
    {return "يوجد تعديلات على البيانات ,هل تريد التجاهل؟";}
    else
    { return "The data has been changed ,Are you sure you want to discard changes?";}
}
function GetCookie (name) {
    var arg = name + "=";
  
    var arr = document.cookie.split(';');
    var retStr = "";
    for(i=0;i<arr.length;i++)
    {
        if (arr[i].indexOf(arg) > -1)
        {
            retStr = arr[i].split('=')[1];
            return retStr;
        }
    }
  
  }

//========================= Do Exit & Submit [ End ]

function formExit()
{
/*
    if (window.document.readyState=="complete")
    {
        var e = window.event;
        //e.returnValue = 'Are you sure you want to leave the page?';
        
        if (IsDataChanged == "T")
        {
           if (window.confirm("The data have been changed ,Are you sure you want to discard changes?"))
           {
              window.close(); 
              
           }
           else
           {
            
           //============= New Idea [Start]
           //window.close(); 
           //reLoadPage(window);
           //============= New Idea [ End ]
           
           
            //window.document.focus();
            //e.returnValue = false;
            //return false;
            
            
            //window.document.readyState = "loading";
            //window.closed= false;
            
            //X.returnValue = false;
            //X.cancelEvent = true;
            
            //X.cancelBubble = true;
            
            //cancelEvent(e);
            //stopEvent(e);
            
            //window.event.beforeUnload = null;
            //return false;
            
            
            
            //===============Cancel Event [Start]
            if (!e) e = window.event;
            if (e.preventDefault) {
                e.preventDefault();
            } else {
            e.returnValue = false;
            //event.returnValue = 'Are you sure you want to leave the page?';

            }

            
            //===============Cancel Event [ End ]
            
            //===============Stop Event [Start]
            if (!e) e = window.event;
            if (e.stopPropagation) {
                e.stopPropagation();
            } else {
            e.cancelBubble = true;
            }
            //===============Stop Event [ End ]
            window.document.focus();
          
          
           //window.document.forms[0].submit();
          
          
          }
          
          
          
        
        }
        else
        {
            window.close();
        }
        
    } 
    */  
}

function reLoadPage(win)
{
/*
    var width = win.screen.availWidth-10;
    var height = win.screen.availHeight-35;
    var url = win.document.forms[0].action;
    
    var nW=window.open(url,"_Parent","height="+height+"px,width="+width+"px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
    window.document.forms[0].innerHtml =win.document.forms[0].innerHtml; 
  */      
    
}

function stopEvent(e) {
    if (!e) e = window.event;
    if (e.stopPropagation) {
        e.stopPropagation();
    } else {
        e.cancelBubble = true;
    }
}
function cancelEvent(e) {
    if (!e) e = window.event;
    if (e.preventDefault) {
        e.preventDefault();
    } else {
        e.returnValue = false;
    }
}

//================================== [Set Arabic ][Start]
//============================= Integrate Data Update Schedule with Arabic Events [Start]
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
    else
    {
        //IsDataChanged = true;
        if (code != 13)
            IsDataChanged = "T";
    } 
    
   ArabicKeyPress(e);
}
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
//================================== [Set Arabic ][ End ]



//frmItems.aspx
function WebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent){
	
	        var oComboFrom = igdrp_getComboById('WebDateChooser1');
	        var oComboTo = igdrp_getComboById('WebDateChooser2');
        	
        	var valueFrom  = oComboFrom.getValue()
        	var nD = new Date

            nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate())
        	if (newValue < valueFrom)
        	{
        	    oComboTo.setValue(nD)
        	}
}

//frmItems.aspx
function WebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent){
	     var oComboTo = igdrp_getComboById('WebDateChooser2');
	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
        	
        	var valueTo  = oComboTo.getValue()
        	var nD = new Date

            nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate())
       	    
       	    if (newValue > valueTo)
        	{
        	    oComboFrom.setValue(nD)
        	}

}

function IsDatesEqual(date1,date2)
{
    var d1 =new Date;
    var d2 = new Date;
    d1 = date1;
    d2 = date2;
    
    if(d1.getDate()== d2.getDate() && d1.getMonth() == d2.getMonth() && d1.getFullYear() == d2.getFullYear()   )
    {
        return true;
        
    }
    else
    {
        return false;
    }
}


//    '========================================================================
//    'ProcedureName  :  CompareTwoTime
//    'Description    :  Compare between two times and return 0 if equal ,1 if first greater
//                       otherwise return -1
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  14-01-2008
//    'Modifacations  : 
//    '========================================================================

function Compare2Time(time1 ,time2)
{
    var t1 = new Date();
    var t2 = new Date();
    
    t1 = time1;
    t2 = time2;
    
    var h1 = t1.getHours();
    var h2 = t2.getHours();
    
    var m1 = t1.getMinutes();
    var m2 = t2.getMinutes();
    
    if(h1>h2)
    {
        return 1;
    }
    else if (h1<h2)
    {
        return -1;
    }
    else
    {
        if (m1>m2)
        {
            return 1;
        }
        else if(m1<m2)
        {
            return -1;
        } 
        else
        {
            return 0;
        }
    }
}


////frmEmployeeVacations.aspx
//function frmVacWebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent){
//	
//	        var oComboFrom = igdrp_getComboById('WebDateChooser1');
//	        var oComboTo = igdrp_getComboById('WebDateChooser2');
//	        var TimeTo = igedit_getById("WebDateTimeEdit2");
//	        var TimeFrom = igedit_getById("WebDateTimeEdit1");
//	        
//	        var fromDate = oComboFrom.getValue();
//	        var toDate =  oComboTo.getValue();
//	        var fromTime = TimeFrom.getValue();
//	        var toTime = TimeTo.getValue();
//	        
//	        if (fromDate > toDate)
//	        {
//	            oComboFrom.setValue(toDate);
//	            if(fromTime > toTime )
//	            {
//	             TimeFrom.setValue(toTime);
//	            }
//	        }
//	        //if(fromTime > toTime && IsDatesEqual (fromDate , toDate) )
//	        if(Compare2Time(fromTime,toTime)==1 && IsDatesEqual (fromDate , toDate) )
//	        {
//	            TimeFrom.setValue(toTime);
//	        }
//        	
//        	/*
//        	var valueFrom  = oComboFrom.getValue();
//        	var nD = new Date

//            //nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate())
//            nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate())
//        	if (newValue < valueFrom && valueFrom != null)
//        	{
//        	    //oComboTo.setValue(nD)
//        	    oComboFrom.setValue();
//        	}
//        	txtTo.setValue(oComboTo.getValue());
//        	*/
//}
//frmEmployeeVacations.aspx


//frmEmployeeVacations.aspx
//function frmVacWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent){
//	    
//	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
//	        var oComboTo = igdrp_getComboById('WebDateChooser2');
//	        var TimeTo = igedit_getById("WebDateTimeEdit2");
//	        var TimeFrom = igedit_getById("WebDateTimeEdit1");
//	        
//	        var fromDate = oComboFrom.getValue();
//	        var toDate =  oComboTo.getValue();
//	        var fromTime = TimeFrom.getValue();
//	        var toTime = TimeTo.getValue();
//	        
//	        if (fromDate > toDate)
//	        {
//	            oComboTo.setValue(fromDate);
//	             if(fromTime > toTime)
//	            {
//	                TimeTo.setValue(fromTime);
//	            }
//	        }
//	        //if(fromTime > toTime && IsDatesEqual( fromDate , toDate) )
//	        if(Compare2Time(fromTime,toTime)==1 && IsDatesEqual (fromDate , toDate) )
//	        {
//	            TimeTo.setValue(fromTime);
//	        }
//	    
//	    
//	     /*var oComboTo = igdrp_getComboById('WebDateChooser2');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
//	      var txtFrom = igedit_getById("WebDateTimeEdit1");
//        	
//        	var valueTo  = oComboTo.getValue()
//        	var nD = new Date

//            nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate())
//       	    
//       	    if (newValue > valueTo &&  valueTo != null)
//        	{
//        	    oComboFrom.setValue(nD)
//        	}
//        	txtFrom.setValue(oComboFrom.getValue());
//*/
//}
////frmEmployeeVacations.aspx
//function frmVacWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent){
//	    
//	    
//	        var webTab = igtab_getTabById("UltraWebTab1");
//            if(webTab == null)
//                return ;
//     
//            var control            = igtab_getElementById("WebDateChooser1", webTab.element) 
//            var oComboFrom = igdrp_getComboById(control.id);
//	        
//	        control                = igtab_getElementById("WebDateChooser2", webTab.element) 
//	        var oComboTo = igdrp_getComboById(control.id);
//	        
//	        control                = webTab.findControl("WebDateTimeEdit2")
//	        var TimeTo = igedit_getById(control.id);
//	        
//	        control                =webTab.findControl("WebDateTimeEdit1")
//	        var TimeFrom = igedit_getById(control.id);
//	        
//	        var fromDate = oComboFrom.getValue();
//	        var toDate =  oComboTo.getValue();
//	        var fromTime = TimeFrom.getValue();
//	        var toTime = TimeTo.getValue();
//	        
//	        if (fromDate > toDate)
//	        {
//	            oComboTo.setValue(fromDate);
//	             if(fromTime > toTime)
//	            {
//	                TimeTo.setValue(fromTime);
//	            }
//	        }
//	        
//	        if(Compare2Time(fromTime,toTime)==1 && IsDatesEqual (fromDate , toDate) )
//	        {
//	            TimeTo.setValue(fromTime);
//	        }
//	   
//	        DdlVacationTypeChange()
//}


//function frmVacWebDateChooser4_ValueChanged(oDateChooser, newValue, oEvent){
//	         var oComboFrom = igdrp_getComboById('WebDateChooser3');
//	        var oComboTo = igdrp_getComboById('WebDateChooser4');
//	        var TimeTo = igedit_getById("WebDateTimeEdit4");
//	        var TimeFrom = igedit_getById("WebDateTimeEdit3");
//	        
//	        var fromDate = oComboFrom.getValue();
//	        var toDate =  oComboTo.getValue();
//	        var fromTime = TimeFrom.getValue();
//	        var toTime = TimeTo.getValue();
//	        
//	        if (fromDate > toDate)
//	        {
//	            oComboFrom.setValue(toDate);
//	            if(fromTime > toTime )
//	            {
//	                TimeFrom.setValue(toTime);
//	            }
//	        }
//	        //if(fromTime > toTime && IsDatesEqual(fromDate , toDate) )
//	        if(Compare2Time(fromTime,toTime)==1 && IsDatesEqual (fromDate , toDate) )
//	        {
//	            TimeFrom.setValue(toTime);
//	        }
//	      
//	       /* var oComboFrom = igdrp_getComboById('WebDateChooser3');
//	        var oComboTo = igdrp_getComboById('WebDateChooser4');
//	        var txtTo = igedit_getById("WebDateTimeEdit4");
//        	
//        	var valueFrom  = oComboFrom.getValue()
//        	var nD = new Date

//            nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate())
//        	if (newValue < valueFrom && valueFrom != null)
//        	{
//        	    oComboTo.setValue(nD)
//        	}
//        	txtTo.setValue(oComboTo.getValue());
//        	*/
//}

////frmEmployeeVacations.aspx
//function frmVacWebDateChooser3_ValueChanged(oDateChooser, newValue, oEvent){
//	     
//	      var oComboFrom = igdrp_getComboById('WebDateChooser3');
//	        var oComboTo = igdrp_getComboById('WebDateChooser4');
//	        var TimeTo = igedit_getById("WebDateTimeEdit4");
//	        var TimeFrom = igedit_getById("WebDateTimeEdit3");
//	        
//	        var fromDate = oComboFrom.getValue();
//	        var toDate =  oComboTo.getValue();
//	        var fromTime = TimeFrom.getValue();
//	        var toTime = TimeTo.getValue();
//	        
//	        if (fromDate > toDate)
//	        {
//	            oComboTo.setValue(fromDate);
//	            if(fromTime > toTime )
//	            {
//	             TimeTo.setValue(fromTime);
//	            }
//	        }
//	        //if(fromTime > toTime && IsDatesEqual(fromDate ,toDate) )
//	        if(Compare2Time(fromTime,toTime)==1 && IsDatesEqual (fromDate , toDate) )
//	        {
//	            TimeTo.setValue(fromTime);
//	        }
//	     
//	     
//	     /*var oComboTo = igdrp_getComboById('WebDateChooser4');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser3');
//	      var txtFrom = igedit_getById("WebDateTimeEdit3");
//        	
//        	var valueTo  = oComboTo.getValue()
//        	var nD = new Date

//            nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate())
//       	    
//       	    if (newValue > valueTo && valueTo != null)
//        	{
//        	    oComboFrom.setValue(nD)
//        	}
//        	txtFrom.setValue(oComboFrom.getValue());
//*/
//}
////frmEmployeeVacations
//function frmVacWebDateTimeEdit2_ValueChange(oEdit, oldValue, oEvent){
//    
//   
//	    var txtFrom = igedit_getById("WebDateTimeEdit1");
//        var txtTo = igedit_getById("WebDateTimeEdit2");
//         var oComboTo = igdrp_getComboById('WebDateChooser2');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
//    
//        if(txtFrom.getValue() != null && txtTo.getValue()!=null && oComboTo.getValue() != null && oComboFrom.getValue()!=null )
//        {
//            //if(txtFrom.getValue() > txtTo.getValue() &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            if( Compare2Time(  txtFrom.getValue() , txtTo.getValue())==1 &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            {
//                //txtTo.setValue(txtFrom.getValue())
//                txtFrom.setValue(txtTo.getValue());
//            }
//        }//End if
//  
//	
//}//End of WebDateTimeEdit2_ValueChange

////frmEmployeeVacations
//function frmVacWebDateTimeEdit1_ValueChange(oEdit, oldValue, oEvent){
//    
//   
//	    var txtFrom = igedit_getById("WebDateTimeEdit1");
//        var txtTo = igedit_getById("WebDateTimeEdit2");
//         var oComboTo = igdrp_getComboById('WebDateChooser2');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
//	     
//	     var fromDateTime = new Date()
//	     fromDateTime = txtFrom.getValue();
//	     
//    
//       
//        if(txtFrom.getValue() != null && txtTo.getValue()!=null && oComboTo.getValue() != null && oComboFrom.getValue()!=null )
//        {
//            //if(txtFrom.getValue() > txtTo.getValue() &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            if( Compare2Time(  txtFrom.getValue() , txtTo.getValue())==1 &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            {
//               //txtFrom.setValue(txtTo.getValue())
//               txtTo.setValue(txtFrom.getValue());
//            }
//        }//End if
//  
//	
//}//End of WebDateTimeEdit2_ValueChange

////frmEmployeeVacations
//function frmVacWebDateTimeEdit4_ValueChange(oEdit, oldValue, oEvent){
//    
//   
//	    var txtFrom = igedit_getById("WebDateTimeEdit3");
//        var txtTo = igedit_getById("WebDateTimeEdit4");
//         var oComboTo = igdrp_getComboById('WebDateChooser4');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser3');
//    
//        if(txtFrom.getValue() != null && txtTo.getValue()!=null && oComboTo.getValue() != null && oComboFrom.getValue()!=null )
//        {
//            //if(txtFrom.getValue() > txtTo.getValue() &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            if( Compare2Time(  txtFrom.getValue() , txtTo.getValue())==1 &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            {
//                //txtTo.setValue(txtFrom.getValue())
//                txtFrom.setValue(txtTo.getValue());
//            }
//        }//End if
//  
//	
//}//End of WebDateTimeEdit2_ValueChange

////frmEmployeeVacations
//function frmVacWebDateTimeEdit3_ValueChange(oEdit, oldValue, oEvent){
//    
//   
//	    var txtFrom = igedit_getById("WebDateTimeEdit3");
//        var txtTo = igedit_getById("WebDateTimeEdit4");
//         var oComboTo = igdrp_getComboById('WebDateChooser4');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser3');
//    
//        if(txtFrom.getValue() != null && txtTo.getValue()!=null && oComboTo.getValue() != null && oComboFrom.getValue()!=null )
//        {
//            //if(txtFrom.getValue() > txtTo.getValue() &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            if( Compare2Time(  txtFrom.getValue() , txtTo.getValue())==1 &&  IsDatesEqual(oComboTo.getValue(),oComboFrom.getValue()) )
//            {
//                //txtFrom.setValue(txtTo.getValue())
//                txtTo.setValue(txtFrom.getValue());
//            }
//        }//End if
//  
//	
//}//End of WebDateTimeEdit2_ValueChange





////PLz delete all function from App_jscript and put this script on App_jscript_M



//=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 14/08/2007
// Description : Validate on Grades Transactions on max value must >= minvalue
//Screen       :frmGrades
//========================================================================          
	function uwgGradesTransactions_AfterCellUpdateHandler(gridName, cellId)
	{
	       var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	       
            if (cell.Column.Index== 2)
             {
                     
                    var nxtCell=igtbl_getCellById (cell.getNextCell().Id);
                    var maxValue=nxtCell.getValue();
                    
                    if(cell.getValue()<0)
                    {
                        cell.setValue(0);
                    }
                    //-------------------------------0257 MODIFIED-----------------------------------------
                    if(cell.getValue()>922337203685477.5807)
                    {
                        cell.setValue(922337203685477.5807);
                    }
                    
                    //-------------------------------=============-----------------------------------------
                    var minValue=cell.Value;
                    
                    if(maxValue < minValue)
                    {
                        nxtCell.setValue(minValue)
                    }
                  
                    
                    
             }
             
              if (cell.Column.Index== 3)
             {
                                     
                   
                    var prvCell=igtbl_getCellById (cell.getPrevCell().Id);
                    var minValue=prvCell.getValue();
                    
                    var maxValue=cell.Value;
                    
                    if(cell.getValue()<0)
                    {
                        cell.setValue(0);
                    }
                    //-------------------------------0257 MODIFIED-----------------------------------------
                    if(cell.getValue()>922337203685477.5807)
                    {
                        cell.setValue(922337203685477.5807);
                    }
                    
                    //-------------------------------=============-----------------------------------------                   
                    
                    if(minValue > maxValue)
                    {
                       prvCell.setValue(maxValue)
                    }
                  
                    
                    
             }
	
    }
    
    //=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 16/08/2007
// Description : Validate on vacations DG
//Screen       :frmGrades
//======================================================================== 
 
 function uwgVacations_AfterCellUpdateHandler(gridName, cellId)
 {
	 
	       var grid = igtbl_getGridById(gridName);
	       var cell = igtbl_getCellById(cellId);
	         
            if (cell.Column.Index== 3)
             {                                     
                                      
                    var degree=cell.Value;
                    if(degree <= 0 )
                    {
                        cell.setValue("1");
                    }
                     //-------------------------------0257 MODIFIED-----------------------------------------
                    if(degree>255)
                    {
                        cell.setValue(255);
                    }
                    
                    //-------------------------------=============-----------------------------------------    
                   
              }
               if (cell.Column.Index== 2)
             { 
                var degree=cell.Value;
                if(degree < 0 )
                    {
                        cell.setValue("0");
                    }
                   //-------------------------------0257 MODIFIED-----------------------------------------
                    if(degree>255)
                    {
                        cell.setValue(255);
                    }
                    
                    //-------------------------------=============-----------------------------------------    
             }
}

//=======================================================================
// Created by  : [MAE]MahAbdel-aziz 
// Date        : 09/08/2007
// Description : Validate on txtRegularHours is between 1 and 24 
// input       : Nothing 
//Screen       :frmGrades
//======================================================================== 
 function txtRegularHours_Changed()
 {
    var txtRegularHours=window.document.getElementById("txtRegularHours");
    var val =txtRegularHours.value
    if (val>24)
    {
        txtRegularHours.value=24
    }
    if(val<=0)
    {
    txtRegularHours.value=1
    }
 }







function CheckShfitsValidity(str)
{
    str = str.Trim(',');
    var arr = str.split(',');
}


//-------------------------------=============-----------------------------------------






//    '========================================================================
//    'ProcedureName  :  WebDateTimeEdit2_ValueChange
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Check that to time in shift not less that from time 
//    'Developer      :[MAE]Mah Abdel-aziz   
//    'Date Created   :09-09-2007
//    'Modifacations  : 
//    '========================================================================




//frm FiscalYearPeriod

//function frmFYPWebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent){
//	
//	        var oComboFrom = igdrp_getComboById('WebDateChooser1');
//	        var oComboTo = igdrp_getComboById('WebDateChooser2');
//        	
//        	var valueFrom  = oComboFrom.getValue()
//        	var nD = new Date
//       	    nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate()+1)
//        	if (newValue <= valueFrom)
//        	{
//        	    oComboTo.setValue(nD)
//        	}
//}

//function frmFYPWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent){
//	     var oComboTo = igdrp_getComboById('WebDateChooser2');
//	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
//        	
//        	var valueTo  = oComboTo.getValue()
//        	var nD = new Date
//       	    nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate()-1)
//       	    //nD.setFullYear(newValue.getFullYear(),newValue.getMonth()+1,newValue.getDate()-1)
//       	    
//       	    if (newValue >= valueTo)
//        	{
//        	    oComboFrom.setValue(nD)
//        	    //oComboTo.setValue(nD)
//        	}
//}
//End FrmFiscalYearPeriods




//================================================================================================
 //'========================================================================

//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Vlaidate on from time less than to date 
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  26-09-2007
//    'Modifacations  : 
//    '========================================================================
function frmEmployeeClasstxtStartTime_ValueChange(oEdit, oldValue, oEvent){
	var fromTime = igedit_getById("txtStartTime");
	var toTime = igedit_getById("txtEndTime");
	if(fromTime.getValue()!=null && toTime.getValue()!=null)
	{
	    if(toTime.getValue()< fromTime.getValue())
	    {
	        //toTime.setValue(fromTime.getValue);
	        toTime.setText(fromTime.txt);
	    }
	}
}
function frmEmployeeClasstxtEndTime_ValueChange(oEdit, oldValue, oEvent){
	var fromTime = igedit_getById("txtStartTime");
	var toTime = igedit_getById("txtEndTime");
	if(fromTime.getValue()!=null && toTime.getValue()!=null)
	{
	    if(toTime.getValue()< fromTime.getValue())
	    {
	        //fromTime.setValue(toTime.getValue);
	        fromTime.setText(toTime.txt);
	    }
	}
}
//================================================================================================



//===============================================================================================
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Vlaidate on start date less than end date 
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  26-09-2007
//    'Modifacations  : 
function frmTrainingwdcStartDate_ValueChanged(oDateChooser, newValue, oEvent){
	 var startDate = igdrp_getComboById('wdcStartDate');
	 var endDate = igdrp_getComboById('wdcEndDate');
	 var sVal = startDate.getValue();
	 var eVal = endDate.getValue();
	 if(eVal <sVal)
	 {
	    endDate.setValue(startDate.getValue());
	 }
}

function frmTrainingwdcEndDate_ValueChanged(oDateChooser, newValue, oEvent){
	var startDate = igdrp_getComboById('wdcStartDate');
	 var endDate = igdrp_getComboById('wdcEndDate');
	 var sVal = startDate.getValue();
	 var eVal = endDate.getValue();
	 
	 if(eVal <sVal)
	 {
	    startDate.setValue(endDate.getValue());
	 }
}
//===============================================================================================



//===============================================================================================
function frmItemsWebDateChooser1_ValueChanged(oDateChooser, newValue, oEvent){
	        var oComboFrom = igdrp_getComboById('WebDateChooser1');
	        var oComboTo = igdrp_getComboById('WebDateChooser2');
        	
        	var valueFrom  = oComboFrom.getValue()
        	var nD = new Date
//       	    nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate()+1)
//        	if (newValue <= valueFrom)
//        	{
//        	    oComboTo.setValue(nD)
//        	}

            nD.setFullYear(valueFrom.getFullYear(),valueFrom.getMonth(),valueFrom.getDate())
        	if (oComboFrom.getValue() > oComboTo.getValue())
        	{
        	    //oComboTo.setValue(nD)
        	    oComboTo.setValue(oComboFrom.getValue());
        	}
}

function frmItemsWebDateChooser2_ValueChanged(oDateChooser, newValue, oEvent){
	     var oComboTo = igdrp_getComboById('WebDateChooser2');
	     var oComboFrom = igdrp_getComboById('WebDateChooser1');
        	
        	var valueTo  = oComboTo.getValue()
        	var nD = new Date
//       	    nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate()-1)
//       	    
//       	    if (newValue >= valueTo)
//        	{
//        	    oComboFrom.setValue(nD)
//        	}

            nD.setFullYear(valueTo.getFullYear(),valueTo.getMonth(),valueTo.getDate())
       	    
       	    if (oComboFrom.getValue() > oComboTo.getValue())
        	{
        	    //oComboFrom.setValue(nD)
        	     oComboFrom.setValue(oComboTo.getValue());
        	}
}
//==============================================================================================





//   '========================================================================
//    'ProcedureName  :  GetRowIndexFromCellId 
//    'Screen         :  frmEmployeeMonthlyTransactions
//    'Project        :  Venus V.
//    'Description    :  Get row index from cell id
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  03-09-2007
//    'fn. Arguments  :
//    '---------------------------------------------------------
//    'Parmeter Name      : Data Type : Description
//    '---------------------------------------------------------
//     cellId             :cell id
//    '========================================================================
    function GetRowIndexFromCellId(cellId)
    {
        var arr = cellId.split('_');
        var tt = arr[2]-1;
        tt = tt +1;
        return tt;
    }
    
    
    function CompareTwoTime(time1,time2)
    {
        var arrAP1   = time1.split(" ");
        var arrHMS1  = arrAP1[0].split(":");
        
        var arrAP2   = time2.split(" ");
        var arrHMS2  = arrAP2[0].split(":");
        
        var h1       = ConvertToNumber(arrHMS1[0]);
        var m1       = ConvertToNumber(arrHMS1[1]);
        var s1       = ConvertToNumber(arrHMS1[2]);
        var ap1      = arrAP1[1];
        
        var h2       = ConvertToNumber(arrHMS2[0]);
        var m2       = ConvertToNumber(arrHMS2[1]);
        var s2       = ConvertToNumber(arrHMS1[2]); 
        var ap2      = arrAP2[1];
        
        if(ap1=="PM")
        {
            h1 = h1+12;
        }
        else
        {
            if (h1 == 12)
            {
                h1 = 0;
            }
        }
        if(ap2=="PM")
        {
            h2= h2+12;
        }
        else
        {
            if(h2==12)
            {
                h2 = 0;
            }
        }
        if(h1 > h2)
        {
            return -1;
        }
        else if (h1<h2)
        {
            return 1;
        }
        else
        {
            if(m1>m2)
            {
                return -1
            }
            else if (m1<m2)
            {
                return 1;
            }
            else
            {
                if(s1 > s2)
                {
                    return -1;
                }
                else if (s1<s2)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        
     
    }
    
    
    function ConvertToNumber(objValue)
    {
        var tmp= objValue-1;
	    tmp = tmp+1;
	    return tmp;
    }//End of function ConvertToNumber
    
    
    
    
    
    //================================================================================================
    //frmEmployeeMonthlyTransactions
    
     
    //================================================================================================
    
    //frmEmployeesVactionTransactions
    
    //=================================================================================================
    function frmEmpVacTransuwgPayabilities_AfterCellUpdateHandler(gridName, cellId){
	  var grid              = igtbl_getGridById(gridName);
	  var cell              = igtbl_getCellById(cellId);
	  
	  if(cell.Column.Index == 1)
	  {
	    var val = cell.getValue();
	    if(val <0)
	    {
	        cell.setValue(0);
	    }
	  }
    }
    //=================================================================================================
    
    
    
    
function frmUsertxtPasswordChangedOn_ValueChanged(oDateChooser, newValue, oEvent){
	 var changOn = igdrp_getComboById('txtPasswordChangedOn');
	 var expiryAt = igdrp_getComboById('txtPasswordExpiryAt');
	 
	 var cOn = changOn.getValue();
	 var eAt = expiryAt.getValue();
	 if(cOn != null && eAt != null)
	 {
	    if (cOn > eAt)
	    {
	        expiryAt.setValue(cOn);
	    }
	 }
	 
	 
}

function frmUsertxtPasswordExpiryAt_ValueChanged(oDateChooser, newValue, oEvent){
	 var changOn = igdrp_getComboById('txtPasswordChangedOn');
	 var expiryAt = igdrp_getComboById('txtPasswordExpiryAt');
	 
	 var cOn = changOn.getValue();
	 var eAt = expiryAt.getValue();
	 if(cOn != null && eAt != null)
	 {
	    if (cOn > eAt)
	    {
	        changOn.setValue(eAt);
	    }
	 }
}

//===============================================================================================
 /*
    ========================================================================
    'Screen         :  frmEmployeesMonthlyTransactions
    'Description    :  Validate on Working Hours and Overtime greater than 0 and summation less than or equal 24
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  18-01-2008
    '========================================================================
*/

//===============================================================================================
var isitFirst = true
function frmEmpProjTransuwgEmployeeProjectsTransactions_AfterCellUpdateHandler(gridName, cellId){

        
        if (! isitFirst )
            return
        
        var grid              = igtbl_getGridById(gridName);
	    var cell              = igtbl_getCellById(cellId);
	    var Row               = igtbl_getRowById(cellId);
	    var RowIndex          =	Row.getIndex();	
	    var rowIndexC         = GetRowIndexFromCellId(cellId);
	    
	    if(RowIndex != rowIndexC && RowIndex != -1)
	    {
	           RowIndex = rowIndexC;
	    } 
	    
	    var workHoursCell ;
	    var overtimeCell ;
	    var useDefaultCell;
	    var nonWorkingCell;
	    var workHoursVal;
	    var overtimeVal;
	    var useDefaultVal;
	    var nonWorkingVal;
	    workHoursCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_3");
	    if(workHoursCell == null)
	    {
	       workHoursCell=igtbl_getCellById(gridName+"_anc_3");
	    }
	        
	    overtimeCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_4");
	    if(overtimeCell == null)
	    {
	       overtimeCell=igtbl_getCellById(gridName+"_anc_4");
	       
	    }
	    
	    useDefaultCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_5");
	    if(useDefaultCell == null)
	    {
	       useDefaultCell=igtbl_getCellById(gridName+"_anc_5");
	       
	    }
	    
	    nonWorkingCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_6");
	    if(nonWorkingCell == null)
	    {
	       nonWorkingCell=igtbl_getCellById(gridName+"_anc_6");
	       
	    }
	    
	    workHoursVal = workHoursCell.getValue();
	    overtimeVal  = overtimeCell.getValue();
	    useDefaultVal = useDefaultCell.getValue();
	    nonWorkingVal = nonWorkingCell.getValue(); 
	    if (cell.Column.Index ==3 || cell.Column.Index ==4)
	    {
	        if (cell.getValue() < 0)
	        {
	            isitFirst = false
	            cell.setValue(0);
	            isitFirst = true
	        }
	    }
	    if(  workHoursVal !=null && overtimeVal != null  ) 
	    {
	        if((workHoursVal+overtimeVal) >24)
	        {
	            if(workHoursVal < 24)
	            {
	                isitFirst = false
	                overtimeCell.setValue((24-workHoursVal));
	                isitFirst = true
	            }
	            else if(workHoursVal >= 24)
	            {
	                isitFirst = false
	                overtimeCell.setValue(0);
	                workHoursCell.setValue(24);
	                isitFirst = true
	            }
	        }
	    }
	    else if (workHoursVal !=null && overtimeVal == null)
	    {
	        if (workHoursVal >24)
	        {
	            isitFirst = false
	            workHoursCell.setValue(24);
	            isitFirst = true
	        }
	    }
	    else if (workHoursVal ==null && overtimeVal != null)
	    {
	        isitFirst = false
	        overtimeCell.setValue(0);
	        isitFirst = true
	        //alert("Please Enter Working Hours First");
	    }
	    //======================= Add OverTime Values [Start]
	   
	    overtimeVal  = overtimeCell.getValue();
	    var oldVal =  overtimeCell._oldValue;
	    if (oldVal=="" || oldVal ==null)
	    {
	        oldVal = 0;
	    }
	    var txtOvertimeWorkHours = igedit_getById("UltraWebTab1__ctl0_txtOvertimeWorkHours");
	    var txtHolidayWorkHours  = igedit_getById("UltraWebTab1__ctl0_txtHolidayWorkHours");
	    
	    var overtimeWorkH = txtOvertimeWorkHours.getValue();
	    if (overtimeWorkH == null)
	    {
	        overtimeWorkH = 0; 
	    }
	    var holidayWorkH  = txtHolidayWorkHours.getValue();
	    if (holidayWorkH == null)
	    {
	        holidayWorkH = 0;
	    }
	    if (useDefaultVal && nonWorkingVal  )
	    {
	        holidayWorkH = (holidayWorkH-oldVal)+overtimeVal;
	        txtHolidayWorkHours.setValue(holidayWorkH);
	    }
	    else if ((!useDefaultVal) && nonWorkingVal)
	    {
	    }
	    else
	    {
	       
           overtimeWorkH = (overtimeWorkH-oldVal)+overtimeVal;
           txtOvertimeWorkHours.setValue(overtimeWorkH);
	       
	    }
	    
	    
	    
	    //======================= Add OverTime Values [ End ]
	    
}

function frmEmpMonthlyTransactiontxtOvertimeWorkHours_ValueChange(oEdit, oldValue, oEvent){
	//curr task
	var txtOvertime = oEdit;
	var overtimeVal = txtOvertime.getValue();
	
	
    var str                 = new String();
    str                     = GetCookie("Lang"); 
    var msg;
    
    if( str.indexOf("ar")>-1)
    {
         msg          = " عدد الساعات الإضافية أقل من عدد ساعات توزيع العمل على المشاريع ";
         
    }
    else
    { 
         msg          = "Overtime hours is less than overtime hours in project transactions";
        
    }
    
    if (overtimeVal < oldValue )
    {
        txtOvertime.setValue(oldValue)
        alert(msg);
        return;
    }
	
	
	var tab = igtab_getTabById("UltraWebTab1");
	var cgrid = igtab_getElementById("uwgEmployeeProjectsTransactions", tab.element);
	var grid  = igtbl_getGridById(cgrid.id)
	
	var isDefaultTime
	var isNonWorkingTime
	var workingHours
	var overtimeHours
	
	var newOvertimeHours = overtimeVal - oldValue;
	
	for(i=0;i<grid.Rows.length;i++)
	{
	     var currRow        = igtbl_getRowById(grid.Id+"_r_"+i)//grid.Rows.rows[i];
	     
	     isDefaultTime      = currRow.getCellFromKey("UseDefaultTime").getValue();
	     isNonWorkingTime   = currRow.getCellFromKey("NonWorkingTime").getValue();
	     workingHours       = currRow.getCellFromKey("WorkingUnits").getValue();
	     overtimeHours      = currRow.getCellFromKey("OvertimeHours").getValue();
	     
	     if ((!isDefaultTime) && isNonWorkingTime)
	     {
	        continue;
	     }//If
	     else if (isDefaultTime && isNonWorkingTime)
	     {
	        if (24-overtimeHours > 0 )
	        {
	            if (24-overtimeHours >= newOvertimeHours)
	            {
	                currRow.getCellFromKey("OvertimeHours").setValue(overtimeHours+newOvertimeHours)
	                break;
	            }
	            else
	            {
	                currRow.getCellFromKey("OvertimeHours").setValue(24)
	                newOvertimeHours =newOvertimeHours-(24-overtimeHours)
	            }
	        }
	        
	            
	     }// End Else 
	     else
	     {
	        var WandO = 24-(overtimeHours+workingHours)
	        if (WandO > 0 )
	        {
	            if (WandO >= newOvertimeHours)
	            {
	                currRow.getCellFromKey("OvertimeHours").setValue(overtimeHours+newOvertimeHours)
	                break;
	            }
	            else
	            {
	                currRow.getCellFromKey("OvertimeHours").setValue(24-workingHours)
	                newOvertimeHours =newOvertimeHours-WandO
	            }
	        }
	     }
	     
	     
	}


}


 /*
    ========================================================================
    'Screen         :  frmEmployeesMonthlyTransactions
    'Description    :  Validate on from date less than or equal to date
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  18-01-2008
    '========================================================================
*/
function frmEmpProjTranswdcFrom_ValueChanged(oDateChooser, newValue, oEvent){

     var fromDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcFrom');
	 var toDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcTo');
	 
	 var fromVal = fromDate.getValue();
	 var toVal = toDate.getValue();
	 
	 if (fromVal != null && toVal != null)
	 {
	    if (toVal < fromVal)
	    {
	        toDate.setValue(fromVal);
	    }
	 }
	
}

/*
    ========================================================================
    'Screen         :  frmEmployeesMonthlyTransactions
    'Description    :  Validate on from date less than or equal to date
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  18-01-2008
    '========================================================================
*/
function frmEmpProjTranswdcTo_ValueChanged(oDateChooser, newValue, oEvent){

     var fromDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcFrom');
	 var toDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcTo');
	 
	 var fromVal = fromDate.getValue();
	 var toVal = toDate.getValue();
	 
	 if (fromVal != null && toVal != null)
	 {
	    if (toVal < fromVal)
	    {
	        fromDate.setValue(toVal);
	    }
	 }
	
}

/*
    ========================================================================
    'Screen         :  frmEmployeesMonthlyTransactions
    'Description    :  Fill Project column with selected project for specific period 
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  18-01-2008
    '========================================================================
*/
function frmEmpProjTransbtnFillClicked()
{
    var fromDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcFrom');
	var toDate = igdrp_getComboById('UltraWebTab1__ctl1_wdcTo');
	var gridName = "UltraWebTab1xxctl1xuwgEmployeeProjectsTransactions";
	var ddlProjects = igtab_getElementById("UltraWebTab1__ctl1_ddlProjects");//window.document.getElementById("ddlProjects");
	
	var dateCell ;
	var projectCell;
	var dateVal =new Date();
	var newProjectVal = ddlProjects.value;
	var fromDateVal   = fromDate.getValue();
	var toDateVal     = toDate.getValue();
	
	var grid = igtbl_getGridById(gridName);
	for(i=0;i<grid.Rows.length;i++)
	 {
	     var currRow = grid.Rows.rows[i];
	     try
	     {
	        dateCell = currRow.cells[1];
	        if (dateCell == "undefined" || dateCell == null)
	        {
	            dateCell = igtbl_getCellById(gridName+"_rc_"+ i +"_1");
	        }
	     }
	     catch (e)
	     {
	       dateCell = igtbl_getCellById(gridName+"_rc_"+ i +"_1");
           if(dateCell == null)
           {
              continue;
           }
	    }
	    
	    
	    //============================
	    var arrDate = dateCell.MaskedValue.split('/');
	    dateVal.setDate (arrDate[0]);
	    dateVal.setMonth(ConvertToNumber( arrDate[1])-1);
	    dateVal.setFullYear (arrDate[2]);
	    //dateVal = dateCell.getValue();
	     //============================
	    try
	     {
	        projectCell = currRow.cells[2];
	        if (projectCell == "undefined" || projectCell == null)
	        {
	            projectCell = igtbl_getCellById(gridName+"_rc_"+ i +"_2");
	        }
	     }
	     catch (e)
	     {
	       projectCell = igtbl_getCellById(gridName+"_rc_"+ i +"_2");
           if(projectCell == null)
           {
              continue;
           }
	    }
	    
	    //if (dateVal >= fromDateVal && dateVal <= toDateVal)
	    if ( (CompareTwoDates (dateVal,fromDateVal,false)==0 || CompareTwoDates (dateVal,fromDateVal,false)==1) && (CompareTwoDates(dateVal,toDateVal,false)==0 || CompareTwoDates(dateVal,toDateVal,false)==-1) )
	    {
	        projectCell.setValue(newProjectVal);
	    }
	    
	 
	 
	 }
}

/*
    ========================================================================
    'Description    :  Compare two dates and return 1 if first greater ,-1 if less and 0 if equal
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  18-01-2008
    '========================================================================
*/
function CompareTwoDates(date1 , date2,bdateTime)
{
    var dateVal1 = new Date();
    var dateVal2 = new Date();
    dateVal1 = date1;
    dateVal2 = date2;
    
    var year1  = dateVal1.getFullYear ();
    var year2  = dateVal2.getFullYear ();
    
    var month1 = dateVal1.getMonth ();
    var month2 = dateVal2.getMonth();
    
    var day1   = dateVal1.getDate ();
    var day2   = dateVal2.getDate ();
    
    var hours1 = dateVal1.getHours ();
    var hours2 = dateVal2.getHours ();
    
    var min1   = dateVal1.getMinutes ();
    var min2   = dateVal2.getMinutes ();
    
    
    if (year1 > year2)
    {
        return 1;
    }
    else if (year1 < year2 )
    {
        return -1;
    }
    else
    {
        if (month1 > month2)
        {
            return 1;
        }
        else if (month1 < month2)
        {
            return -1;
        }
        else
        {
            if (day1 > day2)
            {
                return 1;
            }
            else if (day1 < day2)
            {
                return -1;
            }
            else
            {
                if(!bdateTime)
                {
                    return 0;
                }
                else
                {
                    if (hours1 > hours2)
                    {
                        return 1;
                    }
                    else if (hours1 < hours2 )
                    {
                        return -1;
                    }
                    else
                    {
                        if (min1 > min2)
                        {
                            return 1;
                        }
                        else if (min1 < min2)
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
    }
    
    
}



//frmEmployeeTraining
function frmEmpTrainingUwgTraining_AfterCellUpdateHandler(gridName, cellId){

	    var grid              = igtbl_getGridById(gridName);
	    var cell              = igtbl_getCellById(cellId);
	    var Row               = igtbl_getRowById(cellId);
	    var RowIndex          =	Row.getIndex();	
	    var rowIndexC         = GetRowIndexFromCellId(cellId);
	    
	    if(RowIndex != rowIndexC && RowIndex != -1)
	    {
	           RowIndex = rowIndexC;
	    } 
	    
	    if (cell.Column.Index == 7)
	    {
	        if (cell.getValue() < 0)
	        {
	            cell.setValue(0);
	        }
	    }
	
	
}
//===================== frmEmployeeLoans [Start]
      
      
        
        
        function DeleteRow() 
        {
          var grid   =   igtbl_getGridById("uwgBenetitTemplet")
          var length =   grid.Rows.length
          for(intcounter=0;intcounter < length;intcounter++)
          {
              var rowname = grid.Rows.rows[0].Id 
              igtbl_deleteRow("uwgBenetitTemplet",rowname);
          }
        }


//   '========================================================================
//    'ProcedureName  :  frmEmployeesAttendance_uwgEmployeesAttendance_AfterCellUpdateHandler 
//    'Screen         :  frmEmployeesAttendance
//    'Project        :  Venus V.
//    'Description    : Validate that from time not greater than to time and reverse 
//    'Developer      :[MAE]Mah Abdel-aziz   
//    'Date Created   :05-09-2007
//    'fn. Arguments  :
//    '---------------------------------------------------------
//    'Parmeter Name      : Data Type : Description
//    '---------------------------------------------------------
//     gridName           : grid name
//     cellId             : cell id
//    '========================================================================
function frmEmployeesAttendance_uwgEmployeesAttendance_AfterCellUpdateHandler(gridName, cellId){

        var grid              = igtbl_getGridById(gridName);
	    var cell              = igtbl_getCellById(cellId);
	    var Row               = igtbl_getRowById(cellId);
	    var RowIndex          =	Row.getIndex();	
	    var rowIndexC         = GetRowIndexFromCellId(cellId);
	    
	    if(RowIndex != rowIndexC && RowIndex != -1)
	    {
	           RowIndex = rowIndexC;
	    } 
	    
	    var dateFrom;
	    var dateTo;
	    var timeFrom;
	    var timeTo;
	    
	    dateFrom =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_1");
	    if(dateFrom == null)
	    {
	       dateFrom=igtbl_getCellById(gridName+"_anc_1");
	    }
	    
	    timeFrom =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_2");
	    if(timeFrom == null)
	    {
	       timeFrom=igtbl_getCellById(gridName+"_anc_2");
	    }
	    
	    dateTo =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_3");
	    if(dateTo == null)
	    {
	       dateTo=igtbl_getCellById(gridName+"_anc_3");
	    }
	   
        timeTo =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_4");
	    if(timeTo == null)
	    {
	       timeTo=igtbl_getCellById(gridName+"_anc_4");
	    }
	    
	    var dateFromVal = dateFrom.getValue();
	    var dateToVal   = dateTo.getValue();
	    var timeFromVal = timeFrom.getValue();
	    var timeToVal   = timeTo.getValue();
	    
	    if(dateFromVal != null && dateToVal != null)
	    {
	        if (dateToVal < dateFromVal)
	        {
	            dateTo.setValue(dateFromVal);
	        }
	        var diff   = CompareTwoTime(timeToVal, timeFromVal);
	        var isequl = IsDatesEqual(dateToVal,dateFromVal); 
	        if(isequl && diff==1 )
	        {
	            timeTo.setValue(timeFromVal);
	        }
	    }
	    
	    
	    	    
	    /*
	    if(cell.Column.Index == 3)
	    {
	        var prevCell =cell.getPrevCell();
	        if(cell.getValue() < prevCell.getValue())
	        {
	            cell.setValue(cell._oldValue);
	        }
	    }//End if(cell.Column.Index == 3)
	     if(cell.Column.Index == 2)
	    {
	        var nextCell =cell.getNextCell();
	        if(cell.getValue() > nextCell.getValue())
	        {
	            cell.setValue(cell._oldValue);
	        }
	    }//End if(cell.Column.Index == 2)
	    */
	    
	    
	
}//End function frmEmployeesAttendance_uwgEmployeesAttendance_AfterCellUpdateHandler




//===============================================================================================
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  CEll Click
//    'Developer      :  [MAE]Mah Abdel-aziz   
//    'Date Created   :  14-01-2008
//    'Screen         :  frmEmployeesAttendance
//    'Modifacations  : 
//===============================================================================================

var intNextEditCell = -1;
var intNextEditRow  = -1; 

//   ========================================================================
//   ProcedureName  :  uwgEmployeesAttendance_EditKeyDownHandler 
//   Screen         :  frmEmployeesAttendance
//   Project        :  Venus V.
//   Description    :  Calls the function gridTabOrder 
//   Developer      :  [0260]   
//   Date Created   :  21-04-2008
//   ---------------------------------------------------------
//   Parmeter Name      : Data Type : Description
//   ---------------------------------------------------------
//    gridName           : grid name
//    cellId             : cell id
//    key                : key pressed
//   ========================================================================


/*
function uwgEmployeesAttendance_EditKeyDownHandler(gridName, cellId, key){
	    var grid              = igtbl_getGridById(gridName);
	    var cell              = igtbl_getCellById(cellId);
	    var Row               = igtbl_getRowById(cellId);
	    var RowIndex          =	Row.getIndex();	
	    var rowIndexC         = GetRowIndexFromCellId(cellId);
	    
	    if(RowIndex != rowIndexC && RowIndex != -1)
	    {
	           RowIndex = rowIndexC;
	    } 
	    
	    var dateFrom;
	    var dateTo;
	    var timeFrom;
	    var timeTo;
	    
	    dateFrom =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_1");
	    if(dateFrom == null)
	    {
	       dateFrom=igtbl_getCellById(gridName+"_anc_1");
	    }
	    
	    timeFrom =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_2");
	    if(timeFrom == null)
	    {
	       timeFrom=igtbl_getCellById(gridName+"_anc_2");
	    }
	    
	    dateTo =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_3");
	    if(dateTo == null)
	    {
	       dateTo=igtbl_getCellById(gridName+"_anc_3");
	    }
	   
        timeTo =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_4");
	    if(timeTo == null)
	    {
	       timeTo=igtbl_getCellById(gridName+"_anc_4");
	    }
	    
	    if(cell.Column.Index == 2 && (key == 13||key==9) )
	    {
	       
	      timeTo.activate();
	      timeTo.beginEdit();
	      
	      intNextEditRow = RowIndex ;
	      intNextEditCell  =4;
	      
         //-------------------------------=============-----------------------------------------
	      
	      //dateTo.focus();
	    }
	    if(cell .Column.Index == 4 && (key == 13||key==9) )
	    {
	        var NextRow = Row.getNextRow();
	        if(NextRow != null)
	        {
	            var NextRowIndex  = NextRow.getIndex();
	            var arrCell = cellId .split('_');
	            var NextCellId = arrCell[0]+'_'+arrCell[1]+'_'+NextRowIndex+'_1';
	            
	            var NextCell              = igtbl_getCellById(NextCellId);
	            
	            NextCell.activate();
	            NextCell.beginEdit();
	            
	            intNextEditRow = NextRowIndex;
	            intNextEditCell = 1;
	            //NextCell.focus();
	        }
	    }
}

*/

//================= Check on DG Events [Start]

//   ========================================================================
//   ProcedureName  :  uwgEmployeesAttendance_KeyDownHandler 
//   Screen         :  frmEmployeesAttendance
//   Project        :  Venus V.
//   Description    :  focus to the time-in,time-out cells by pressing tab or enter.  
//                       if the cell is not editable
//   Developer      :  [AIM]   
//   Date Created   :  21-04-2008
//   ---------------------------------------------------------
//   Parmeter Name      : Data Type : Description
//   ---------------------------------------------------------
//    gridName           : grid name
//    cellId             : cell id
//   ========================================================================
//=============================[0260] [Start]
var NextCell;
function uwgEmployeesAttendance_KeyDownHandler(gridName, cellId,key)
{
  if(key==9 || key ==13)
    {
        gridTabOrder(gridName,cellId);
        return true;
    }
}
function uwgEmployeesAttendance_EditKeyDownHandler(gridName, cellId,key)
{
  if(key==9 || key ==13)
    {
        gridTabOrder(gridName,cellId);
        return true;
    }
}
//   ========================================================================
//   ProcedureName  :  gridTabOrder
//   Screen         :  frmEmployeesAttendance
//   Project        :  Venus V.
//   Description    :  focus to the time-in,time-out cells by pressing tab or enter.  
//   Developer      :  [AIM]   
//   Date Created   :  21-04-2008
//   ---------------------------------------------------------
//   Parmeter Name      : Data Type : Description
//   ---------------------------------------------------------
//    gridName           : grid name
//    key                : key pressed
//    cellId             : cell id
//   ========================================================================
function gridTabOrder(gridName, cellId)
{ 

    var grid        = igtbl_getGridById(gridName);
    var cell        = igtbl_getCellById(cellId);
    var NextCell    = cell;
    if(cell.Column.Index < 2 )
       NextCell  = cell.Row.getCellFromKey("CheckInT");
    if(cell.Column.Index >= 2)
       NextCell  = cell.Row.getCellFromKey("CheckOutT");   
    if(cell.Column.Index == 4)
    {    
        var nextindex=cell.Row.getIndex()+1;
        // if(nextindex = grid.Rows.rows.length) nextindex=0;
        var NextRow=  igtbl_getRowById(gridName+"_r_"+nextindex);
        if (NextRow == null)
        {
            nextindex = 0
            NextRow=  igtbl_getRowById(gridName+"_r_"+nextindex);
        }
        NextCell  =  NextRow.getCellFromKey("CheckInT");   
    }
    if(NextCell!=undefined)
    {   
        NextCell.activate();
        NextCell.beginEdit();
        return true;
    }  
}



//================= Check on DG Events [ End ]



function frmCompaniestxtSeparatorChanged()
{
    var str = new String();
     var webTab = igtab_getTabById("UltraWebTab1");
   if(webTab == null)
   return ;
   var txtSep = webTab.findControl("txtSeparator"); 
   

    
    var str = txtSep.value;
    if( str.length != 1 )
    {
        txtSep.value = ',';
    }
    
}
//=============== Check that Separator length = 1 [ End ]



var toolBarObj ;
function TlbMainNavigation_Click(oToolbar, oButton, oEvent){
	if (oButton.Key == "First" || oButton.Key == "Next" || oButton.Key == "Previous" || oButton.Key == "Last")
	{	 
	
	                        
	}
}
function callback_UseLessFn(retval)
{

}
function callback_GetRetVal(retVal)
{
     if (retVal.value == false)
     {  
         if (window.confirm("The data have been changed ,Are you sure you want to discard changes?"))
            {
                switch (oButton.Key)
	            {
	            case "First":
	                PageMethods.DoNavigation("First", callback_UseLessFn, OnSucceeded, OnFailed);
	                 break;
	            case "Next":
	                PageMethods.DoNavigation("Next", callback_UseLessFn, OnSucceeded, OnFailed);
	                 break;
	            case "Previous":
	                PageMethods.DoNavigation("Previous", callback_UseLessFn, OnSucceeded, OnFailed);
	                 break;
	            case "Last":
	                PageMethods.DoNavigation("Last", callback_UseLessFn, OnSucceeded, OnFailed);
	                 break;
	            }
            }
            else 
	        {
	            window.document.focus();
	        }
	 }//End If (retVal == false)
}



function CHeckChanges(toolBarId)
{
        
         var item                    = igtbar_getItemById("TlbMainToolbar_Item_1");
         alert(item.Id);

}

function Open_Search_KeyDown(SearchID,ControlName)
{
  var e=window.event;
  if (e.keyCode ==120)
  {
    var winopen =  window.open("frmSearchScreen.aspx?TargetControl="+ControlName+"&SearchID="+SearchID,"_Parent"+1,"height=525,width=724,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");		
	winopen.document.focus();
  }
}


function ShowAnnualSickStatus()
{
   
    var str                 = new String();
    str                     = GetCookie("Lang"); 
    var annualMsg;
    var sickMsg;
    if( str.indexOf("ar")>-1)
    {
         annualMsg          = "لا يوجد أجازة سنوية حتى الان";
         sickMsg            = "لا يوجد اجازة مرضية حتى الان";
    }
    else
    { 
         annualMsg          = "There is no annual vacation until now";
         sickMsg            = "There is no sick vacation until now";  
    }
    
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
        return ;
     
    var control                = igtab_getElementById("txtHidden", webTab.element) 
     
    var txtH = window.document.getElementById(control.id);
    
    var val  = txtH.value;
    var arr  = val.split(',')
    ///*
    control                = igtab_getElementById("chkAnnual", webTab.element)
    var ctrlAnnual          = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("chkSick", webTab.element)
    var ctrlSick            = window.document.getElementById(control.id);
    //*/
    control                = igtab_getElementById("lblErrorAnnual", webTab.element)
    var ctrlErrorAnnual     = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("lblErrorSick", webTab.element)
    var ctrlErrorSick       = window.document.getElementById(control.id); 
    
    //if ( ConvertToNumber( arr[0])>0 ||( ConvertToNumber( arr[0])<=0 && ctrlAnnual.checked ) )
      //   ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = ""
    if(ConvertToNumber( arr[0]) ==0  && ConvertToNumber(arr[4])==0 )
       ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = annualMsg;
    else
       ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = ""
    
    //if ( ConvertToNumber( arr[1])>0 ||( ConvertToNumber( arr[1])<=0 && ctrlSick.checked ))
        //ctrlErrorSick.value   = ctrlErrorSick.innerText   = "";
    if(ConvertToNumber(arr[1])==0 && ConvertToNumber(arr[5])==0)
       ctrlErrorSick.value   = ctrlErrorSick.innerText   = sickMsg;
    else
        ctrlErrorSick.value   = ctrlErrorSick.innerText   = ""; 
     
}

function ShowAnnualSickStatus()
{
   
    var str                 = new String();
    str                     = GetCookie("Lang"); 
    var annualMsg;
    var sickMsg;
    if( str.indexOf("ar")>-1)
    {
         annualMsg          = "لا يوجد أجازة سنوية حتى الان";
         sickMsg            = "لا يوجد اجازة مرضية حتى الان";
    }
    else
    { 
         annualMsg          = "There is no annual vacation until now";
         sickMsg            = "There is no sick vacation until now";  
    }
    
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
        return ;
     
    var control                = igtab_getElementById("txtHidden", webTab.element) 
     
    var txtH = window.document.getElementById(control.id);
    
    var val  = txtH.value;
    var arr  = val.split(',')
    ///*
    control                = igtab_getElementById("chkAnnual", webTab.element)
    var ctrlAnnual          = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("chkSick", webTab.element)
    var ctrlSick            = window.document.getElementById(control.id);
    //*/
    control                = igtab_getElementById("lblErrorAnnual", webTab.element)
    var ctrlErrorAnnual     = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("lblErrorSick", webTab.element)
    var ctrlErrorSick       = window.document.getElementById(control.id); 
    
    //if ( ConvertToNumber( arr[0])>0 ||( ConvertToNumber( arr[0])<=0 && ctrlAnnual.checked ) )
      //   ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = ""
    if(ConvertToNumber( arr[0]) ==0  && ConvertToNumber(arr[4])==0 )
       ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = annualMsg;
    else
       ctrlErrorAnnual.value = ctrlErrorAnnual.innerText = ""
    
    //if ( ConvertToNumber( arr[1])>0 ||( ConvertToNumber( arr[1])<=0 && ctrlSick.checked ))
        //ctrlErrorSick.value   = ctrlErrorSick.innerText   = "";
    if(ConvertToNumber(arr[1])==0 && ConvertToNumber(arr[5])==0)
       ctrlErrorSick.value   = ctrlErrorSick.innerText   = sickMsg;
    else
        ctrlErrorSick.value   = ctrlErrorSick.innerText   = ""; 
     
}

function CheckAnnualVaction()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
        return ;
     
    var control                = igtab_getElementById("chkAnnual", webTab.element) 
    
    var ctrlAnnual          = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("chkSick", webTab.element) 
    var ctrlSick            = window.document.getElementById(control.id);
    //var txtCode             = window.document.getElementById("txtCode");
    
    if (ctrlSick.checked  && ctrlAnnual.checked)
        ctrlSick.checked = false
   
   SetHiddenTxt() 
}
function CheckSickVacation()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
        return ;
     
    var control                = igtab_getElementById("chkAnnual", webTab.element) 
    
    var ctrlAnnual          = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("chkSick", webTab.element) 
    var ctrlSick            = window.document.getElementById(control.id);
    //var txtCode             = window.document.getElementById("txtCode");
    
    if (ctrlSick.checked  && ctrlAnnual.checked)
        ctrlAnnual.checked = false
    
    SetHiddenTxt()  
}

function SetHiddenTxt()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    if(webTab == null)
        return ;
     
    var control                = igtab_getElementById("txtHidden", webTab.element) 
    
    var txtH                = window.document.getElementById(control.id);
    var val                 = txtH.value;
    var arr                 = val.split(',')
    
    control                = igtab_getElementById("chkAnnual", webTab.element) 
    var ctrlAnnual          = window.document.getElementById(control.id);
    
    control                = igtab_getElementById("chkSick", webTab.element) 
    var ctrlSick            = window.document.getElementById(control.id);
    
    
    var str = new String();
    str  =GetCookie("Lang"); 
    var confirmMsg ;
    var confirmMsg2 ;
    if( str.indexOf("ar")>-1)
    {
        confirmMsg=" يوجد أجازة سنوية بالفعل . هل تريد جعل هذا الحقل اجازة سنوية؟"
        confirmMsg2=" يوجد أجازة مرضية بالفعل . هل تريد جعل هذا الحقل اجازة مرضية؟"
    }
    else
    {
        confirmMsg="Already found Annual Vacation, Would you like to set this record as Annual Vacation ?"
        confirmMsg2="Already found Sick Vacation, Would you like to set this record as Sick Vacation ?"
    }
    
    if(ctrlAnnual.checked)
    {
        if ( ConvertToNumber(arr[4])==1 && ConvertToNumber(arr[2])==0 )
        {
            /*
            if (confirm(confirmMsg))
            {
                
            }
            else
            {
                ctrlAnnual.checked = false
            }
            */
        }
        else if (ConvertToNumber(arr[2])==1 )
        {
            arr[4] = 1
        }
        arr[0] = 1
    }
    else
    {
        if ( ConvertToNumber(arr[2])==1 )
        {
           arr[4] = 0
        }
        arr[0] = 0
    }
    
    
    if(ctrlSick.checked)
    {
        if ( ConvertToNumber(arr[5])==1 && ConvertToNumber(arr[3])==0 )
        {
            /*
            if (confirm(confirmMsg2))
            {
                
            }
            else
            {
                ctrlAnnual.checked = false
            }
           */
        }
        else if (ConvertToNumber(arr[3])==1 )
        {
            arr[5] = 1
        }
        arr[1] = 1
    }
    else
    {
        if ( ConvertToNumber(arr[3])==1 )
        {
           arr[5] = 0
        }
        arr[1] = 0
    }
    
    var finalStr = new String()
    finalStr     = arr[0]+','+arr[1]+','+arr[2]+','+arr[3]+','+arr[4]+','+arr[5];
    txtH.value = finalStr;
    
    ShowAnnualSickStatus()
    
    
}




var SubmitFlag=0;

function Do_Submit_Flag()
{
    if(SubmitFlag==0)
    {
     return true;
    }
      if(SubmitFlag==1)
      {
      return false;
      }
}



//======================== Other Fields [Start]
function TransactionsTypes_Check_Other_Fields(Interfaces_frm)
{
    var tlbControl =  igtbar_getToolbarById("TlbMainToolbar");
    SubmitFlag=0;
	TlbMainToolbarNotNavigation_Click();    
	if (tlbControl.Items[11].Selected ==true)
	{
	    var txtCode = window.document.getElementById("txtCode")
	    var GetAddress='';
	    SubmitFlag=1;
	    PageMethods.Get_Other_Fields(txtCode.value, OnSucceeded, OnFailed);
	 
	}
}
//======================== Other Fields [ End ]


var isFirstTime = true
function frmGradesStepuwgGradesStepsTransactions_AfterCellUpdateHandler(gridName, cellId){
	
	if (!isFirstTime)
	    return
	var cell = igtbl_getCellById(cellId)
	var prevCell 
	
	var lang                = new String(); 
    var msg;
    lang                    = GetCookie("Lang");
   
    if (lang.indexOf("ar")>-1)
        msg = "القيمة يجب أن تكون بين ";
    else
        msg =" Amount must between";


    if (cell.Column.Index == 2) {
        prevCell = cell.getPrevCell()
        var valStr = new String()
        valStr = prevCell.Element.outerText

        var arr = valStr.split('(')[1].split(')')[0].split(',')

        var min = ConvertToNumber(arr[0])
        var max = ConvertToNumber(arr[1])

        if (ConvertToNumber(cell.getValue()) < min || ConvertToNumber(cell.getValue()) > max) {
            msg = msg + " " + min + " , " + max;
            isFirstTime = false
            if (ConvertToNumber(cell.getValue()) < min)
                cell.setValue(min)
            else
                cell.setValue(max)
            isFirstTime = true
            alert(msg)
        }
    }
}

var oEdit;
//function wdcDate_TextChanged(ctrlName, Interfaces_frm) {

//    try {
//        oEdit = igedit_getById("UltraWebTab1__ctl0_" + ctrlName)
//        if (oEdit == null)
//            oEdit = igedit_getById(ctrlName)
//    } catch (ex) {
//        oEdit = igedit_getById(ctrlName)
//    }
//    PageMethods.CheckDate(oEdit.getText(), OnSucceeded, OnFailed);
//}



function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'CheckDate') {

        var strEMessage = "InValid Date!"
        var strAMessage = "تاريخ غير صحيح"
        var strVal = result;
        var strArr = strVal.split(",")
        var intLang = ConvertToNumber(strArr[0])
        var intValid = ConvertToNumber(strArr[1])
        if (intValid == 0) {
            oEdit.setValue("")
            if (intLang == 1)
                alert(strAMessage)
            else
                alert(strEMessage)
            if (oEdit.webGrid != null) {
                oEdit.webGrid._getSelectedCells()[0].setValue("")
                oEdit.webGrid._getSelectedCells()[0].focus()
                oEdit.webGrid._getSelectedCells()[0].select()
                oEdit.webGrid._getSelectedCells()[0].beginEdit()
            }
            IsDataChanged = "F"
        }
    }
    else if (methodName == 'Get_Other_Fields') {
    window.open(result, "_Parent", "height=306px,width=602px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
	 
    }
}
function OnFailed(error) {
    alert(error.get_message());
}

