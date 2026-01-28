// JScript File

var gridName            = "uwgViewColumns"
var txtColumnsStyles    //= window.document.getElementById("txtColumnsStyles")
var grid                //= igtbl_getGridById(gridName)
var columnsStyles

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

var Format
var CalculatedSummary
var ShowCurrencySymbol
var Sort

function LoadGridColumnsStyles()
{
    txtColumnsStyles    = window.document.getElementById("txtColumnsStyles")
    grid = igtbl_getGridById(gridName)
    if (grid == null)
        return 
    columnsStyles = txtColumnsStyles.value;
    var currCol;
    for (i=0;i<grid.Bands[0].Columns.length ;i++)
    {
        currCol = grid.Bands[0].Columns[i];
        GetCurrColumnStyleValues(currCol.Key,"H")
        SetCurrColumnStyleValues(currCol.Key,"H")
        
        GetCurrColumnStyleValues(currCol.Key,"F")
        SetCurrColumnStyleValues(currCol.Key,"F")
        
    }//End For Each grid.Columns
}

function SetCurrColumnStyleValues(colKey,SectionType)
{
    var currStyle;
    var currCol = grid.Bands[0].getColumnFromKey(colKey);
    if(SectionType == "H")
    {
        if (currCol.IsGroupBy == true)
            return ;
        else 
            currStyle = currCol._getHeadTags()[0].style
    }
    else if (SectionType == "F")
    {
       if (currCol.IsGroupBy == true)
            return ;
        else 
            currStyle = currCol._getFootTags()[0].style          
    }
    
    if (backgroundColor != "" && backgroundColor !=0)
        currStyle.backgroundColor=backgroundColor
    if (borderColor != "" && borderColor!=0)
        currStyle.borderColor=borderColor
    if (borderBottomColor != "" && borderBottomColor!=0)    
        currStyle.borderBottomColor=borderBottomColor
    if (borderTopColor != "" && borderTopColor != 0)
        currStyle.borderTopColor=borderTopColor
    if (borderLeftColor != "" && borderLeftColor!=0)
        currStyle.borderLeftColor=borderLeftColor
    if (borderRightColor != "" && borderRightColor!=0)
        currStyle.borderRightColor=borderRightColor
    if (borderStyle != "")
    {
            if (borderStyle == "NotSet" ||borderStyle == "Notset")
                borderStyle = "none"
            currStyle.borderStyle=borderStyle
    }
    if (borderTopStyle != "")
    {
        if (borderTopStyle == "NotSet" || borderTopStyle == "Notset")
            borderTopStyle = "none"
        currStyle.borderTopStyle=borderTopStyle
    }
    if(borderLeftStyle != "")
    {
        if (borderLeftStyle == "NotSet" || borderLeftStyle == "Notset")
            borderLeftStyle = "none"
        currStyle.borderLeftStyle=borderLeftStyle
    }
    if (borderRightStyle != "")
    {
        if (borderRightStyle == "NotSet" || borderRightStyle == "Notset")
            borderRightStyle = "none"
        currStyle.borderRightStyle=borderRightStyle
    }
    if (borderBottomStyle != "")
    {
        if (borderBottomStyle == "NotSet" || borderBottomStyle == "Notset")
            borderBottomStyle = "none"
        currStyle.borderBottomStyle=borderBottomStyle
    }
    if (borderWidth != "")    
        currStyle.borderWidth=borderWidth
    if (borderTopWidth != "")
        currStyle.borderTopWidth=borderTopWidth
    if (borderBottomWidth != "")
        currStyle.borderBottomWidth=borderBottomWidth
    if (borderLeftWidth != "")
        currStyle.borderLeftWidth=borderLeftWidth
    if (borderRightWidth != "")
        currStyle.borderRightWidth=borderRightWidth
    if (fontFamily != "")
        currStyle.fontFamily=fontFamily
    if (fontSize != "")
        currStyle.fontSize=fontSize
   
    
    currStyle.fontStyle_bold=fontStyle_bold
    currStyle.fontStyle=fontStyle
    currStyle.textDecorationUnderline=textDecorationUnderline
    
    if (foreColor != "")
        currStyle.color=foreColor
    if (height != "")
        currStyle.height=height
    if (textAlign != "" && textAlign !="NotSet")
        currStyle.textAlign=textAlign
    if (margin != "")
        currStyle.margin=margin
//    currStyle.marginTop=marginTop
//    currStyle.marginBottom=marginBottom
//    currStyle.marginLeft=marginLeft
//    currStyle.marginRight=marginRight
    if (padding != "")
        currStyle.padding=padding
//    currStyle.paddingTop=paddingTop
//    currStyle.paddingBottom=paddingBottom
//    currStyle.paddingRight=paddingRight
//    currStyle.paddingLeft=paddingLeft
    if (verticalAlign != "" && verticalAlign != "NotSet")    
        currStyle.verticalAlign=verticalAlign
    
    if (width != "")
        currCol.setWidth(width+"px")
    //if (HeaderText != "")
    if (SectionType == "H")
        currCol.setHeaderText(HeaderText)
    else if (SectionType =="F")
        currCol.setFooterText(HeaderText)
     if (Sort == "1" || Sort=="2")
     {   
        currCol.SortIndicator   = Sort  
        //grid.AllowSort = 1 ;
        //grid.addSortColumn(currCol.Id,true)
        //grid.sort();
        //grid.AllowSort = 0 ;
     }
     
     
//    if(SectionType == "H")
//       currCol._getHeadTags()[0].style = currStyle 
//    else if (SectionType == "F")
//       currCol._getFootTags()[0].style = currStyle 
    
}

function GetCurrColumnStyleValues(colKey,SectionType)
{
     var key = colKey+"_"
            
     if (SectionType == "H")
         key +="CH" 
     else if (SectionType == "R")
         key +="CC" 
     else if (SectionType == "F")
         key +="CF" 
            
     try
     { 
            backgroundColor= GetValueFromString(columnsStyles,key+"BKC")
            borderColor= GetValueFromString(columnsStyles,key+"BC")
            borderBottomColor= GetValueFromString(columnsStyles,key+"BCB")
            borderTopColor= GetValueFromString(columnsStyles,key+"BCT")
            borderLeftColor= GetValueFromString(columnsStyles,key+"BCL")
            borderRightColor= GetValueFromString(columnsStyles,key+"BCR")
            borderStyle= GetValueFromString(columnsStyles,key+"BS")
            borderTopStyle= GetValueFromString(columnsStyles,key+"BST")
            borderLeftStyle= GetValueFromString(columnsStyles,key+"BSL")
            borderRightStyle= GetValueFromString(columnsStyles,key+"BSR")
            borderBottomStyle= GetValueFromString(columnsStyles,key+"BSB")
            borderWidth= GetValueFromString(columnsStyles,key+"BW")
            borderTopWidth= GetValueFromString(columnsStyles,key+"BWT")
            borderBottomWidth= GetValueFromString(columnsStyles,key+"BWB")
            borderLeftWidth= GetValueFromString(columnsStyles,key+"BWL")
            borderRightWidth= GetValueFromString(columnsStyles,key+"BWR")
            fontFamily= GetValueFromString(columnsStyles,key+"FN")
            
            fontSize= GetValueFromString(columnsStyles,key+"FS")
            
            if (GetValueFromString(columnsStyles,key+"FIB") =="True")
                fontStyle_bold= true
            else
                fontStyle_bold= false
            
            if (GetValueFromString(columnsStyles,key+"FII") =="True")
                fontStyle = "italic"
            else
                fontStyle = "normal"
            
            if (GetValueFromString(columnsStyles,key+"FIU") =="True")
                textDecorationUnderline = true
            else
                textDecorationUnderline = false
            
           
            foreColor= GetValueFromString(columnsStyles,key+"FC")
            height= GetValueFromString(columnsStyles,key+"H")
            textAlign= GetValueFromString(columnsStyles,key+"HA")
            margin= GetValueFromString(columnsStyles,key+"M")
//            marginTop= currStyle.marginTop
//            marginBottom= currStyle.marginBottom
//            marginLeft= currStyle.marginLeft
//            marginRight= currStyle.marginRight
            padding= GetValueFromString(columnsStyles,key+"P")
//            paddingTop= currStyle.paddingTop
//            paddingBottom= currStyle.paddingBottom
//            paddingRight= currStyle.paddingRight
//            paddingLeft= currStyle.paddingLeft
           verticalAlign = GetValueFromString(columnsStyles,key+"VA")
           width         = GetValueFromString(columnsStyles,key+"W")
           HeaderText    = GetValueFromString(columnsStyles,key+"TXT")
           
           Format        = GetValueFromString(columnsStyles,colKey+"_CFORMAT")
           
           if (GetValueFromString(columnsStyles,colKey+"_CCCURR")=="1")
                ShowCurrencySymbol = true
           else
                ShowCurrencySymbol = false
           
           CalculatedSummary = GetValueFromString(columnsStyles,colKey+"_CFTOTAL")
           Sort              = GetValueFromString(columnsStyles,colKey+"_CSORT")
           
      }
      catch(e){}
      

}
