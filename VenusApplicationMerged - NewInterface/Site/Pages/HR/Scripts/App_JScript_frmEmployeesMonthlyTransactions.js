   var cEnter = 13;
   var oActiveCell = '';
   var oDeactiveCell = '' ;
   var blnEntere     = true ;
   var cEnter                   = 13;
   var cF9                      = 120;
   var cF8 = 119;
   var cDelete = 46;

   var cell;
   var cellID;
   var nextcell;
   var ColumnSum;
   var Row;
   var HiddenValues;

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 04-02-2009
   -- Description   : 
   ==============================================*/

   function btnVacations_Click(oButton, oEvent) {
       var strEmpCode = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value;
       var winopen = window.open("frmEmployeesVacations.aspx?EmployeeCode=" + strEmpCode, "_Parent"+1, "height=630,width=757,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");
       winopen.document.focus();
   }
   
function txtNoOfWorkingDays_ValueChange(oEdit, oldValue, oEvent)
{
//    var hdnPrepared = window.document.getElementById("hdnPrepared")
//    if (ConvertToNumber(hdnPrepared.value) == 0)
//    {
//        var grid = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions")
//        var firstCell = grid.Rows.rows[0].getCellFromKey("NumberofWorkingDays")
//        var diff = oEdit.getValue() - oldValue
//        firstCell.setValue(firstCell.getValue()+diff)
//    }
    
}
   

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 04-02-2009
   -- Description   : 
   ==============================================*/
function uwgDepartmentsTransactions_AfterCellUpdateHandler(gridName, cellId)
{

    if (! blnEntere ) return ;
    var grid           = igtbl_getGridById(gridName);
    var cell           = igtbl_getCellById(cellId);
    var Row            = igtbl_getRowById(cellId);
    var CountDays      = 0 ;
    var lang = window.document.getElementById("hdnLang");
    var hdnTotalMonthlyDays = window.document.getElementById("hdnTotalMonthlyDays");
    var intTotalMonthlyDays = ConvertToNumber(hdnTotalMonthlyDays.value)
    
    var msg = ""
    if (lang.value == "Arb")
        msg = "الحد الأقصى لعدد الأيام:"
    else
        msg = "Max Days ="  
    
    if(cell.Column.Key == 'Holidays' )
    {
        /* [E]
        if (cell.getValue() > Row.getCellFromKey('AWHours').getValue()) 
        {   
            blnEntere = false ;
            cell.setValue(Row.getCellFromKey('AWHours').getValue());
            GetColumnSum(cellId);
            blnEntere = true ; 
        } 
        */ 
        blnEntere = false ;
        GetColumnSum(cellId);
        blnEntere = true ;     
    }
   else if(cell.Column.Key == 'OvertimeHours' )
    {
        /* [E]
        if (cell.getValue() > Row.getCellFromKey('MaxOvertimeHours').getValue()) 
        {   
            blnEntere = false ;
            cell.setValue(Row.getCellFromKey('MaxOvertimeHours').getValue());
            GetColumnSum(cellId);
            blnEntere = true ; 
            
        }
        */
        blnEntere = false ;
        GetColumnSum(cellId);
        blnEntere = true ;      
    }
    else if (cell.Column.Key == 'NumberofWorkingDays' )
    {
        for(i=0;i<grid.Rows.length;i++)
        {
            var currRow = grid.Rows.rows[i];
            //var intVacationTypeID = currRow.getCellFromKey("VacationType").getValue()
            if ( currRow != undefined )
            {
                if(currRow.getCellFromKey("NumberofWorkingDays").getValue() > 0)
                {
                    CountDays = parseFloat(CountDays) + parseFloat(currRow.getCellFromKey("NumberofWorkingDays").getValue());
                }

//                if (intVacationTypeID != null) {
//                    var retVal = Update_EmployeesTransactions.CheckVacationType(intVacationTypeID)
//                    if (retVal.value == 0) {
//                        if (currRow.getCellFromKey("SickDays").getValue() > 0) {
//                            CountDays = parseFloat(CountDays) + parseFloat(currRow.getCellFromKey("SickDays").getValue());
//                        }
//                    } 
//                }
            }
        }
        if(CountDays > intTotalMonthlyDays)
        {
            var DaysValue = 0 ;
            DaysValue = parseFloat(CountDays) - intTotalMonthlyDays ;
            DaysValue = parseFloat(cell.getValue()) - parseFloat(DaysValue) ;
            msg += intTotalMonthlyDays
            alert(msg)
            blnEntere = false ;
            cell.setValue(DaysValue);
            GetColumnSum(cellId);
            SetWorkingDays() 
            blnEntere = true ; 
            //GetHiddenColumnsValues(cellId)
           
        }
        blnEntere = false ;
        GetColumnSum(cellId);
        SetWorkingDays()
        blnEntere = true ; 
        //GetHiddenColumnsValues(cellId)
        
    }
    else if (cell.Column.Key == 'SickDays' )
    {
        //if (cell.getValue() > Row.getCellFromKey('AWDays').getValue()) {cell.setValue(Row.getCellFromKey('AWDays').getValue());}     
        for(i=0;i<grid.Rows.length;i++)
        {
            var currRow = grid.Rows.rows[i];
            var intVacationTypeID = currRow.getCellFromKey("VacationType").getValue()
            if ( currRow != undefined )
            {

//                if (intVacationTypeID != null) {
//                    var retVal = Update_EmployeesTransactions.CheckVacationType(intVacationTypeID)
//                    if (retVal.value == 0) {
//                        if (currRow.getCellFromKey("SickDays").getValue() > 0) {
//                            CountDays = parseFloat(CountDays) + parseFloat(currRow.getCellFromKey("SickDays").getValue());
//                        }
//                    }
//                }
               
                if(currRow.getCellFromKey("NumberofWorkingDays").getValue() > 0)
                {
                    CountDays = parseFloat(CountDays) + parseFloat(currRow.getCellFromKey("NumberofWorkingDays").getValue());
                }
            }
        }

        if(CountDays > intTotalMonthlyDays)
        {
            var DaysValue = 0 ;
            DaysValue = parseFloat(CountDays) - intTotalMonthlyDays ;
            DaysValue = parseFloat(cell.getValue()) - parseFloat(DaysValue) ;
            msg += intTotalMonthlyDays
            alert(msg)
            blnEntere = false ; 
            cell.setValue(DaysValue);
            GetColumnSum(cellId);
            SetWorkingDays()
            blnEntere = true ; 
        }
        blnEntere = false ;
        GetColumnSum(cellId);  
        SetWorkingDays()
        blnEntere = true ; 
    }
    
}


/* =============================================
   -- Author         : [0261]
   -- Date Created  :  17-02-2009
   -- Description   :  Add new row to grid
   ==============================================*/
function uwgDepartmentsTransactions_AfterExitEditModeHandler(gridName, cellId)
{
    var grid           = igtbl_getGridById(gridName);
    var cell           = igtbl_getCellById(cellId);
        oDeactiveCell  = cellId ;
    var Row            = igtbl_getRowById(cellId);
    var intRowIndex    = parseFloat(Row.getIndex())+1;
    var NextRow  ; 
    //var txtNoOfWorkingDays = igedit_getById("UltraWebTab1__ctl0_txtNoOfWorkingDays");

    if(cell.Column.Key == 'Holidays' || cell.Column.Key == 'Total' )  
    {
          if(intRowIndex == grid.Rows.length )
    {
        NextRow = igtbl_addNew(gridName,0,false,false);
        NextRow.remove();
        if(intRowIndex < 0) intRowIndex=0;   
        grid.Rows.insert(NextRow,intRowIndex);
            }
      //  txtNoOfWorkingDays.doPost()
   }
    
}

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 05-02-2009
   -- Description   : 
   ==============================================*/
function GetNextActiveCellDepartment(cell)
{
    if (cell == null)return cell
    var nextcell;
    nextcell= cell.getNextTabCell()
    if (nextcell ==null) return cell;
        if (nextcell.isEditable() == false)
        {
            return GetNextActiveCellDepartment(nextcell)
        }
        else 
        {
            return nextcell
        }
}
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 05-02-2009
   -- Description   : 
   ==============================================*/
function wneGlobal_KeyDown(oEdit, keyCode, oEvent)
{
    var cell     = igtbl_getCellById(oActiveCell) 
    var Row      = igtbl_getRowById(oActiveCell);
    var e        = window.event;
    var nextcell ;   
    if(keyCode  ==  cEnter || keyCode  == 9)
     { 
        nextcell       = GetNextActiveCellDepartment(cell)
        Row.activate()
        Row.select;
        cell.setSelected(false);   
        nextcell.activate()
        nextcell.select;
        nextcell.beginEdit();
        return;
    }
    else if (e.ctrlKey && keyCode == cDelete)
     {
        var nextRow = Row.getNextRow();
        if(Row != null && Row!= undefined && nextRow != null) 
        { 
         nextcell = nextRow.getCellFromKey("ProjectID");          
         Row.remove();
         nextRow.activate()
         nextRow.select;
         nextcell.activate()
         nextcell.select;
         nextcell.beginEdit();
         }

     }
     SetWorkingDays()
}
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 05-02-2009
   -- Description   : 
   ==============================================*/
function uwgDepartmentsTransactions_AfterEnterEditModeHandler(gridName, cellId)
{
    oActiveCell = cellId; 
}


/* =============================================
   -- Author        : [0261]
   -- Date Created  : 10-02-2009
   -- Description   : Set Working Days
   ==============================================*/
function SetWorkingDays()
{
    var txtNoOfWorkingDays = igedit_getById("UltraWebTab1__ctl0_txtNoOfWorkingUnits");
    var ctrGrid            = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions");
    var currentrow ;
    var intWorkingDays = 0 ;
    for (i=0;i<ctrGrid.Rows.length ;i++) {
        currentrow = igtbl_getRowById(ctrGrid.Id + "_r_" + i);
        if (currentrow == null) {
            currentrow = ctrGrid.Rows.rows[i];  
        }     
        if (currentrow.getCellFromKey("NumberofWorkingDays").getValue() > 0) {
              intWorkingDays = parseFloat(intWorkingDays) + parseFloat(currentrow.getCellFromKey("NumberofWorkingDays").getValue());
        }
//        var intVacationTypeID = currentrow.getCellFromKey("VacationType").getValue()
//        if (intVacationTypeID != null) {
//            var retVal = Update_EmployeesTransactions.CheckVacationType(intVacationTypeID)
//            if (retVal.value == 0) {
//                if (currentrow.getCellFromKey("SickDays").getValue() > 0) {
//                    intWorkingDays = parseFloat(intWorkingDays) + parseFloat(currentrow.getCellFromKey("SickDays").getValue());
//                }
//            }
//        }
      //if(currentrow.getCellFromKey("SickDays").getValue() > 0 )
        //intWorkingDays = parseFloat(intWorkingDays)+ parseFloat(currentrow.getCellFromKey("SickDays").getValue());               
     
    }
    //txtNoOfWorkingDays.setValue(parseFloat(intWorkingDays));
    //txtNoOfWorkingDays.doPost()
}
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 17-02-2009
   -- Description   : Handel Mouse event
   ==============================================*/
   function uwgDepartmentsTransactions_CellClickHandler(gridName, cellId, button)
   {
     var Row            = igtbl_getRowById(oDeactiveCell);
     var CurrentRow     = igtbl_getRowById(cellId);
     var cell           = igtbl_getCellById(cellId);
     var grid = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions");
     var RightActiveCell;
         
     //if ( (CurrentRow.getCellFromKey('ProjectID').getValue()==null && cell.Column.Key == 'ProjectID') || (cell.Column.Key != 'ProjectID' && CurrentRow.getCellFromKey('ProjectID').getValue()== null ))
     if (CurrentRow.getCellFromKey('ProjectID').getValue()==null)
     {
        for(i=0;i<grid.Rows.length;i++)
        {
            var currRow = grid.Rows.rows[i];
            if (currRow == null)
                currRow = igtbl_getRowById("UltraWebTab1xxctl2xuwgDepartmentsTransactions_r_"+i)
            if ( currRow != null )
                if(currRow.getCellFromKey('ProjectID').getValue()== null)
                {
                   RightActiveCell = currRow.getCellFromKey('ProjectID')
                   break; 
                }
        }
        RightActiveCell.activate()
        RightActiveCell.select;
        RightActiveCell.beginEdit() 
     
     }
     
   }
   
/* =============================================
   -- Author        : [0261]
   -- Date Created  : 17-02-2009
   -- Description   : Handel Before Enter Edit Mode
   ==============================================*/
function uwgDepartmentsTransactions_BeforeEnterEditModeHandler(gridName, cellId)
{
    var grid           = igtbl_getGridById(gridName);
    var Row            = igtbl_getRowById(cellId);
    var CellProject    = Row.getCellFromKey("ProjectID");
    var currentcell    = igtbl_getCellById(cellId);
    
     if (currentcell.Column.Key != 'ProjectID' )
     if (CellProject.getValue() == null)
        {
          CellProject.activate()
          CellProject.select;
          CellProject.beginEdit();
          return 1 ;
        }
}

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 02-03-2009
   -- Description   : Handel Key Down
   ==============================================*/
   function wneSickDays_KeyDown(oEdit, keyCode, oEvent){
    var grid = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions"); 
    var ACell = grid.getActiveCell()
    var e        = window.event;
    var cell           = igtbl_getCellById(oActiveCell) 
    var Row            = igtbl_getRowById(oActiveCell);
    var nextcell ;

    if(keyCode == cEnter || keyCode  ==  9)
    {
        nextcell       = GetNextActiveCellDepartment(cell)
        Row.activate()
        Row.select;
        cell.setSelected(false);   
        nextcell.activate()
        nextcell.select;
        nextcell.beginEdit();
        return 1 ;
    }
    else if (e.ctrlKey && keyCode == 68)
    {
            var nextRow = Row.getNextRow();
            if(Row != null && Row!= undefined && nextRow != null) 
             { 
             nextcell = nextRow.getCellFromKey("ProjectID");          
             Row.remove();
             nextRow.activate()
             nextRow.select;
             nextcell.activate()
             nextcell.select;
             nextcell.beginEdit();
             }
    }
    SetWorkingDays()
}

/* =============================================
   -- Author        : [0261]
   -- Date Created  : 02-03-2009
   -- Description   : Handel Key Down
   ==============================================*/
  
   function GetColumnSum(cellID){
       
       var grid      = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions");

       var txtNoOfWorkingDays = igedit_getById('UltraWebTab1__ctl0_txtNoOfWorkingUnits');
       var txtHolidayWorkHours  = igedit_getById('UltraWebTab1__ctl0_txtHolidayWorkHours');
       var txtOvertimeWorkHours = igedit_getById('UltraWebTab1__ctl0_txtOvertimeWorkHours');
       
        cell      = igtbl_getCellById(cellID);
       var cellKey   = cell.Column.Key ;
       var ColumnSum = 0;
       for(i=0;i<grid.Rows.length;i++)
        {
         var currRow = grid.Rows.rows[i];
            if ( currRow != undefined )
                if(currRow.getCellFromKey(cellKey).getValue() > 0)
                {
                ColumnSum = parseFloat(ColumnSum) + parseFloat(currRow.getCellFromKey(cellKey).getValue());
                }
        }

       
       cell.Column.setFooterText( Math.round ( RoundDecimalNumber(ColumnSum)) )
       
        switch (cellKey)
        {
            case 'OvertimeHours' :
            {
             txtOvertimeWorkHours.setValue(ColumnSum);
             SetStaticValues(cellID);
             break ;
            }

            case 'Holidays'  :
            {
             txtHolidayWorkHours.setValue(ColumnSum);
             SetStaticValues(cellID);
             break ;
            }
            case 'SickDays'  :
            {
             SetSickDaysValue(cellID);
             break ;
            }

            case 'NumberofWorkingDays'  :
            { 
             //txtNoOfWorkingDays.setValue(ColumnSum);
             SetWorkinDaysValue(cellID);
             break ;
            }
        }
       SetRowSumation(cellID) ; 
}
   /* =============================================
   -- Author        : [0261]
   -- Date Created  : 02-03-2009
   -- Description   :  
   ==============================================*/   
   function SetRowSumation(cellID) {
     var Row                  = igtbl_getRowById(cellID); 
     var nuTotal              = 0 ; 
     var dblWorkingDays       = Row.getCellFromKey("NumberofWorkingDaysValue").getValue() ;  
     var dblOvertimeWorkHours = Row.getCellFromKey("OvertimeHoursValue").getValue() ;   
     var SickDaysValue        = Row.getCellFromKey("SickDaysValue").getValue() ;   
     var dblHolidayWorkHours  = Row.getCellFromKey("HolidaysValue").getValue() ;   
     var CellTotal            = Row.getCellFromKey("Total");  
          
     if (dblWorkingDays       == null || dblWorkingDays       == undefined ) { dblWorkingDays       = 0 ; }
     if (dblOvertimeWorkHours == null || dblOvertimeWorkHours == undefined ) { dblOvertimeWorkHours = 0 ; }
     if (SickDaysValue        == null || SickDaysValue        == undefined ) { SickDaysValue        = 0 ; }
     if (dblHolidayWorkHours  == null || dblHolidayWorkHours  == undefined ) { dblHolidayWorkHours  = 0 ; }

        nuTotal = parseFloat(dblWorkingDays) + parseFloat(dblOvertimeWorkHours) + parseFloat(SickDaysValue) + parseFloat(dblHolidayWorkHours) ;
        CellTotal.setValue(nuTotal);
        GetCellColumnSum(CellTotal.Id)
       
}

function GetCellColumnSum(cellID) {

    var txtNoOfWorkingDays = igedit_getById('UltraWebTab1__ctl0_txtNoOfWorkingUnits').getValue();
     var grid      = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions"); 
     cell      = igtbl_getCellById(cellID);
     var cellKey   = cell.Column.Key ;
     ColumnSum = 0;
     var RowID = cellID.split("_")[2] ;
     var strEmpCode           = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value ;
     var IntFisicalPeriod     = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value; 
     var dblSalaryPerDay      = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerDay').value;
     var dblSalaryPerHour     = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerHour').value;

     for (i = 0; i < grid.Rows.length; i++){
         var currRow = grid.Rows.rows[i];
         if (currRow != undefined) {
             if (currRow.getCellFromKey(cellKey).getValue() > 0) {
                 ColumnSum = parseFloat(ColumnSum) + parseFloat(currRow.getCellFromKey(cellKey).getValue());
             }
         }
     }
     if (cellID.localeCompare("UltraWebTab1xxctl2xuwgDepartmentsTransactions_rc_" + RowID + "_13") == 0){
         PageMethods.GetAllEmployeeDataAfterTextChange_Ajax(strEmpCode, IntFisicalPeriod, igedit_getById('UltraWebTab1__ctl0_txtNoOfWorkingUnits').getValue(), 0, 0, dblSalaryPerHour, dblSalaryPerDay, cellID, OnSucceeded, OnFailed);   
     }
        else 
     {
            cell.Column.setFooterText(Math.round(RoundDecimalNumber(ColumnSum)))
     }

}


/* =============================================
    -- Author        : [0261]
    -- Date Created  : 02-03-2009
    -- Description   :  
    ==============================================*/
function SetWorkinDaysValue(cellID) {
        var Row                  = igtbl_getRowById(cellID); 
        var strEmpCode           = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value ;
        var IntFisicalPeriod     = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value; 
        var dblSalaryPerDay      = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerDay').value;
        var dblSalaryPerHour     = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerHour').value;
        var lblTotalBenefits     = window.document.all.item("UltraWebTab1__ctl0_lblTotalBenefits").innerText;
        var dblWorkingDays       = Row.getCellFromKey("NumberofWorkingDays").getValue() ; 
        var cell                 = Row.getCellFromKey("NumberofWorkingDaysValue");
        var Total                = 0 ;
       
       if(dblWorkingDays         == null || dblWorkingDays       == undefined ) { dblWorkingDays       = 0 ; }
       try {
           
           Total = (lblTotalBenefits / 30) * dblWorkingDays;
           cell.setValue(Total); 
           
           //PageMethods.fnGetAllEmployeeDataAfterTextChange_Distributed_Ajax(strEmpCode, IntFisicalPeriod, dblWorkingDays, 0, 0, dblSalaryPerHour, dblSalaryPerDay, 0, cellID, OnSucceeded, OnFailed)
       }
       catch (e)
       {
        cell.setValue(0);
        GetCellColumnSum(cell.Id)
       }

}
   
   
   
      /* =============================================
   -- Author        : [0261]
   -- Date Created  : 02-03-2009
   -- Description   :  
   ==============================================*/
function SetSickDaysValue(cellID) {
    var Row = igtbl_getRowById(cellID);
    var strEmpCode = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value;
    var IntFisicalPeriod = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value;
    var dblSalaryPerDay = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerDay').value;
    var dblSalaryPerHour = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerHour').value;
    var Total = 0;

    var VacationType = Row.getCellFromKey("VacationType").getValue();
    var dblSickDays = Row.getCellFromKey("SickDays").getValue();
    cell = Row.getCellFromKey("SickDaysValue");

    if (dblSickDays == null || dblSickDays == undefined) { dblSickDays = 0; }
    try {
        PageMethods.fnGetAllEmployeeDataAfterTextChange_Distributed_Ajax(strEmpCode, IntFisicalPeriod, dblSickDays, 0, 0, dblSalaryPerHour, dblSalaryPerDay, VacationType, OnSucceeded, OnFailed)
    }
    catch (e) {
        cell.setValue(0);
        GetColumnSum(cell.Id)
    }
}
 /* =============================================
 -- Author        : [0261]
 -- Date Created  : 02-03-2009
 -- Description   :  
 ==============================================*/  
    function SetStaticValues(cellID){
        Row              = igtbl_getRowById(cellID); 
        cell             = igtbl_getCellById(cellID);

        var strEmpCode       = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value ;
        var IntFisicalPeriod = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value;
        PageMethods.GetEmployeesSalaryFactor(strEmpCode, IntFisicalPeriod, cellID, OnSucceeded, OnFailed);
}

var  LastPrevIndex = 0;
function GetPreviousRowsCount(Row)
{
    if(Row.getPrevRow() != null && Row.getPrevRow() != undefined )
    {
        LastPrevIndex += Row.getPrevRow().getCellFromKey('NumberofWorkingDays').getValue();
        GetPreviousRowsCount( Row.getPrevRow()) 
    }
        
}

    /* =============================================
     -- Author        : [0261]
     -- Date Created  : 11-03-2009
     -- Description   : Set Hidden Columns Values
    ==============================================*/
function GetHiddenColumnsValues(cellId) {
    var cell = igtbl_getCellById(cellId);
    Row = igtbl_getRowById(cellId);
    var strEmpCode = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value;
    var IntFisicalPeriod = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value;
    var intLenght = Row.getCellFromKey('NumberofWorkingDays').getValue()
    HiddenValues = '';
    var arrHiddenValues;

    LastPrevIndex = 0
    GetPreviousRowsCount(Row)

    if (LastPrevIndex == null || LastPrevIndex == undefined) {
        LastPrevIndex = 0;
    }
    else {
        LastPrevIndex = ConvertToNumber(LastPrevIndex);
    }
    // strEmpCode ,  intFiscalPeriodID ,  startIndex ,  intLength
    var txtNoOfWorkingDays = igedit_getById('UltraWebTab1__ctl0_txtNoOfWorkingUnits');
    PageMethods.GetProjectRecordInformation(strEmpCode, IntFisicalPeriod, LastPrevIndex, intLenght, txtNoOfWorkingDays.getValue(), OnSucceeded, OnFailed);


}




function RoundDecimalNumber(dblNumber)
 {
 dblNumber = dblNumber.toString();
 var arrNumber          ;
 var IntNumber          ;
 var DecimalNumber = 0  ;
 var TempDecimal        ;
 var intLenght     = 0  ;
 var tempNumber         ; 
 var ReturnNumber       ;
 var intMul = 1         ;
 if(dblNumber.indexOf("-")!=-1)
 {
 intMul = -1 ;        
 }
 else
 {
 intMul = 1 ;        
 }
 dblNumber =Math.abs(dblNumber).toString();
 if(dblNumber.indexOf(".")!=-1)
  {
            arrNumber  =  dblNumber.split(".");
            IntNumber    =  ConvertToNumber(arrNumber[0]);
            intLenght  =  arrNumber[1].length; 
            var arrdecimal ;
            if( intLenght  > 2)
            {         
                DecimalNumber    =  arrNumber[1].toString().substring(0,2);
                TempDecimal      =  ConvertToNumber(arrNumber[1].toString().substring(2,3));
               if(TempDecimal > 5 )
                {
                    DecimalNumber = ConvertToNumber(DecimalNumber)  + 1 ;
                    DecimalNumber = ConvertToNumber(DecimalNumber)
                   if(DecimalNumber >= 100)
                    {
                        tempNumber     =  ConvertToNumber(DecimalNumber.toString().substring(0,1));
                        DecimalNumber  =  ConvertToNumber(DecimalNumber.toString().substring(1,3));
                        IntNumber      =  ConvertToNumber(IntNumber) + ConvertToNumber(tempNumber) ;
                    }
                    else if (DecimalNumber < 10)
                        {
                        DecimalNumber = "0"+ DecimalNumber.toString()
                     
                        }
                }
                
            }
            else
            {
             DecimalNumber=arrNumber[1];
            }
  return  ReturnNumber= parseFloat( ConvertToNumber(IntNumber) +"."+DecimalNumber)* intMul ;     
  } 
  else
  {
  return  parseFloat(dblNumber)* intMul ;
  }      
 }
 
 
 
function frmEmployeeMonthlyTransactionProjectCode_KeyDown(oEdit, keyCode, oEvent)
{
    var language        = window.document.getElementById("hdnLang").value;
    var grid            = igtbl_getGridById("UltraWebTab1xxctl2xuwgDepartmentsTransactions"); 
    var ACell           = grid.getActiveCell()
    var e               = window.event;
    cell            = igtbl_getCellById(oActiveCell) 
    Row             = igtbl_getRowById(oActiveCell);
    nextcell ;

    if (keyCode == 27) //Escape
    {
       if (ACell != null)
       {
           ACell.activate()
           ACell.select()
           ACell.beginEdit()
       }
   }
    else if (e.ctrlKey && keyCode == cDelete)
    {
        var nextRow = Row.getNextRow();
        if(Row != null && Row!= undefined && nextRow != null) 
         { 
             nextcell = nextRow.getCellFromKey("ProjectID");          
             Row.remove();
             nextRow.activate()
             nextRow.select;
             nextcell.activate()
             nextcell.select;
             nextcell.beginEdit();
         }
         SetWorkingDays()
    }
    else if(keyCode == cEnter || keyCode  ==  9)
    {
        if(oEdit.getValue() == "        ")
        {
            var cell           = igtbl_getCellById(oActiveCell) 
            cell.setValue("")
            cell.activate();
            cell.select;
            cell.beginEdit(); 
        }
        else
            PageMethods.GetProjectDesc(oEdit.getValue(), language, OnSucceeded, OnFailed);

    }
    else if (keyCode == cF9)
    {
        var searchctrl=window.document.getElementById("txHiddenSearchID");
        var winopen = window.open("frmSearchScreen.aspx?TargetControl="+oActiveCell+"&SearchID=70","_Parent"+1,"height=560,width=725,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=yes");		
        winopen.document.focus();
    }
    else if (keyCode == 118)
    {
        gridArrowNavigation(keyCode,ind_CodeActiveCell);
    }
    
}




function cmbVacationsTypesID_EditKeyDown(webComboId, newValue, keyCode) {
    var cell = igtbl_getCellById(oActiveCell)
    var Row = igtbl_getRowById(oActiveCell);
    var cmb = igcmbo_getComboById(webComboId)
    var selectedIndex = cmb.getSelectedIndex()
    var e = window.event;
    var nextcell;


    var strEmpCode = window.document.getElementById('UltraWebTab1__ctl0_txtEmployeeCode').value;
    var IntFisicalPeriod = window.document.getElementById('UltraWebTab1__ctl0_ddlPeriod').value;
    var dblSalaryPerDay = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerDay').value;
    var dblSalaryPerHour = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerHour').value;
    var Total = 0;

    var VacationType = cmb.getValue();  //Row.getCellFromKey("VacationType").getValue();
    var dblSickDays = Row.getCellFromKey("SickDays").getValue();
    cell = Row.getCellFromKey("SickDaysValue");


    if (keyCode == cEnter || keyCode == 9) {



        if (dblSickDays == null || dblSickDays == undefined) { dblSickDays = 0; }
        try {
            PageMethods.fnGetAllEmployeeDataAfterTextChange_Distributed_Ajax(strEmpCode, IntFisicalPeriod, dblSickDays, 0, 0, dblSalaryPerHour, dblSalaryPerDay, VacationType, OnSucceeded, OnFailed)
        }
        catch (e) {
            cell.setValue(0);
            GetColumnSum(cell.Id)
        }
        
    
        nextcell = GetNextActiveCellDepartment(cell)
        Row.activate()
        Row.select;
        cell.setSelected(false);
        nextcell.activate()
        nextcell.select;
        nextcell.beginEdit();
        SetWorkingDays()
        return;
    }
    else if (keyCode == 32) // Space
    {
        if (cmb.getDropDown()) {
            cmbVacationsTypesID_EditKeyDown(webComboId, newValue, cEnter); //Can change with same event
        }
        else {
            cmb.setDropDown(true)

            cmb.focus()
            try {
                if (selectedIndex == -1)
                    cmb.setSelectedIndex(0);
                else
                    cmb.setSelectedIndex(selectedIndex);
            }
            catch (e)
                { }
        }

    }
    else if (keyCode == 27) //Escape
    {
        cmb.setDropDown(false)
        if (cell != null) {
            cell.activate()
            cell.select()
            cell.beginEdit()
        }
        cmb.focus()

    }
    SetWorkingDays()
}

function cmbVacationsTypesID_AfterSelectChange(webComboId) {

}

function frmEmpMonthlyTransuwgPayabilities_AfterCellUpdateHandler(gridName, cellId) {

    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var Tab = igtab_getTabById("UltraWebTab1")
    var rw = cell.getRow();
    var OldValue = rw.getCellFromKey("OrgValue").getValue();
    var IsDistr = rw.getCellFromKey("EmpSchId").getValue();

    var lblTotalBenefits = igtab_getElementById("lblTotalBenefits", Tab.element)
    var lblTotalDeductions = igtab_getElementById("lblTotalDeductions", Tab.element)
    var lblNetSalary = igtab_getElementById("lblNetSalary", Tab.element)

    if (cell.Column.Index == 1) {
        var val = cell.getValue();
        if (val < 0) {
            cell.setValue(0);
        }
        if (IsDistr > 0 && val > OldValue) {
            cell.setValue(OldValue);
        }
        var totalSum = 0;
        var strVacType = new String()
        var strPaid = new String()
        
        for (i = 0; i < grid.Rows.length; i++) {
            strVacType = igtbl_getCellById(gridName + "_rc_" + i + "_3").getValue()
            strPaid = igtbl_getCellById(gridName + "_rc_" + i + "_5").getValue()
            if (strPaid == "Not Paid")        
                continue;
            var valCell = igtbl_getCellById(gridName + "_rc_" + i + "_1")//grid.Rows.rows[i].cells[1]
            if (valCell != null && valCell.getValue() != "") {
                totalSum += valCell.getValue();
            }
        }
        lblTotalDeductions.value = totalSum;
        //lblTotalDeductions.innerText = lblTotalDeductions.value
        lblNetSalary.value = ConvertToNumber(lblTotalBenefits.value) - totalSum
        //lblNetSalary.innerText = lblNetSalary.value

    }
}

function uwgEmployeeTransaction_AfterCellUpdateHandler(gridName, cellId) {
   
    var grid = igtbl_getGridById(gridName);
    var cell = igtbl_getCellById(cellId);
    var Tab = igtab_getTabById("UltraWebTab1")
    var lblTotalBenefits = igtab_getElementById("lblTotalBenefits", Tab.element)
    var lblTotalDeductions = igtab_getElementById("lblTotalDeductions", Tab.element)
    var lblNetSalary = igtab_getElementById("lblNetSalary", Tab.element)
    var rw = cell.getRow();
    var OldValue = rw.getCellFromKey("OrgValue").getValue();
    var IsDistr = rw.getCellFromKey("EmpSchId").getValue();

    if (cell.Column.Index == 1) {
        var val = cell.getValue();
        if (val < 0) {
            cell.setValue(0);
        }
        if (IsDistr > 0 && val > OldValue) {
            cell.setValue(OldValue);
        }
        var totalSum = 0;
        var strVacType = new String()
        var strPaid = new String()
        
        for (i = 0; i < grid.Rows.length; i++) {
            strVacType = igtbl_getCellById(gridName + "_rc_" + i + "_3").getValue()
            strPaid = igtbl_getCellById(gridName + "_rc_" + i + "_6").getValue()
            if (strPaid == "Not Paid")
                continue;
            var valCell = igtbl_getCellById(gridName + "_rc_" + i + "_1")//grid.Rows.rows[i].cells[1]
            if (valCell != null && valCell.getValue() != "") {
                totalSum += valCell.getValue();
            }
        }
        lblTotalBenefits.value = totalSum;
        //lblTotalBenefits.innerText = lblTotalBenefits.value
        lblNetSalary.value = totalSum - ConvertToNumber(lblTotalDeductions.value)
        //lblNetSalary.innerText = lblNetSalary.value
    }
    
}






function OnSucceeded(result, userContext, methodName) {
    if (methodName == 'GetAllEmployeeDataAfterTextChange_Ajax') {
        var cell = igtbl_getCellById(result.split('|')[1]);
        cell.Column.setFooterText(RoundDecimalNumber(ColumnSum) + result.split('|')[0]);
        
        //SetRowSumation(cell.Id); 
    }
    else if (methodName == 'fnGetAllEmployeeDataAfterTextChange_Distributed_Ajax') {
        var cell = igtbl_getCellById(result.split('|')[1]);
        cell.setValue(result.split('|')[0]);
        GetCellColumnSum(cell.Id)

        //SetRowSumation(cell.Id);
        //GetHiddenColumnsValues(cellId);
    }
    else if (methodName == 'GetProjectRecordInformation') {
        if (result != '' || result != null || result != undefined) {
            var MaxOvertimeHours = parseFloat(result.split(',')[0]);
            var AWDays = parseFloat(result.split(',')[1]);
            var AWHours = parseFloat(result.split(',')[2]);
            var dblOvertimeHours = parseFloat(result.split(',')[3]);
            var intSickDays = parseFloat(result.split(',')[4]);
            var dblHoidaysHours = parseFloat(result.split(',')[5]);

            Row.getCellFromKey('MaxOvertimeHours').setValue(MaxOvertimeHours)
            Row.getCellFromKey('AWDays').setValue(AWDays)
            Row.getCellFromKey('AWHours').setValue(AWHours)
        }
    }
    else if (methodName == 'GetProjectDesc') {
        if (result != "" && result != null) {
            cell = igtbl_getCellById(oActiveCell);
            nextcell = GetNextActiveCellDepartment(cell);
            Row.activate()
            Row.select();
            Row.getCellFromKey('ProjectDescriptions').setValue(result)
            //cell.setSelected(false);
            try {
                nextcell.activate()
                nextcell.select();
                nextcell.beginEdit();
            }
            catch (ex) {
                alert(ex)
            }
        }
        else {
            cell = igtbl_getCellById(oActiveCell)
            cell.setValue("")
            cell.activate();
            cell.select;
            cell.beginEdit();
        }
    }
    else if (methodName == 'GetEmployeesSalaryFactor') {
        var dblSalaryPerDay = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerDay').value;
        var dblSalaryPerHour = window.document.getElementById('UltraWebTab1__ctl0_lblSalaryPerHour').value;
        var strFactor = result
        var OverTimeFactor = strFactor.split("$")[0];
        var HolidayFactor = strFactor.split("$")[1];
        var cell = igtbl_getCellById(strFactor.split('$')[2]);
        var cellKey = cell.Column.Key;
        var CellValue = cell.getValue();
        var Total = 0;

        if (CellValue == null || CellValue == undefined) { CellValue = 0; }

        if (cellKey == 'Holidays') {
            Total = parseFloat(CellValue) * parseFloat(dblSalaryPerHour) * parseFloat(HolidayFactor);
            Row.getCellFromKey('HolidaysValue').setValue(Total);
            GetColumnSum(Row.getCellFromKey('HolidaysValue').Id)
            //SetRowSumation(Row.getCellFromKey('HolidaysValue'))
        }
        else if (cellKey == 'OvertimeHours') {
            Total = parseFloat(CellValue) * parseFloat(dblSalaryPerHour) * parseFloat(OverTimeFactor);
            Row.getCellFromKey('OvertimeHoursValue').setValue(Total);
            GetColumnSum(Row.getCellFromKey('OvertimeHoursValue').Id)
            //SetRowSumation(Row.getCellFromKey('OvertimeHoursValue'))
        }
    }
    //    else if (methodName == '') {
    //    }
}


function OnFailed(error) {
   // alert(error.get_message());
}
