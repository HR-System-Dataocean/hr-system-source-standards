// Author        : [0256]
// Date Created  : 7-2007 
// Date Modified : 20-04-2008 
// Description   : The file contains the other fields creation and data collection script
//=============================================================
 function SaveOtherFieldsData()
            {
                var Parameter = window.document.getElementById("name")
		        var realParameter = window.document.getElementById("realname")
    		
		        var Value =Parameter.innerText
		        var realValue = realParameter.innerText
    		
		        var Arr =Value.split("|");
		        var realArr = realValue.split("|");
		       		        
		        var Final_Value="";
		        var FinalTest ="";
		        //
		        var webTab = igtab_getTabById("UltraWebTab1");
                if(webTab == null)
                return;
                //var firstTab = webTab.Tabs[0];
        
                //window.alert("Label of 1st tab:" + firstTab.getText());
                //var webTab = igtab_getTabById("UltraWebTab1");
               //
		        for(i=0;i<Arr.length;i++)
		        {
		            
		            //var StrGroupKey    = new String(); 	    
		            //var StrCtrlName    = new String();        
		            var str            = new String();
		            //var start          = 0;
		            //var IntGroupLimit  = Arr[i].indexOf("$");
		            //StrGroupKey        = Arr[i].substring(0,IntGroupLimit);
		            //start = IntGroupLimit+1 ;
		            //str                = Arr[i].substring(start,start+3);
		            //StrCtrlName        = Arr[i].substring(IntGroupLimit+4);
		          
		            var str=Arr[i].substring(0,3)
		
				    switch(str)
				    {
				        case("WV_"):
				            {
				              var Control = webTab.findControl(Arr[i]);
                             //var Control =window.document.getElementById(Arr[i]);
		                      Final_Value += " @" + realArr[i] + "%" + Control.value;
		                      break;
						    }
					    case("WN_"):
					        {
					          var Control = webTab.findControl(Arr[i]);
					          //var Control =window.document.getElementById(Arr[i]);
					          Final_Value += " @" + realArr[i] + "%" + Control.value;
						      break;
					        }
					    case("WD_"):
					        {
					          var Control = webTab.findControl(Arr[i]);
					          var Control =igdrp_getComboById(Control.id)
					          if (Control.getValue()!= undefined)
					              Final_Value += " @" + realArr[i] + "%" + Control.getText() ;
						      break;}
					    case("WB_"):
					         {
					          var Control = webTab.findControl(Arr[i]);
					          //var Control  = window.document.getElementById(Arr[i]);
					          Final_Value += " @" + realArr[i] + "%" + Control.value
					          break;
					         }    
				        }
		            }
		
		          var Target_File = window.document.getElementById("value")
		          Target_File.value=Final_Value
		          //window.document.form1.submit() 
		          //window.close();
	            }


function btnCancelOtherFields_Click(oButton, oEvent){
         window.close();
}




