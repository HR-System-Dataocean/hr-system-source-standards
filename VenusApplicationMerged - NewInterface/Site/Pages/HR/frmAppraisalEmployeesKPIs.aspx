<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraisalEmployeesKPIs.aspx.vb"
    Inherits="frmAppraisalEmployeesKPIs" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Documents Types</title>
    <script src="../HR/Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>

    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script type="text/javascript">
       // flag to add listener for resize events
       var _onloadFlag = true;
       function adjustHeight() {
           var myHeight = 0;
           if (typeof (window.innerWidth) == 'number') {
               myHeight = window.innerHeight;
           } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
               myHeight = document.documentElement.clientHeight;
           } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
               myHeight = document.body.clientHeight;
           }
           var tab = igtab_getTabById('UltraWebTab1');
           // <td> which is used as content pane
           var cp = document.getElementById(tab.ID + '_cp');
           // <table> of tab
           var table = tab.element;
           // <div> container of tab
           var container = table.parentNode;
           // height available for tab
           var height = container.clientHeight;
           height = (myHeight - 85);
           if (!height) return;
           // difference between heights of tab and content pane
           var heightShift = tab._myHeightShift;
           // 4 - is adjustment for top/bottom borders of tab
           if (!heightShift)
               heightShift = tab._myHeightShift = (table.offsetHeight - cp.offsetHeight + 4);
           // calculate height for content pane (can be improved for different browsers)
           height -= heightShift;
           if (height < 0) return;
           // set height of content pane to make height of tab to fit with container
           if (table.offsetHeight < (myHeight - 85)) {
               cp.style.height = height + 'px';
           }
           if (!_onloadFlag)
               return;
           _onloadFlag = false;
           // process onresize events
           ig_shared.addEventListener(window, 'resize', adjustHeight);
       }

       var Row;
       var IsEdit = true;
       function uwg_AfterCellUpdateHandler(gridName, cellId) {
           if (IsEdit == true) {
               var cell = igtbl_getCellById(cellId);
               Row = igtbl_getRowById(cellId);

               var count = igtbl_getGridById(gridName).Rows.length - 1;
               var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

               if (rowIndex == count) {

                   igtbl_addNew(gridName, 0, true, false);

               }
           }
       }

       var cell;
       function uwgEnterCellEdit(gridName, cellId) {
           cell = cellId;

       }
       // Renumber Serial column by client row order
       function renumberKpiSerials(gridName) {
           try {
               var grid = igtbl_getGridById(gridName);
               if (!grid) return;
               var rows = grid.getRows();
               if (!rows) return;
               for (var i = 0; i < rows.getLength(); i++) {
                   var row = rows.getRow(i);
                   if (!row) continue;
                   var cell = row.getCellFromKey('Serial');
                   if (cell) cell.setValue(i + 1);
               }
           } catch (e) { }
       }

       function onKpiRowAdded(gridName, cellId) {
           renumberKpiSerials(gridName);
       }

       function onKpiRowsDeleted(gridName, cellId) {
           renumberKpiSerials(gridName);
       }
       var ODialoge;
       var OSender;
       function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
           if (CheckID == false) {
               var page = pageurl;
               var $dialog = $('<div></div>')
                   .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                   .dialog({
                       autoOpen: false,
                       modal: true,
                       height: height,
                       width: width
                   });
               ODialoge = $dialog;
               OSender = SenderCtrl;
               $dialog.dialog('open');
           }
       }
       function CloseIt(retvalue) {
           if (retvalue != "") {
               var Sender = window.document.getElementById(OSender);
               Sender.value = retvalue;
               Sender.focus();
           }
           var $dialog = ODialoge;
           $dialog.dialog('close');
       }
   </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmDocumentsTypes" runat="server">
        <div style="display: none">
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
             <igtxt:WebDateTimeEdit ID="txtDate" runat="server" meta:resourcekey="txtDateResource1"
            DisplayModeFormat="dd/MM/yyyy" EditModeFormat="dd/MM/yyyy">
        </igtxt:WebDateTimeEdit>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="display:none">
                                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                        OnClientClick="SaveOtherFieldsData();" />
                                </td>
                                <td style="width: 120px">
                                    <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                        CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                        OnClientClick="SaveOtherFieldsData();" />
                                    <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                        meta:resourcekey="LinkButton_SaveNResource1" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                        CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                        SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                        SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                        SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
                                    <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" CommandArgument="Property"
                                        meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
                                    <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                        meta:resourcekey="LinkButton_RemarksResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                        SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                        SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" />
                                </td>
                                <td style="width: 10px"></td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                        SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                        SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
                                </td>
                                <td style="width: 30%"></td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 5%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; height: 42px; vertical-align: top">
                            <tr>
                                <td style="width: 32px; vertical-align: top">
                                    <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                        meta:resourcekey="Image_LogoResource1" />
                                </td>
                                <td style="width: 50%; vertical-align: middle">
                                    <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                                </td>
                                <td style="width: 50%; vertical-align: middle">
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;"></td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Details">
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                        
                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                              <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                         <td class="SeparArea">
                                                        </td>
                                                          <td class="LabelArea">
                                                            <asp:Label ID="lblEmployee" Width="120px" runat="server" Text=" الموظف "
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelEmployeeResource1"></asp:Label>
                                                        </td>
                                                       
                                                          <td>
                                                                         <asp:TextBox ID="txtEmployee" Width="130px" runat="server"  MaxLength="15"
                                                                AutoPostBack="True" meta:resourcekey="txtDelegatedResource1">
                                                           </asp:TextBox>
                                                                    
                                                                        <igtxt:WebImageButton ID="btnEmployeeSearch" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnEmployeeSearchResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>  
                                                        <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                         
                                                        <td>
                                                         <asp:TextBox ID="txtEmployeeName" Width="260px" runat="server"  MaxLength="100"
                                                                AutoPostBack="false"  meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                            </td>
                                                          <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                               <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                         <td class="SeparArea">
                                                        </td>
                                                          <td class="LabelArea">
                                                            <asp:Label ID="lblAppraisalType" runat="server" Width="120px" SkinID="Label_LargtNormal"
                                              Text="نوع التقييم" meta:resourcekey="lblAppraisalTypeResource1"></asp:Label>
                                                         </td>
                                                       
                                                          <td>
                                                                   <asp:DropDownList ID="DdlAppraisalType" runat="server" width="500px" AutoPostBack="True" SkinID="DropDownList_DefaultNormal"
                                              meta:resourcekey="DdlAppraisalTypeResource1" TabIndex="3"> 
                                          </asp:DropDownList>
    

                                                                    </td>  
                                                        <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                          <td class="spacearea">

                                                        </td>
                                                         <td class="spacearea">

                                                        </td>
                                                         <td class="spacearea">

                                                        </td>
                                                        <td>
                                                         
                                                            </td>
                                                          <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                            <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblFromDate" runat="server" 
                                                                meta:resourcekey="lblFromDateResource1" SkinID="Label_DefaultNormal" 
                                                                Style="height: 13px" Text="From Date" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 40%;">
                                                                <tr>
                                                                    <td>
                                                                       <%-- <igtxt:WebMaskEdit ID="txtFromDate" runat="server" InputMask="##/##/####" 
                                                                            SkinID="WebMaskEdit_Fix" Width="120px">
                                                                        </igtxt:WebMaskEdit>--%>
                                                                        <igsch:WebDateChooser ID="txtFromDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                            </td>
                                        </tr>

                                               <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                           <asp:Label ID="lblToDate" runat="server" 
                                                                meta:resourcekey="lblToDateResource1" SkinID="Label_DefaultNormal" 
                                                                Style="height: 13px" Text="To Date" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 40%;">
                                                                <tr>
                                                                    <td>
                                                                        <igsch:WebDateChooser ID="txtToDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                            </td>
                                        </tr>

                                             <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblTotalWeight" runat="server" 
                                                                meta:resourcekey="lblTotalWeightResource1" SkinID="Label_DefaultNormal" 
                                                                Style="height: 13px" Text="Total Weight" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 40%;">
                                                                <tr>
                                                                    <td>
                                                                         <asp:TextBox ID="txtTotalWeight" runat="server" SkinID="TextBox_SmalltNormalC"
                                                                MaxLength="5" meta:resourcekey="txtTotalWeightResource1"  Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                            </td>
                                        </tr>

                                            <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                         <td class="SeparArea">
                                                        </td>
                                                          <td class="LabelArea">
                                                            <asp:Label ID="lblDescription" runat="server" Width="120px" SkinID="Label_LargtNormal"
                                              Text="ملاحظات" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                                         </td>
                                                       
                                                          <td>
                                                                 <asp:TextBox ID="txtDescription" runat="server" SkinID="TextBox_LargeNormalC"
                                                                MaxLength="255" meta:resourcekey="lblDescResource1"></asp:TextBox>
    

                                                                    </td>  
                                                        <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                          <td class="spacearea">

                                                        </td>
                                                         <td class="spacearea">

                                                        </td>
                                                         <td class="spacearea">

                                                        </td>
                                                        <td>
                                                         
                                                            </td>
                                                          <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 75%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" EnableAppStyling="False"
                                                                Height="100%" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                                Width="100%">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                        Width="100%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwgEnterCellEdit" AfterRowInsertHandler="onKpiRowAdded"/>
                                                                    <Pager MinimumPagesForDisplay="2">
                                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                        </PagerStyle>
                                                                    </Pager>
                                                                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                    </EditCellStyleDefault>
                                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </FooterStyleDefault>
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                        <Padding Left="3px" />
                                                                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                                    </RowStyleDefault>
                                                                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                                    </GroupByRowStyleDefault>
                                                                    <GroupByBox Hidden="True">
                                                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                        </BoxStyle>
                                                                    </GroupByBox>
                                                                    <AddNewBox>
                                                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                        </BoxStyle>
                                                                    </AddNewBox>
                                                                    <ActivationObject BorderColor="" BorderWidth="">
                                                                    </ActivationObject>
                                                                    <FilterOptionsDefault>
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1"
                                                                        AllowAdd="Yes">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid"
                                                                                BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                           
                                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True" Width="1px">
                                                                                <Header Caption="">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Serial" Key="Serial" AllowUpdate="No"  meta:resourcekey="UltraGridColumnLine" Width="5%">
                                                                                <Header Caption="Line">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="KPI" Key="KPI"
                                                                                meta:resourcekey="UltraGridColumnKPI" Width="70%" Type="DropDownList">
                                                                                
                                                                                <Header Caption="KPI">

                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
<%--                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="EngName" Key="EngName"
                                                                                meta:resourcekey="UltraGridColumnEngName" Width="25%">
                                                                                <Header Caption="EngName">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>--%>
                                                                           <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="MinimumScore" Key="MinimumScore" 
                                                                     meta:resourcekey="UltraGridColumnMinimumScore" Width="10%">
                                                                                <Header Caption="MinimumScore">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="MaximumScore" Key="MaximumScore" 
                                                                     meta:resourcekey="UltraGridColumnMaximumScore" Width="10%">
                                                                                <Header Caption="MaximumScore">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Weight" Key="Weight" 
                                                                    meta:resourcekey="UltraGridColumnWeight"  Width="10%">
                                                                                <Header Caption="Weight">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </igtab:Tab>
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
