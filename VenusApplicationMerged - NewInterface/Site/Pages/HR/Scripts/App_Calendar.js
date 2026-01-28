//***********************************************************************************************
//-----------------------------------------------------------------------------------------------
//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
//    '========================================================================
//    'ProcedureName  :  uwgDays_AfterRowActivateHandler
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Show information about selected day
//    'Developer      :[MAE]Mah Abdel-aziz   
//    'Date Created   :09-09-2007
//    'Modifacations  : 
//    '========================================================================
var DayRowID      =-1;
function uwgDays_AfterRowActivateHandler(gridName, rowId){
	
	var grid = igtbl_getGridById(gridName);
	var Row  =  igtbl_getRowById(rowId);
	DayRowID = rowId;
	var RowIndex = Row.getIndex();
	
	//ClearAllTxt();
	//==========Start Clear All Txt================================
	var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit1");
    var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit2");
    txt1.setValue("");
    txt11.setValue("");
    
    var txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit3");
    var txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit4");
    txt2.setValue("");
    txt22.setValue("");
    
    var txt3 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit5");
    var txt33 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit6");
    txt3.setValue("");
    txt33.setValue("");
    
    var txt4 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit7");
    var txt44 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit8");
    txt4.setValue("");
    txt44.setValue("");
    
    var txt5 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit9");
    var txt55 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit10");
    txt5.setValue("");
    txt55.setValue("");
	//==========End Clear All Txt================================== 
	
	var chkCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_0");
	if(chkCell == null)
	{
	    chkCell = Row.cells[0]; 
	}
	var chkVal = chkCell.getValue();
	
	var dayCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_1");
	if(dayCell == null)
	{
	    dayCell = Row.cells[1];
	}
	var dayVal = dayCell.getValue();
	
	var sValCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_4");
	if(sValCell == null)
	{
	    sValCell = Row.cells[4];
	}
	var sValVal = sValCell.getValue();
	
	var shiftCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_5");
	if(shiftCell == null)
	{
	    shiftCell = Row.cells[5];
	}
	var shiftsVal = shiftCell.getValue();
	
	
	//if(chkVal == true)
	//{
	    if(sValVal != null)
	    {
	    var ControlName     = "UltraWebTab1__ctl0_rbtnSelectedDateType_"+sValVal ;
	    var Control         = window.document.getElementById(ControlName)        ;
	        Control.checked = true ;
	    }
	    
	    if( sValVal == 2 && shiftsVal != null )
	    {
	        var arr = shiftsVal.split(',');
	        var len = arr.length/2;
	        switch (len)
	        {
	            case 1:
	                txt1.setValue(arr[0]);
	                txt11.setValue(arr[1]);
	                
	                txt1.setEnabled(true);
	                txt11.setEnabled(true);
	                txt2.setEnabled(true);
	                txt22.setEnabled(true);
	                break;
	            case 2:
	                txt1.setValue( arr[0]);
	                txt11.setValue( arr[1]);
	                txt2.setValue( arr[2]);
	                txt22.setValue(arr[3]);
	                
	                txt1.setEnabled(true);
	                txt11.setEnabled(true);
	                txt2.setEnabled(true);
	                txt22.setEnabled(true);
	                txt3.setEnabled(true);
	                txt33.setEnabled(true);
	              
	                break;
	            case 3:
	                txt1.setValue(arr[0]);
	                txt11.setValue( arr[1]);
	                txt2.setValue(arr[2]);
	                txt22.setValue( arr[3]);
	                txt3.setValue( arr[4]);
	                txt33.setValue(arr[5]);
	                
	                txt1.setEnabled(true);
	                txt11.setEnabled(true);
	                txt2.setEnabled(true);
	                txt22.setEnabled(true);
	                txt3.setEnabled(true);
	                txt33.setEnabled(true);
	                txt4.setEnabled(true);
	                txt44.setEnabled(true);
	                break;
	            case 4:
	                txt1.setValue(arr[0]);
	                txt11.setValue(arr[1]);
	                txt2.setValue( arr[2]);
	                txt22.setValue( arr[3]);
	                txt3.setValue(arr[4]);
	                txt33.setValue( arr[5]);
	                txt4.setValue(arr[6]);
	                txt44.setValue( arr[7]);
	                
	                txt1.setEnabled(true);
	                txt11.setEnabled(true);
	                txt2.setEnabled(true);
	                txt22.setEnabled(true);
	                txt3.setEnabled(true);
	                txt33.setEnabled(true);
	                txt4.setEnabled(true);
	                txt44.setEnabled(true);
	                txt5.setEnabled(true);
	                txt55.setEnabled(true);
	               
	                break;
	            case 5:
	                txt1.setValue( arr[0]);
	                txt11.setValue( arr[1]);
	                txt2.setValue( arr[2]);
	                txt22.setValue( arr[3]);
	                txt3.setValue(arr[4]);
	                txt33.setValue( arr[5]);
	                txt4.setValue( arr[6]);
	                txt44.setValue( arr[7]);
	                txt5.setValue( arr[8]);
	                txt55.setValue(arr[9]);
	                
	                txt1.setEnabled(true);
	                txt11.setEnabled(true);
	                txt2.setEnabled(true);
	                txt22.setEnabled(true);
	                txt3.setEnabled(true);
	                txt33.setEnabled(true);
	                txt4.setEnabled(true);
	                txt44.setEnabled(true);
	                txt5.setEnabled(true);
	                txt55.setEnabled(true);
	                break;
	        }	    
	    } 	
}


//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

function ClearAllShiftsTimes()
{
    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit1");
    var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit2");
    txt1.setValue("");
    txt11.setValue("");
    txt1.setEnabled(false);
    txt11.setEnabled(false);
   
    
    var txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit3");
    var txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit4");
    txt2.setValue("");
    txt22.setValue("");
    txt2.setEnabled(false);
    txt22.setEnabled(false);
    
    var txt3 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit5");
    var txt33 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit6");
    txt3.setValue("");
    txt33.setValue("");
    txt3.setEnabled(false);
    txt33.setEnabled(false);
    
    var txt4 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit7");
    var txt44 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit8");
    txt4.setValue("");
    txt44.setValue("");
    txt4.setEnabled(false);
    txt44.setEnabled(false);
    
    var txt5 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit9");
    var txt55 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit10");
    txt5.setValue("");
    txt55.setValue("");
    txt5.setEnabled(false);
    txt55.setEnabled(false);
}


//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$


function rbtnSelectedDateTypeChanged()
{
    var gridName = "UltraWebTab1xxctl0xuwgDays";
    var grid = igtbl_getGridById(gridName);
    var dayRowSelected = true;
    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit1");
    var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit2");
    if(DayRowID == -1)
    {
        //return;
        dayRowSelected = false;
    }
    if(dayRowSelected)
	{
	    var Row;
	    var RowIndex;  
	
	    Row =  igtbl_getRowById(DayRowID);
	    RowIndex= Row.getIndex();
	
	
	var sValCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_4");
	if(sValCell == null)
	{
	    sValCell = Row.cells[4];
	}
    var sStatusCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_2");
	if(sStatusCell == null)
	{
	    sStatusCell = Row.cells[2];
	}
    if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_0.checked)
    {
       sValCell.setValue(0);
       sStatusCell.setValue("Default Time");
       ClearAllShiftsTimes();
    }
    else if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_1.checked)
    {
       sValCell.setValue(1);
       sStatusCell.setValue("Non Working Time");
       ClearAllShiftsTimes();
    }
    else if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_2.checked)
    {
       sValCell.setValue(2);
       sStatusCell.setValue("Non Default Time");
       txt1.setEnabled(true);
       txt11.setEnabled(true);
    }
    }//End of if(dayRowSelected)
    else
    {
        if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_0.checked)
        {
      
            ClearAllShiftsTimes();
        }
        else if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_1.checked)
        {
       
            ClearAllShiftsTimes();
        }
        else if (document.forms[0].UltraWebTab1__ctl0_rbtnSelectedDateType_2.checked)
        {   
            txt1.setEnabled(true);
            txt11.setEnabled(true);
        }
    }
    
}

//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$


//    '========================================================================
//    'ProcedureName  :  WebDateTimeEdit1_ValueChange
//    'Module         :  Hrs (Human Resource Module)
//    'Project        :  Venus V.
//    'Description    :  Check that from time in shift not greater that to time 
//    'Developer      :[MAE]Mah Abdel-aziz   
//    'Date Created   :09-09-2007
//    'Modifacations  : 
//    '========================================================================
function frmCalWebDateTimeEdit1_ValueChange(oEdit, oldValue, oEvent){
	
	var gridName = "UltraWebTab1xxctl0xuwgDays";
    var grid = igtbl_getGridById(gridName);
    var dayRowSelected = true;
    if(DayRowID == -1)
    {
        //return;
        dayRowSelected = false;
    }
    if(dayRowSelected)
    {
	var Row  =  igtbl_getRowById(DayRowID);
	var RowIndex = Row.getIndex();
	
	var shiftCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_5");
	if(shiftCell == null)
	{
	    shiftCell = Row.cells[5];
	}
	
	var str ="";
	
	var nextE = false;
	var oneE = false;
	var disabAll =false;
	for(i=1 ; i<=10; i=i+2)
	{
	    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+i);
        var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+1));
        var txt2;
        var txt22;
        if(nextE)
        {
            txt1.setEnabled(true);
            txt11.setEnabled(true);
            nextE=false;
            oneE = true;
        }
        if(disabAll)
        {
            txt1.setEnabled(false);
            txt11.setEnabled(false); 
            txt1.setValue("");
            txt11.setValue("");
        }
        if(i>=3)
        {
            var prevtxt =igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i-1)) 
            if(txt1.getValue() < prevtxt.getValue())
            {
                txt1.setValue(prevtxt.getValue());
            }
        }
        if(txt11.getValue() != null && txt1.getValue()!=null)
        {
            if(txt1.getValue() > txt11.getValue())
            {
               //txt1.setValue(txt11.getValue())
               txt11.setValue(txt1.getValue())
            }
            str = str + txt1.lastText +','+txt11.lastText+',';
            txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
            txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
            if(txt2!=null && txt22 !=null)
            if(!oneE && !txt2.getEnabled() && !txt22.getEnabled() && txt2.getValue()==null && txt22.getValue()== null )
            {
                    nextE = true;
            }
            disabAll = false;
        }//End if
        else if (txt11.getValue() == null || txt1.getValue()==null)
        {
            disabAll = true;
        }
        
   }//End for
   //var ch = ',';
   //str = str.trimEnd(ch).trim();
   if(str != "")
   {
        str= str.substring(0,str.length-2);
        shiftCell.setValue(str);
   }
   }//End of if(dayRowSelected)
   else
   {
      var nextE = false;
      var oneE = false;
      var disabAll =false;
      for(i=1 ; i<=10; i=i+2)
	  {
	    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+i);
        var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+1));
        var txt2;
        var txt22;
        if(nextE)
        {
            txt1.setEnabled(true);
            txt11.setEnabled(true);
            nextE=false;
            oneE=true;
        }
        if(disabAll)
        {
            txt1.setEnabled(false);
            txt11.setEnabled(false);
            txt1.setValue("");
            txt11.setValue(""); 
        }
        if(i>=3)
        {
            var prevtxt =igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i-1)) 
            if(txt1.getValue() < prevtxt.getValue())
            {
                txt1.setValue(prevtxt.getValue());
            }
        }
    
        if(txt11.getValue() != null && txt1.getValue()!=null)
        {
            if(txt1.getValue() > txt11.getValue())
            {
               //txt1.setValue(txt11.getValue())
               txt11.setValue(txt1.getValue())
            }
            txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
            txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
            if(txt2!=null && txt22 !=null)
            if(!oneE && !txt2.getEnabled() && !txt22.getEnabled() && txt2.getValue()==null && txt22.getValue()== null )
            {
                    nextE = true;
            }
            disabAll = false;
        }//End if
        else if (txt11.getValue() == null || txt1.getValue()==null)
        {
            disabAll = true;
        }
        
   }//End for
   }
    

}

//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

function frmCalWebDateTimeEdit2_ValueChange(oEdit, oldValue, oEvent){
    
    var gridName = "UltraWebTab1xxctl0xuwgDays";
    var grid = igtbl_getGridById(gridName);
    var dayRowSelected = true;
    if(DayRowID == -1)
    {
        dayRowSelected = false;
        //return;
    }
    if(dayRowSelected)
    {
	var Row  =  igtbl_getRowById(DayRowID);
	var RowIndex = Row.getIndex();
	
	var shiftCell =igtbl_getCellById(gridName+"_rc_"+ RowIndex +"_5");
	if(shiftCell == null)
	{
	    shiftCell = Row.cells[5];
	}
    var str ="";
    
    var nextE = false;
	var oneE = false;
	var disabAll =false;
    for(i=1 ; i<=10; i=i+2)
	{
	    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+i);
        var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+1));
        var txt2;
        var txt22;
        if(nextE)
        {
            txt1.setEnabled(true);
            txt11.setEnabled(true);
            nextE=false;
            oneE=true;
        }
        if(disabAll)
        {
            txt1.setEnabled(false);
            txt11.setEnabled(false); 
            txt1.setValue("");
            txt11.setValue("");
        }
        
        if(txt11.getValue() != null && txt1.getValue()!=null)
        {
            if(txt1.getValue() > txt11.getValue())
            {
                txt11.setValue(txt1.getValue())
                //txt1.setValue(txt11.getValue())
            }
             str = str + txt1.lastText +','+txt11.lastText+',';
             txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
             txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
             if(txt2!=null && txt22 !=null)
             if(!oneE && !txt2.getEnabled() && !txt22.getEnabled() && txt2.getValue()==null && txt22.getValue()== null )
             {
                    nextE = true;
             }
            disabAll = false;
        }//End if
        else if (txt11.getValue() == null || txt1.getValue()==null)
        {
            disabAll = true;
        }
        if((i+1)<=8)
         {
            var nexttxt = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
            var nexttxt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
            if (nexttxt.getValue()!=null &&  nexttxt.getValue() < txt11.getValue())
            {
               nexttxt.setValue("");
               nexttxt2.setValue("");
               disabAll = true; 
               i=i+2
            }
         }
   }//End for
   //var ch = ',';
   //str = str.trimEnd(c).trim();
   if(str != "")
   {
        str= str.substring(0,str.length-2);
        shiftCell.setValue(str);
   }
   }//End of if(dayRowSelected)
   else
   {
        var nextE = false;
	    var oneE = false; 
	    var disabAll =false;      
        for(i=1 ; i<=10; i=i+2)
	    {
	    var txt1 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+i);
        var txt11 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+1));
        var txt2;
        var txt22;
        if(nextE)
        {
            txt1.setEnabled(true);
            txt11.setEnabled(true);
            nextE=false;
            oneE=true;
        }
        if(disabAll)
        {
            txt1.setEnabled(false);
            txt11.setEnabled(false); 
            txt1.setValue("");
            txt11.setValue("");
        }
        if(txt11.getValue() != null && txt1.getValue()!=null)
            {
                if(txt1.getValue() > txt11.getValue())
                {
                txt11.setValue(txt1.getValue())
                //txt1.setValue(txt11.getValue())
                }
                txt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
                txt22 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
                if(txt2!=null && txt22 !=null)
                if(!oneE && !txt2.getEnabled() && !txt22.getEnabled() && txt2.getValue()==null && txt22.getValue()== null )
                {
                    nextE = true;
                }
                disabAll = false;
            }//End if
         else if (txt11.getValue() == null || txt1.getValue()==null)
         {
            disabAll = true;
         }
         if((i+1)<=8)
         {
            var nexttxt = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+2));
            var nexttxt2 = igedit_getById("UltraWebTab1__ctl0_WebDateTimeEdit"+(i+3));
            if (nexttxt.getValue()!=null &&  nexttxt.getValue() < txt11.getValue())
            {
               nexttxt.setValue("");
               nexttxt2.setValue("");
               disabAll = true; 
               i=i+2
            }
         }
        }//End for
   }
   
	
}//End of WebDateTimeEdit2_ValueChange




//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$





//- 0261 -- Validate Save 

var CalendardoSubmitTag = 0 ;
function frmCalendarDoSubmit()
{
   if(CalendardoSubmitTag==1)
      return  false;
   if(CalendardoSubmitTag==0 ) 
      return true;
}


function TlbMainToolbar_Click(oToolbar, oButton, oEvent)
{
    var tlbControl =  igtbar_getToolbarById(oToolbar);
    var ddlEmployeeClass = window.document.getElementById("UltraWebTab1__ctl0_ddlEmployeeClass");
    var lang   = new String(); 
    var retMsg = "";
    lang       = GetCookie("Lang");
    CalendardoSubmitTag = 0 ;
    
        if (lang.indexOf("ar")>-1)
        {
            retMsg  =" من فضلك اختر فئة الموظف أولا "
        }
        else
        {
            retMsg  = " Please Select Employee Class "
        }

	if (oButton.Key.toUpperCase() =="SAVE")
	{
	
        if (ddlEmployeeClass.value == "0")
        {
           CalendardoSubmitTag = 1 ;
           alert(retMsg);    
        }
        
    }
}





