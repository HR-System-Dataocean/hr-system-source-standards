<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraisalResult.aspx.vb"
    Inherits="frmEmployeesVacations" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employee Vacations</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_EmpVacations.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">


      
        function TlbMainToolbar_Click(oToolbar, oButton, oEvent) {
            var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
            if (tlbControl.Items.fromKey("Payments").Selected == true) {
                btnVacationTransactionOn_Click();
            }
        }

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

        var ODialoge;
        var OSender;
        var empVacationId;


        function uwgEmployeeAppraisal_ClickCellButtonHandler(gridName, cellId) {
            try {
                debugger;

                var row = igtbl_getActiveRow(gridName);
                if (!row) {
                    alert("لا يمكن العثور على الصف المحدد");
                    return;
                }

                var appraisalCell = row.getCellFromKey("AppraisalID");
                var employeeCell = row.getCellFromKey("EmployeeID");
                var CriteriaCell = row.getCellFromKey("CriteriaID");

                if (!appraisalCell || !employeeCell) {
                    alert("بيانات التقييم غير مكتملة");
                    return;
                }

                var AppraisalID = appraisalCell.getValue();
                var EmployeeID = employeeCell.getValue();
                var CriteriaID = CriteriaCell.getValue();
  

                OpenModalNew('FrmAppraisalCriteriaResult.aspx?EmployeeID=' + EmployeeID +
                          '&AppraisalID=' + AppraisalID + '&CriteriaID=' + CriteriaID + '&', 800, 1200);

            } catch (e) {
                console.error("حدث خطأ: ", e);
                alert("حدث خطأ أثناء معالجة الطلب");
            }
        }


        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            debugger
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

        // New method for vacation module
        function OpenModalNew(pageurl, height, width) {
            
            var page = pageurl + "ItemId=" + empVacationId

            var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: height,
                    width: width
                });
            ODialoge = $dialog;
            $dialog.dialog('open');
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
        function OpenModal3(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("lbVactionID");
            if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {
                var page = pageurl + "RId=" + ctrId.value;
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
    </script>
    <style type="text/css">
        .igWebDateChooserMainBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
        
        .igWebDateChooserMainBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmEmployeesVacations" runat="server" defaultbutton="Button1">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:HiddenField ID="EmpVacationId" runat="server" />
        <asp:HiddenField ID="hdnWorkingHoursPerDay" runat="server" />
        <asp:HiddenField ID="hdnRequiredMonths" runat="server" />
        <asp:HiddenField ID="hdnAnnualVacId" runat="server" />
        <asp:HiddenField ID="hdnDurationDays" runat="server" />
        <asp:HiddenField ID="RemaningOPenBalanceDays" runat="server" Value="0" />
        <asp:HiddenField ID="OpenBalanceId" runat="server" Value="0" />
        <asp:LinkButton ID="LinkButton_OverDueMessage" runat="server" Visible="False" meta:resourcekey="LinkButton_OverDueMessageResource1">LinkButton</asp:LinkButton>
        <asp:TextBox ID="lbVactionID" runat="server" meta:resourcekey="lbVactionIDResource1"></asp:TextBox>
         <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="width: 10px; display: none">
                                <asp:Button ID="Button1" runat="server" OnClientClick="return false;" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    TabIndex="-1" />
                            </td>
                            <td style="width: 10px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server" 
                                    SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="15px" Height="12px" runat="server"
                                    ImageUrl="~/Pages/HR/Img/i.p.edit.gif" meta:resourcekey="ImageButton_PrintResource1"
                                    CommandArgument="Print" />
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP3" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 90px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                    CommandArgument="Property" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                    CommandArgument="Property"></asp:LinkButton>
                            </td>
                            <td style="width: 90px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                    CommandArgument="Remarks"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 90px">
                              
                            </td>
                            <td>
                                <asp:ImageButton ID="textbtn" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command" 
                                  meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" Visible="False" />
                             </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Documents" runat="server" CommandArgument="Documents" Visible="False"
                                    meta:resourcekey="ImageButton_DocumentsRec" Height="16px" ImageUrl="./img/abook_add_1.gif"
                                    Width="16px" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Role" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRole_Command" OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;" meta:resourcekey="ImageButton_RoleResource1" />
                                <asp:LinkButton ID="LinkButton_Role" runat="server" Text="الإجراءات" meta:resourcekey="LinkButton_RoleResource1"
                                    OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;"></asp:LinkButton>
                            </td>
                            <td style="width: 50px">
                            </td>
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
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                       
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
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
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                    </td>
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
                            <igtab:Tab Text="نتائج التقييمات" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 750px; vertical-align: top"
                                        cellspacing="0">
                                <%--        <tr>
                                            <td style="height: 10px" colspan="3">
                                                   
                                            </td>
                                        </tr>--%>
                               
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                   
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                               
                                                </table>
                                            </td>
                                         <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                     

                                                                                                        <tr>
    <td class="SeparArea">
    </td>
    <td class="LabelArea">
        <asp:Label ID="LblAppraisal" Width="90px" runat="server" Text="التقييم" SkinID="Label_DefaultNormal"
            meta:resourcekey="LblAppraisalResource1"></asp:Label>
    </td>
    <td class="DataArea">
      <asp:DropDownList ID="ddlAppraisals" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
          meta:resourcekey="ddlAppraisalsResource1" TabIndex="3">
      </asp:DropDownList>
  </td>
</tr>
                                                    
                                                                                                        <tr>
    <td class="SeparArea">
    </td>
    <td class="LabelArea">
        <asp:Label ID="LblCriteria" Width="90px" runat="server" Text="المعايير" SkinID="Label_DefaultNormal"
            meta:resourcekey="LblCriteriaResource1"></asp:Label>
    </td>
    <td class="DataArea">
      <asp:DropDownList ID="ddlCriteria" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
          meta:resourcekey="ddlCriteriaResource1" TabIndex="3">
      </asp:DropDownList>
  </td>
</tr>
    <tr>
    <td class="SeparArea">
    </td>
      <td class="LabelArea">
        <asp:Label ID="LblEmployees" Width="90px" runat="server" Text="الموظفين" SkinID="Label_DefaultNormal"
            meta:resourcekey="LblEmployeesResource1"></asp:Label>
    </td>
    <td class="DataArea">
      <asp:DropDownList ID="ddlEmployees" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
          meta:resourcekey="ddlCriteriaResource1" TabIndex="3">
      </asp:DropDownList>
 </td>
  </td>
</tr>


                                                     <tr>
             <td style="height: 10px" colspan="3">
                    
             </td>
         </tr>

                                                <tr>
    <td class="SeparArea">
    </td>
      <td class="SeparArea">
 </td>
                                              <td colspan="5" style="text-align: center; padding-top: 8px;">
                                                        <igtxt:WebImageButton ID="btnSearch" runat="server" Height="5px"   Style="font-family: Tahoma;
           font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnSearchResource1"
           Overflow="NoWordWrap" Text=" اظهار النتائج " UseBrowserDefaults="False" Width="150px">
           <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
           <Appearance>
               <Image Url="./img/forum_search.gif" />
               <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                   ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                   WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
           </Appearance>
       </igtxt:WebImageButton>
                                            </td>
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
 
                                             <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                           
                                            
                                            <td style="width: 6%; height: 16px; vertical-align: top">
</td><td style="width: 6%; height: 16px; vertical-align: top">
</td>
                                            
                                     
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEmployeeAppraisal"   runat="server" EnableAppStyling="False"
                                                        Height="650px" meta:resourcekey="uwgEmployeeAppraisalResource1" SkinID="Default"
                                                        Width="99%">
                                                       
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeAppraisal" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="650px"
                                                                Width="99%">
                                                            </FrameStyle>
                                                           <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="uwgEmployeeAppraisal_ClickCellButtonHandler" />
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
                                                            <RowSelectorStyleDefault Font-Size="7pt" Width="40px">
                                                            </RowSelectorStyleDefault>
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
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                        Key="ID" meta:resourcekey="IDResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="ID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
 

                                                                             <igtbl:UltraGridColumn BaseColumnName="AppraisalID" DataType="System.Int32"
                                                                                      Hidden="True"    Key="AppraisalID" meta:resourcekey="AppraisalIDResource1"  >
           
                                                                                          <Header Caption="AppraisalID">
                                                                                          </Header>
                                                                              </igtbl:UltraGridColumn>

                                                            <igtbl:UltraGridColumn BaseColumnName="CriteriaID" DataType="System.Int32"
                                                                         Hidden="True"    Key="CriteriaID" meta:resourcekey="AppraisalIDResource1"  >
           
                                                                             <Header Caption="CriteriaID">
                                                                             </Header>
                                                                 </igtbl:UltraGridColumn>
                                                                    
                                                            <igtbl:UltraGridColumn BaseColumnName="EmployeeID" DataType="System.Int32"
                                                                           Hidden="True"     Key="EmployeeID" meta:resourcekey="AppraisalIDResource1"  >
           
                                                                             <Header Caption="Employee ID">
                                                                             </Header>
                                                                 </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="EmployeeName" DataType="System.Int32" Width="25%"
                     Key="EmployeeName" meta:resourcekey="EmployeeNameResource1"  >
           
                     <Header Caption="اسم الموظف">
                     </Header>
         </igtbl:UltraGridColumn>
                                                                      <igtbl:UltraGridColumn BaseColumnName="Criteria" DataType="System.String" Width="25%"
                                                                               Hidden="False" Key="Criteria" meta:resourcekey="CriteriaResource2">
      
                                                                              <Header Caption="معيار التقييم">
                                                                                  <RowLayoutColumnInfo OriginX="1" />
                                                                              </Header>
                                                                              <Footer>
                                                                                  <RowLayoutColumnInfo OriginX="1" />
                                                                              </Footer>
                                                                          </igtbl:UltraGridColumn>
                                                                      <igtbl:UltraGridColumn BaseColumnName="MinimumScore" DataType="System.String" Width="8%"
                                                                             Hidden="False" Key="MinimumScore" meta:resourcekey="MinimumScoreResource2">
      
                                                                            <Header Caption="اقل قيمة">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                      <igtbl:UltraGridColumn BaseColumnName="MaximumScore" DataType="System.String" Width="8%"
                                                                             Hidden="False" Key="MaximumScore" meta:resourcekey="MaximumScoreResource2">
      
                                                                            <Header Caption="اكبر قيمة">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                       <igtbl:UltraGridColumn BaseColumnName="Result" DataType="System.String" Width="7%"
                                                                              Hidden="False" Key="Result" meta:resourcekey="ResultResource2">
      
                                                                             <Header Caption=" التقييم">
                                                                                 <RowLayoutColumnInfo OriginX="1" />
                                                                             </Header>
                                                                             <Footer>
                                                                                 <RowLayoutColumnInfo OriginX="1" />
                                                                             </Footer>
                                                                         </igtbl:UltraGridColumn>

                                                                      <igtbl:UltraGridColumn BaseColumnName="AppCriteriaScore" DataType="System.String" Width="16%"
         Hidden="False" Key="AppCriteriaScore" meta:resourcekey="AppCriteriaScoretResource2">
      
        <Header Caption=" النسبة من اجمالي التقييم">
            <RowLayoutColumnInfo OriginX="1" />
        </Header>
        <Footer>
            <RowLayoutColumnInfo OriginX="1" />
        </Footer>
    </igtbl:UltraGridColumn>
                                                                     <igtbl:UltraGridColumn Type="Button" Width="10%" AllowRowFiltering="False" CellButtonDisplay="Always"  
                                                                    meta:resourcekey="BtnDetaisResource16">
                                                                    <Header Caption="تفاصيل">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                    <CellButtonStyle BackgroundImage="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" BorderStyle="None"
                                                                        Cursor="Hand" Height="12px" Width="13px">
                                                                    </CellButtonStyle>
                                                                         </igtbl:UltraGridColumn>


                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Key="VacationTypeID"
                                                                        meta:resourcekey="UltraGridColumnResource1" Type="DropDownList" Width="200px">
                                                                        <Header Caption="Vacation Type">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualStartDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualStartDate" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="220px">
                                                                        <Header Caption="Start Date">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualEndDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualEndDate" meta:resourcekey="UltraGridColumnResource5"
                                                                        Width="220px">
                                                                        <Header Caption="End Date">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                              
                                                                   
                                                                      </Columns>

                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                                &nbsp;&nbsp;
                                            </td>
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
    <script type="text/javascript" id="igClientScript">
        $(document).ready(function () {
            var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")

                Deletebtn.click(function () {
                    if (confirm("هل انت متأكد من الحذف؟") == false)
                 {
                      
                        return false;
                 } 
               
            })
                  

        });
        
    </script>/script>ipt>
     
</body>
</html>
