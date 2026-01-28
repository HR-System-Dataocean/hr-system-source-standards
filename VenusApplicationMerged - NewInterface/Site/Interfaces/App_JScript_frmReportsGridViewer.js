/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
==============================================*/
    function uwgFormParameters_AfterColumnSizeChangeHandler(gridName, columnId, width)
    {
        var currColumns        = igtbl_getColumnById(columnId)
        var txtColumnsStyles   = window.document.getElementById("txtColumnsStyles")
        var currtxtStyle       = txtColumnsStyles.value ;        
            currtxtStyle       = SetValueFromString(currtxtStyle,currColumns.Key+"_CCW",width) 
        txtColumnsStyles.value = currtxtStyle
    }
/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
==============================================*/
    function uwgViewColumns_AfterSortColumnHandler(gridName, columnId)
    {
        var currColumns = igtbl_getColumnById(columnId)
        SetGridSortIndecator(currColumns.Index,gridName)
    }
/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
==============================================*/
    function uwgViewColumns_AfterColumnMoveHandler(gridName, columnId)
    {
	    SetAllColumnsRank(gridName)
    }
/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
==============================================*/
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
/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
==============================================*/
    function SetAllColumnsRank(gridName)
    {
        var txtRank      = window.document.getElementById("hfRank")        
        var grid         =  igtbl_getGridById(gridName);
        var currCol ;
        var currtxtStyle =  txtRank.value ;
        for(i=1;i<grid.Bands[0].Columns.length;i++)
        {
           currCol = grid.Bands[0].Columns[i]
           currtxtStyle = SetValueFromString(currtxtStyle,currCol.Key+"_Rank",currCol.Index)      
        }
        txtRank.value= currtxtStyle ;
    }
/*=============================================
-- Author        : [0261]
-- Date Created  : 28-01-2009 
-- Description   : 
                        0 - None
                        1 - Ascending
                        2 - Descending
                        3 - Disabled
==============================================*/

    function SetGridSortIndecator(SortColIndex,gridName)
    {
        var grid              = igtbl_getGridById(gridName)
        var txtColumnsStyles  = window.document.getElementById("txtColumnsStyles")
        var strCurrtxtStyle   = txtColumnsStyles.value;
        for(i=0;i< grid.Bands[0].Columns.length ;i++)
        {
            var col = grid.Bands[0].Columns[i];
            if(i!=SortColIndex)
            {
                col.SortIndicator = 0              
                strCurrtxtStyle   = SetValueFromString(strCurrtxtStyle ,col.Key+"_CSORT",'0')                  
            }
            else
            {
                strCurrtxtStyle   = SetValueFromString(strCurrtxtStyle ,col.Key+"_CSORT",col.SortIndicator)
            }
        }
     txtColumnsStyles.value = strCurrtxtStyle ;
    }
//---------------------------------------------------------------------