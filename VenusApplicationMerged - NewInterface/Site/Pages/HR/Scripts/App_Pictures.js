
function uwgDocumentPictures_AfterSelectChangeHandler(gridName, id)
{
//------//Get Screen Controls--------------------------------------------------------------------------
    var webTab = igtab_getTabById("UltraWebTab1");
    //-----------------------------

    var txtID = igtab_getElementById("txtID", webTab.element);
    var ImgEmployee = igtab_getElementById("ImgEmployee", webTab.element);
    var lblName = igtab_getElementById("txtPhotoName", webTab.element);
    var txtExpireDate = igtab_getElementById("txtExpireDate", webTab.element);
    var btnLast = igtab_getElementById("btnLast", webTab.element);
    var btnNext = igtab_getElementById("btnNext", webTab.element);
    var btnPrevious = igtab_getElementById("btnPrevious", webTab.element);
    var btnFirst = igtab_getElementById("btnFirst", webTab.element);
    var chkIsDefault = igtab_getElementById("chkIsDefault", webTab.element);

//------//Get Row Data--------------------------------------------------------------------------------       

    var Grid = igtab_getElementById("uwgDocumentPictures", webTab.element);

    var Row = igtbl_getRowById(id);
        Grid.control.setActiveRow(Row);
//        //Row.select();
//        Row.setSelected=true ;
//        Row.activate = true ;
//          Row.getPrevRow();
//        Row.getNextRow() ;
      
        //--------------------------------------   
        
        var ID           = Row.getCell(0).getValue  () ;
        var ObjectID     = Row.getCell(1).getValue  () ;
        var RecordID     = Row.getCell(2).getValue  () ;
        var EngName      = Row.getCell(3).getValue  () ;
        var ArbName      = Row.getCell(4).getValue  () ;
        var FolderName   = Row.getCell(5).getValue  () ;
        var FileName     = Row.getCell(6).getValue  () ;
        var ExpiryDate   = Row.getCell(7).Element.innerText
        var DefaultPhoto = Row.getCell(8).getValue  () ;
   //-------//Bind Data To Controls-----------------------------------------------------------------------  

        //-----------------------------
        txtID .value            =   ID         ;
        txtID .innerText        =   ID         ;
        //-----------------------------
        ImgEmployee.src = "../../" + "Photos/" + ObjectID + "_" + RecordID + "/" + FileName;
       //-----------------------------
       lblName.value      = FileName ;
       lblName.innerText  = FileName ;
       if (DefaultPhoto == true)
       {
           chkIsDefault.checked = true;       
       }
       else
       {
           chkIsDefault.checked = false;
       }
        //-----------------------------
       if (ExpiryDate != null) {
           var strValue_0 = ExpiryDate.split(" ");
           txtExpireDate.Object.setValue(strValue_0[0]);
       }
       else {
           txtExpireDate.Object.setValue("");
       }
        
      
    //--------------------------------------------------------------------------------------------------------   
    btnLast.disabled       = true ;
    btnNext.disabled       = true ;
    btnPrevious.disabled   = false;
    btnFirst.disabled      = false;
}


function uwgDocumentPictures_BeforeRowActivateHandler (gridName, rowId)
{

    var row = igtbl_getRowById(rowId)
    if (isFormChanged())
    {
        var msg = returnDiscardMsg();
        if (window.confirm(msg))
        {
           IsDataChanged = "F";     
        }
        else
        {

            return 1
          
        }
    }
}






function GetNextRow() {

    var webTab = igtab_getTabById("UltraWebTab1");
    //-----------------------------
    var btnLast = igtab_getElementById("btnLast", webTab.element);
    var btnNext = igtab_getElementById("btnNext", webTab.element);
    var btnPrevious = igtab_getElementById("btnPrevious", webTab.element);
    var btnFirst = igtab_getElementById("btnFirst", webTab.element);

    var Grid = igtab_getElementById("uwgDocumentPictures", webTab.element);
    if (Grid.control.Rows.length != 0)
    {
    var intRowId    =  Grid.control.getActiveRow().Id ;
    var arrCount    =  intRowId.split("_");
    if (Grid.control.getActiveRow().Id != Grid.control.Rows.getLastRowId())
    {
    var rowCount    =  Math.abs(arrCount[2])+ 1  ;
    var realRowId   =  arrCount[0]+"_"+ arrCount[1] +"_"+ rowCount ; 
    uwgDocumentPictures_AfterSelectChangeHandler("uwgDocumentPictures", realRowId);
    }
    else
    {
     //alert("This Is Last Photo")
     btnLast.disabled       = true ;
     btnNext.disabled       = true ;
     btnPrevious.disabled   = false;
     btnFirst.disabled      = false;
     
    }
    }
}

function GetPreviousRow()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    //-----------------------------
    var btnLast = igtab_getElementById("btnLast", webTab.element);
    var btnNext = igtab_getElementById("btnNext", webTab.element);
    var btnPrevious = igtab_getElementById("btnPrevious", webTab.element);
    var btnFirst = igtab_getElementById("btnFirst", webTab.element);

    var Grid = igtab_getElementById("uwgDocumentPictures", webTab.element);
    if (Grid.control.Rows.length != 0)
    {
    var intRowId    =  Grid.control.getActiveRow().Id ;
    var arrCount    =  intRowId.split("_");
        if (Math.abs(arrCount[2]) != 0 )
        {
            var rowCount    =  Math.abs(arrCount[2])- 1  ;
            var realRowId   =  arrCount[0]+"_"+ arrCount[1] +"_"+ rowCount ; 
            uwgDocumentPictures_AfterSelectChangeHandler("uwgDocumentPictures", realRowId);
        }
        else
        {
          //alert("This Is First Photo")
        btnLast.disabled       = false ;
        btnNext.disabled       = false ;
        btnPrevious.disabled   = true;
        btnFirst.disabled      = true;
        }

    }    
}


//-------------------
function GetFirstRow()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    //-----------------------------
    var btnLast = igtab_getElementById("btnLast", webTab.element);
    var btnNext = igtab_getElementById("btnNext", webTab.element);
    var btnPrevious = igtab_getElementById("btnPrevious", webTab.element);
    var btnFirst = igtab_getElementById("btnFirst", webTab.element);

    var Grid = igtab_getElementById("uwgDocumentPictures", webTab.element);
    if (Grid.control.Rows.length != 0)
    {
        uwgDocumentPictures_AfterSelectChangeHandler("uwgDocumentPictures", "UltraWebTab1xxctl0xuwgDocumentPictures_r_0");
        btnLast.disabled       = false ;
        btnNext.disabled       = false ;
        btnPrevious.disabled   = true;
        btnFirst.disabled      = true;
    }
}

function GetLastRow()
{
    var webTab = igtab_getTabById("UltraWebTab1");
    //-----------------------------
    var btnLast = igtab_getElementById("btnLast", webTab.element);
    var btnNext = igtab_getElementById("btnNext", webTab.element);
    var btnPrevious = igtab_getElementById("btnPrevious", webTab.element);
    var btnFirst = igtab_getElementById("btnFirst", webTab.element);

    var Grid = igtab_getElementById("uwgDocumentPictures", webTab.element);

    if (Grid.control.Rows.length != 0)
    {
    var intRowId    = Grid.control.Rows.getLastRowId()  ;
    uwgDocumentPictures_AfterSelectChangeHandler("uwgDocumentPictures",intRowId  );
        btnLast.disabled       = true ;
        btnNext.disabled       = true ;
        btnPrevious.disabled   = false;
        btnFirst.disabled      = false;
    }
}
//------------------

