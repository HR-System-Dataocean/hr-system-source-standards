//   function btnCriteriaDisplay_Click(oButton, oEvent){
//	MainCollect_Start() 
//	//alert("dsdsdsdsdsd");
//	}

//    var StrInputNames='';
//    var StrReportCode='';
//    var StrRealSQLNames='';
//    var StrNit ='';
    
//    function SetParameter(Names,RealNames,ReportCode,RepFilters)
//    {
//        StrInputNames   = Names; 
//        StrReportCode   = ReportCode;  
//        StrRealSQLNames = RealNames;
//        StrNit          = RepFilters;
//    }
    
//    function TbrMainToolbar_Click(oToolbar, oButton, oEvent)
//    {
//	    MainCollect_Start() 
//	    //alert("ddddddddddddd");
//    }

    function MainCollect_Start()
    {
        
        var Arr = StrInputNames.split("/");
        var Arrsql = StrRealSQLNames.split("/");
               
        var ArrNip = StrNit.split("/");
        
        var Final_Value="";
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
                                 if (ArrNip[i].toUpperCase() == "LIKE")
                                   {
                                     Final_SQL   += "|" + Arrsql[i]                   + ArrNip[i] + " '$" + Control.value + "$' ";
                                   }
                                 else
                                   {
                                    Final_SQL   += "|"  + Arrsql[i]                   + ArrNip[i] + " '" + Control.value + "' ";
                                   }
                                 Values += "|" +Control.value ;                                      
                                 }
      		           break;
			        }
		        case("Dte"):
		            {
		                var Control       = igtab_getElementById(Arr[i],ultraTab.element);
		                if (Control.value!="")
                          {
                            Final_Value += "|" +  Arr[i].substring(3) ;
                                                                     
                            Final_SQL   += "|" +  Arrsql[i] + " " +  ArrNip[i] + " '" +Control.value +"'" ;
                            Values += "|" + Control.value ; 
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
                                 //Final_SQL   += "|" +  Arrsql[i] + "=" + Control.value ;
                              Final_SQL   += "|" +  Arrsql[i] + " " +  ArrNip[i] + " " +Control.value ;
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
                                Final_SQL   += "|" +  Arrsql[i] + " " +  ArrNip[i] + " " +Control.value ;
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
                                 Final_SQL   += "|" +  Arrsql[i] + " " +  ArrNip[i] + " " +Control.value ;
                                 Values += "|" + Control.options[Control.selectedIndex].text ;
                              }
  	                    break;
                    }           
    		    case("Drl"):
		            {
		                var Control       = igtab_getElementById(Arr[i],ultraTab.element);
		                if (Control.value!= "")
                          {
                            Final_Value += "|" +  Arr[i].substring(3) 
                            Final_SQL   += "|" +  Arrsql[i] + " " +  ArrNip[i] + " " +Control.value ;
                            Values += "|" + Control.options[Control.selectedIndex].text;
                          }
		  	            break;
		            }    
    		        
	            }
            }
            
            var hight =window.screen.availHeight -35;
	        var width =window.screen.availWidth -10;
	        var win =window.open("frmReportsGridViewerSti.aspx?Criteria=" + Final_Value.substring(1)+ "&ReportCode=" + StrReportCode+"&sq0="+Final_SQL.substring(1)+"&v="+Values.substring(1),"_NEW","height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
	        win.moveTo(0,0);
	        win.focus();
 }

// Removes leading whitespaces
function LTrim( value ) {
	
	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");
	
}

 Removes ending whitespaces
function RTrim( value ) {
	
	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");
	
}

////// Removes leading and ending whitespaces
function trim( value ) {
	
	return LTrim(RTrim(value));
            
        }


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

